using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetPixalll : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D texture;
    private KMeansResults result;
    private Color[] pixels;
    public GameObject[] clusters;

    void Start()
    {
        pixels = texture.GetPixels(0);        
        result = KMeans.Cluster(pixels, 6, 1000, 101);
        Debug.Log(result.clusters[0].Length);
        Debug.Log(result.clusters[1].Length);
        int[][] resultcluster = result.clusters;
        Array.Sort(resultcluster,delegate(int[] x, int[] y) { return x.Length.CompareTo(y.Length); });
        
        Debug.Log(result.clusters[0].Length);
        Debug.Log(result.clusters[1].Length);
        for (int i = 0; i < result.clusters.Length; i++)
         {
            CreateTexture(i, clusters[i]);
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTexture(int index, GameObject gameObject)
    {
        Color[] temp = pixels;
        Texture2D newtexture = new Texture2D(texture.width, texture.height);

        for(int i = 0; i < temp.Length; i++)
        {
            temp[i].a = 0;
        }
        for (int i = 0; i < result.clusters[index].Length; i++)
        {

            temp[result.clusters[index][i]].a = 1;

        }
        newtexture.SetPixels(temp, 0);
        newtexture.Apply();
        Sprite mySprite = Sprite.Create(newtexture, new Rect(0.0f, 0.0f, newtexture.width, newtexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        gameObject.GetComponent<SpriteRenderer>().sprite = mySprite;
        
        
    }

    


}

