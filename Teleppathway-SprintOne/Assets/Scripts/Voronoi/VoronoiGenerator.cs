using System.Collections.Generic;
using UnityEngine;

public static class VoronoiGenerator
{
    //Give seeds, number, and max number
    //seed, (meshSize / pointSpacing) * (meshSize / pointSpacing), meshSize
    public static List<Vector2> GetVector2Points(int seed, int number, int max)
    {
        UnityEngine.Random.InitState(seed);
        var points = new List<Vector2>();
        for (int i = 0; i < number; i++)
        {
            points.Add(new Vector2(UnityEngine.Random.Range(0, max), UnityEngine.Random.Range(0, max)));
        }
        //Jennifer TODO add specified points


        return points;
    }

    public static List<Vector2> GetVector2Points_Jennifer(int seed, int number, int max)
    {
        var points = new List<Vector2>();
        //Jennifer TODO add specified points


        return points;
    }

}
