using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Voronoi3DProperty //: MonoBehaviour
{
    public static List<Vector2> flowersPos;
    public static List<Vector2> meansPos;
    public static bool clickKgenerate;
    public static void MapProperty(List<Vector2> _flowersPos, List<Vector2> _meansPos)
    {
        flowersPos = _flowersPos;
        meansPos = _meansPos;
        clickKgenerate = true;
    }
}
