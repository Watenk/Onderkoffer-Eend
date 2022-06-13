using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSettings : MonoBehaviour
{
    Terrain terrain;

    private void Start()
    {
        terrain = Terrain.activeTerrain;
        terrain.detailObjectDistance = 500;
    }
}
