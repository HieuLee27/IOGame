using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MonsterSpawner : MonoBehaviour
{
    public List<SpawnerConfig> customSpawner;
    private SpawnerConfig selectedSpawner;
    public BSPMapGenerator roomMap;
    private List<RectInt> rooms;

    private void Start()
    {
        selectedSpawner = customSpawner[Random.Range(0, customSpawner.Count)];
        rooms = roomMap.roomEnemy;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1); //Wait for the map to be generated
        Debug.Log("Spawn Enemy");
        for(int i = 1; i < rooms.Count; i++) //Spawn normal enemies in all rooms except the first one
        {
            int typeOfEnemy = Random.Range(0, selectedSpawner.enemyPrefabs.Count);
            for (int x = rooms[i].x + 1; x < rooms[i].x + rooms[i].width - 1; x++)
            {
                for (int y = rooms[i].y + 1; y < rooms[i].y + rooms[i].height - 1; y++)
                {
                    if (Random.Range(0, 100) < 10)
                    {
                        Instantiate(selectedSpawner.enemyPrefabs[typeOfEnemy], new Vector3(x, y, y) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    }
                }
            }
        }

        Instantiate(selectedSpawner.bosses, new Vector3(roomMap.roomBoss.center.x, roomMap.roomBoss.center.y, roomMap.roomBoss.center.y) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity); //Spawn boss in the last room
    }

}
