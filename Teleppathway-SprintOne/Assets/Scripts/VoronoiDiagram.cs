using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Math;
using csDelaunay;
using System;

public class VoronoiDiagram : MonoBehaviour
{
    public Texture2D tx;
    // The number of polygons/sites we want
    public int polygonNumber = 200;

    // This is where we will store the resulting data
    private Dictionary<Vector2f, Site> sites;
    private List<Edge> edges;

    void Start()
    {
        // Create your sites (lets call that the center of your polygons)
        List<Vector2f> points = CreateRandomPoint();//Change it to specific points
        //Texture
        tx = new Texture2D(512, 512);

        // Create the bounds of the voronoi diagram
        // Use Rectf instead of Rect; it's a struct just like Rect and does pretty much the same,
        // but like that it allows you to run the delaunay library outside of unity (which mean also in another tread)
        Rectf bounds = new Rectf(0, 0, 512, 512);

        // There is a two ways you can create the voronoi diagram: with or without the lloyd relaxation
        // Here I used it with 2 iterations of the lloyd relaxation
        Voronoi voronoi = new Voronoi(points, bounds, 5);

        // But you could also create it without lloyd relaxtion and call that function later if you want
        //Voronoi voronoi = new Voronoi(points,bounds);
        //voronoi.LloydRelaxation(5);

        // Now retreive the edges from it, and the new sites position if you used lloyd relaxtion
        sites = voronoi.SitesIndexedByLocation;
        edges = voronoi.Edges;

        DisplayVoronoiDiagram();
    }
    //To Do
    private List<Vector2f> CreateRandomPoint()
    {
        // Use Vector2f, instead of Vector2
        // Vector2f is pretty much the same than Vector2, but like you could run Voronoi in another thread
        List<Vector2f> points = new List<Vector2f>();
        for (int i = 0; i < polygonNumber; i++)
        {
            points.Add(new Vector2f(UnityEngine.Random.Range(0, 512), UnityEngine.Random.Range(0, 512)));
        }

        return points;
    }

    // Here is a very simple way to display the result using a simple bresenham line algorithm
    // Just attach this script to a quad
    private void DisplayVoronoiDiagram()
    {
        //Texture2D tx = new Texture2D(512, 512);
        foreach (KeyValuePair<Vector2f, Site> kv in sites)
        {
            tx.SetPixel((int)kv.Key.x, (int)kv.Key.y, Color.red);
        }
        foreach (Edge edge in edges)
        {
            // if the edge doesn't have clippedEnds, if was not within the bounds, dont draw it
            if (edge.ClippedEnds == null) continue;
            //Jennifer
            DrawLine(edge.ClippedEnds[LR.LEFT], edge.ClippedEnds[LR.RIGHT], tx, Color.black);
        }
        tx.Apply();

        this.GetComponent<Renderer>().material.mainTexture = tx;
    }

    // Bresenham line algorithm
    private void DrawLine(Vector2f p0, Vector2f p1, Texture2D tx, Color c, int offset = 0)
    {
        DrawLine_xiaolinwu(p0, p1, tx, c, offset);
    }
    private void DrawLine_Bresenham(Vector2f p0, Vector2f p1, Texture2D tx, Color c, int offset = 0)
    {
        int x0 = (int)p0.x;
        int y0 = (int)p0.y;
        int x1 = (int)p1.x;
        int y1 = (int)p1.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            tx.SetPixel(x0 + offset, y0 + offset, c);

            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }

    private void DrawLine_xiaolinwu(Vector2f p0, Vector2f p1, Texture2D tx, Color c, int offset = 0)
    {
        rasterize_line_xiaolinwu(p0.x, p0.y, p1.x, p1.y, c);
    }
    //inline functions for xiaolinwu
    private float ipart(float x)
    {
        return (float)System.Math.Floor(x);
    }
    private float fpart(float x)
    {
        return x - (float)Math.Floor(x);
    }
    private float rfpart(float x)
    {
        return 1 - fpart(x);
    }
    private void rasterize_point(float x0, float y0, Color color)
    {
        tx.SetPixel((int)x0, (int)y0, color);
    }
    //Based on the xiaolinwu algorhitm on wiki
    private void rasterize_line_xiaolinwu(float x0, float y0, float x1, float y1, Color color)
    {
        int sample_rate = 1;
        bool steep = Math.Abs(x1 - x0) < Math.Abs(y1 - y0);
        if (steep)
        { // abs(slope) > 1
            float tmp = x0;x0 = y0; y0 = tmp;
            float tmp1 = x1;x1 = y1; y1 = tmp1;
        }

        if (x0 > x1)
        {
            float tmp2 = x0; x0 = x1; x1 = tmp2;
            float tmp3 = y0; y0 = y1; y1 = tmp3;
        }

        float dx = x1 - x0;
        float dy = y1 - y0;
        float gradient;
        if (dx == 0.0f)
            gradient = 1.0f;
        else
            gradient = dy / dx;

        // handle first endpoint
        float xend = (float)Math.Round(x0);
        float yend = y0 + gradient * (xend - x0);
        float xgap = rfpart(x0 + 0.5f);
        float xpxl1 = xend; // this will be used in the main loop
        float ypxl1 = ipart(yend);

        if (steep)
        {
            color.a = rfpart(yend) * xgap;
            rasterize_point(ypxl1, xpxl1, color);
            color.a = fpart(yend) * xgap;
            rasterize_point(ypxl1 + 1, xpxl1, color);
        }
        else
        {
            color.a = rfpart(yend) * xgap;
            rasterize_point(xpxl1, ypxl1, color);
            color.a = fpart(yend) * xgap;
            rasterize_point(xpxl1, ypxl1 + 1, color);
        }

        float intery = yend + gradient; // first y-intersection for the main loop

        // handle first endpoint
        xend = (float)Math.Round(x1);
        yend = y1 + gradient * (xend - x1);
        xgap = fpart(x1 + 0.5f);
        float xpxl2 = xend; // this will be used in the main loop
        float ypxl2 = ipart(yend);
        if (steep)
        {
            color.a = rfpart(yend) * xgap;
            rasterize_point(ypxl2, xpxl2, color);
            color.a = fpart(yend) * xgap;
            rasterize_point(ypxl2 + 1, xpxl2, color);
        }
        else
        {
            color.a = rfpart(yend) * xgap;
            rasterize_point(xpxl2, ypxl2, color);
            color.a = fpart(yend) * xgap;
            rasterize_point(xpxl2, ypxl2 + 1, color);
        }

        if (steep)
        {
            for (float x = xpxl1 + 1; x <= xpxl2 - 1 * sample_rate; ++x)
            {
                color.a = rfpart(intery);
                rasterize_point(ipart(intery), x, color);
                color.a = fpart(intery);
                rasterize_point(ipart(intery) + 1, x, color);
                intery += gradient;
            }
        }
        else
        {
            for (float x = xpxl1 + 1; x <= xpxl2 - 1 * sample_rate; ++x)
            {
                color.a = rfpart(intery);
                rasterize_point(x, ipart(intery), color);
                color.a = fpart(intery);
                rasterize_point(x, ipart(intery) + 1, color);
                intery += gradient;
            }
        }
    }


}
