using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSpawner", menuName = "Spawns/MonsterSpawner")]
public class SpawnerConfig : ScriptableObject
{
    [Header("Enemy Prefabs")]
    public List<GameObject> enemyPrefabs;
    public GameObject bosses;
}
