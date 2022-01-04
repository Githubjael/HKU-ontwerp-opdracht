using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    // get the Renderer component of the plane
    public Renderer textureRender;

    public void DrawTexture(Texture2D texture)
    {
        //use this instead to see results in the editor instead of only at runtime
        textureRender.sharedMaterial.mainTexture = texture;
        //set the plane to the same size as the map 
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}
