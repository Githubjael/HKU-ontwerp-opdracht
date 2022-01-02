using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapGeneratorEditor : Editor
{
    //Make button to generate map in editor
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}
