using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGeneratorPreview))]
public class MapGeneratorPreviewEditor : Editor
{
    //Jennifer
    public void generate_Jennifer()
    {
        MapGeneratorPreview generator = (MapGeneratorPreview)target;
        if (Voronoi3DProperty.clickKgenerate)
        {
            generator.GenerateMap();
            Voronoi3DProperty.clickKgenerate = false;
        }
    }
    public override void OnInspectorGUI()
    {
        MapGeneratorPreview generator = (MapGeneratorPreview)target;

        if (DrawDefaultInspector())
        {
            if (generator.autoUpdate)
            {
                generator.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            generator.GenerateMap();
        }
    }
}