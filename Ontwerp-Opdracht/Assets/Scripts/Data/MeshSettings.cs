using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MeshSettings : UpdatableData
{
    public const int numSupportedLODs = 5;
    public const int numsupportedChunkSizes = 9;
    public const int numsupportedFlatShadedChunkSizes = 3;
    public static readonly int[] supportedChunkSizes = { 48, 72, 96, 120, 144, 192, 216, 240 };
    
    public float meshScale = 2.5f;
    public bool useFlatShading;
    
    [Range(0, numsupportedChunkSizes - 1)]
    public int chunkSizeIndex;

    [Range(0, numsupportedFlatShadedChunkSizes - 1)]
    public int flatShadedChunkSizesIndex;
    
    //aantal vertices per line van mesh rendered at LOD = 0. Inclusief de 2 twee extra vertices dat uitgesloten worden avn de finale mesh
    //maar wordt gebruikt voor normals
    public int numVertsPerline
    {
        get
        {
            return supportedChunkSizes[(useFlatShading) ? flatShadedChunkSizesIndex : chunkSizeIndex] + 1;
        }
    }

    public float meshWorldSize
    {
        get
        {
            return (numVertsPerline - 3) * meshScale;
        }
    }
}
