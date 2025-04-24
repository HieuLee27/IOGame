using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MapConfig", menuName = "ChillVerse/MapConfig", order = 1)]
public class MapConfig : ScriptableObject
{
    public int width = 50;
    public int height = 50;
    public int minRoomSize = 6;

    [Header("Prefabs")]
    public TileBase wallTile;
    public TileBase[] floorTile;

    [Space]
    [Header("TileBases")]
    public GameObject chestPrefab;
    public GameObject[] bushPrefabs;
    public GameObject[] treePrefabs;
    public GameObject[] rockPrefabs;
    public GameObject teleportationAltar;

    [Space]
    [Header("Percen")]
    public int SpawnBushPercent = 10;
    public int SpawnTreePercent = 10;
    public int SpawnRockPercent = 10;
}
