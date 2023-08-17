using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public int depth = 20;

    public float scale = 20f;
    private float CalculateHeights(int x, int y)
    {
        float xCord = (float)x/width * scale;
        float yCord = (float)y/height * scale;
        return Mathf.PerlinNoise(xCord, yCord);
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0;x<width; x++)
        {
            for (int y = 0;y< height; y++)
            {
                heights[x, y] = CalculateHeights(x,y);
            }
        }
        return heights;
    }
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0,0,GenerateHeights());
        return terrainData;
    }
    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    // Update is called once per frame
}
