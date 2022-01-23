using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    const float viewerMoveThresholdForChunkUpdate = 25f;
    const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;
    

    public int collidreLODIndex;
    public LODInfo[] detailLevels;

    public MeshSettings meshSettings;
    public HeightMapSettings heightMapSettings;
    public TextureData textureSettings;

    public Transform viewer;
    public Material mapMaterial;

    Vector2 viewerPosition;
    Vector2 oldViewerPosition;
    float meshWorldSize;
    int chunksVisibleInViewDst;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> VisibleTerrainchunks = new List<TerrainChunk>();

    public void Start()
    {
        textureSettings.ApplyToMaterial(mapMaterial);
        textureSettings.UpdateMeshHeights(mapMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight);
        float maxViewDistance = detailLevels[detailLevels.Length - 1].visibleDstThreshold;
        meshWorldSize = meshSettings.meshWorldSize;
        chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDistance / meshWorldSize);

        UpdateVisibleChunks();
    }

    public void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);

        if(viewerPosition != oldViewerPosition)
        {
            foreach(TerrainChunk chunk in VisibleTerrainchunks)
            {
                chunk.UpdateCollisionMesh();
            }
        }

        if((oldViewerPosition - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate)
        {
            oldViewerPosition = viewerPosition;
            UpdateVisibleChunks();
        }
    }

    public void UpdateVisibleChunks()
    {
        HashSet<Vector2> alreadyUpdatedChunkCoords = new HashSet<Vector2>();
        for(int i = VisibleTerrainchunks.Count - 1; i >= 0; i--)
        {
            alreadyUpdatedChunkCoords.Add(VisibleTerrainchunks[i].coord);
            VisibleTerrainchunks[i].updateTerrainChunk();
        }

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / meshWorldSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / meshWorldSize);

        for(int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                if (!alreadyUpdatedChunkCoords.Contains(viewedChunkCoord))
                {
                    if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                    {
                        terrainChunkDictionary[viewedChunkCoord].updateTerrainChunk();
                    }
                    else
                    {
                        TerrainChunk newChunk = new TerrainChunk(viewedChunkCoord, heightMapSettings, meshSettings, detailLevels,
                        collidreLODIndex, transform, viewer, mapMaterial);
                        terrainChunkDictionary.Add(viewedChunkCoord, newChunk);
                        newChunk.onVisibilityChanged += OnTerrainChunkVisibilityChanged;
                        newChunk.Load();
                    }
                }
            }
        }
    }

    void OnTerrainChunkVisibilityChanged(TerrainChunk chunk, bool isVisible)
    {
        if (isVisible)
        {
            VisibleTerrainchunks.Add(chunk);
        }
        else
        {
            VisibleTerrainchunks.Remove(chunk);
        }
    }
}
[System.Serializable]
    public struct LODInfo
    {
        [Range(0 , MeshSettings.numSupportedLODs - 1)]
        public int lod;
        public float visibleDstThreshold;

        public float sqrVisibleDstThreshold
        {
            get
            {
                return visibleDstThreshold * visibleDstThreshold;
            }
        }
    }
