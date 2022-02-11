using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlowersGeneration
{
    public static List<Vector3> points;
    public static List<Vector3> getFlowersPoints(MapGraph map)
    {
        //points = new List<Vector3>();
        var meshData = new MeshData();
        meshData.vertices = new List<Vector3>();
        meshData.indices = new List<int>();

        //Get all vertices
        foreach (var node in map.nodesByCenterPosition.Values)
        {
            meshData.vertices.Add(node.centerPoint);
            var centerIndex = meshData.vertices.Count - 1;
            
        }

        return meshData.vertices;
    }
}
