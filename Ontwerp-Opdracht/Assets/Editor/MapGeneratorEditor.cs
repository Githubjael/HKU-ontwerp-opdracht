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

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate) 
            {
                mapGen.GenerateMap();
            }
        }


        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}
