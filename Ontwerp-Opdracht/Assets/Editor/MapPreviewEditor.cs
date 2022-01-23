using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//zonder dit wordt het knopje niet in inspector gezien
[CustomEditor(typeof(MapPreview))]
public class MapPreviewEditor : Editor
{
    //Make button to generate map in editor
    public override void OnInspectorGUI()
    {
        MapPreview mapPreview = (MapPreview)target;

        if (DrawDefaultInspector())
        {
            if (mapPreview.autoUpdate) 
            {
                mapPreview.DrawMapInEditor();
            }
        }


        if (GUILayout.Button("Generate"))
        {
            mapPreview.DrawMapInEditor();
        }
    }
}
