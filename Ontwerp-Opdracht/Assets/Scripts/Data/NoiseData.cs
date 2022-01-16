using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute()]
public class NoiseData : ScriptableObject
{
    public Noise.NormalizeMode normalizeMode;

    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;


    private void OnValidate()
    {
        //lacunarity mag niet onder 1
        if(lacunarity< 1)
        {
            lacunarity = 1;
        }
        //octaves mag niet onder 0
        if (octaves < 0)
        {
            octaves = 0;
        }   
    }

}
