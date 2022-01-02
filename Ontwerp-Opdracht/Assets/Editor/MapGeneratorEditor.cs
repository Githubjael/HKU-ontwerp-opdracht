using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//zonder dit wordt het knopje niet in inspector gezien
[CustomEditor(typeof(MapGenerator))]
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
