using System.Collections.Generic;
using UnityEngine;

public static class VoronoiGenerator
{
    //Give seeds, number, and max number
    //seed, (meshSize / pointSpacing) * (meshSize / pointSpacing), meshSize
    public static List<Vector2> GetVector2Points(int seed, int number, int max)
    {
        //Debug.Log(seed);
        //Debug.Log(number);
        //Debug.Log(max);
        UnityEngine.Random.InitState(seed);
        var points = new List<Vector2>();
        for (int i = 0; i < number; i++)
        {
            points.Add(new Vector2(UnityEngine.Random.Range(0, max), UnityEngine.Random.Range(0, max)));
        }

        //Jennifer for our points


        return points;
    }



}
