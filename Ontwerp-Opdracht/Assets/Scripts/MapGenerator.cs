using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //om te bepalen als de resultaten in black/white noise moet zijn of in kleur
    public enum DrawMode {noiseMap, colorMap, Mesh}
    public DrawMode drawMode;

    const int mapChunkSize = 241;
    [Range(0, 6)]
    public int levelOfDetail;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];

        for(int y = octaves; y < mapChunkSize; y++) 
        { 
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i < regions.Length; i++)
                {
                    // om color op het map te plaatsen
                    if(currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            } 
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.noiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(drawMode == DrawMode.colorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
    }

    private void OnValidate()
    {
        //lacunarity mag niet onder 1
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        //octaves mag niet onder 0
        if(octaves < 0)
        {
            octaves = 0;
        }
    }
    //Om terrain te maken
    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}
