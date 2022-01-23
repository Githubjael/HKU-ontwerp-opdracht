using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPreview : MonoBehaviour
{
    // get the Renderer component of the plane
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public enum DrawMode { noiseMap, Mesh, FallOffMap }
    public DrawMode drawMode;

    public MeshSettings meshSettings;
    public HeightMapSettings heightMapSettings;
    public TextureData textureData;

    public Material terrainMaterial;

    [Range(0, MeshSettings.numSupportedLODs - 1)]
    public int editorPreviewLOD;
    public bool autoUpdate;

    
    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial);
    }

    public void DrawMapInEditor()
    {
        textureData.ApplyToMaterial(terrainMaterial);
        textureData.UpdateMeshHeights(terrainMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight);
        HeightMap HeightMap = HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerline,
            meshSettings.numVertsPerline, heightMapSettings, Vector2.zero);

        if (drawMode == DrawMode.noiseMap)
        {
            DrawTexture(TextureGenerator.TextureFromHeightMap(HeightMap));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            DrawMesh(MeshGenerator.GenerateTerrainMesh(HeightMap.values, meshSettings, editorPreviewLOD));
        }
        else if (drawMode == DrawMode.FallOffMap)
        {
            DrawTexture(TextureGenerator.TextureFromHeightMap(new HeightMap(FallOffGenerator.GenerateFallOffMap(meshSettings.numVertsPerline), 0, 1)));
        }
    }

    public void DrawTexture(Texture2D texture)
    {
        //use this instead to see results in the editor instead of only at runtime
        textureRender.sharedMaterial.mainTexture = texture;
        //set the plane to the same size as the map 
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height) / 10f;

        textureRender.gameObject.SetActive(true);
        meshFilter.gameObject.SetActive(false);
    }

    public void DrawMesh(MeshData meshData)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();

        textureRender.gameObject.SetActive(false);
        meshFilter.gameObject.SetActive(true);
    }
    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            DrawMapInEditor();
        }
    }

    private void OnValidate()
    {
        if (meshSettings != null)
        {
            meshSettings.OnValuesUpdated -= OnValuesUpdated;
            meshSettings.OnValuesUpdated += OnValuesUpdated;
        }
        if (heightMapSettings != null)
        {
            meshSettings.OnValuesUpdated -= OnValuesUpdated;
            heightMapSettings.OnValuesUpdated += OnValuesUpdated;
        }

        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnTextureValuesUpdated;
            textureData.OnValuesUpdated += OnTextureValuesUpdated;
        }
    }
}
