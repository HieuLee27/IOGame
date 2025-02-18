using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public CinemachineVirtualCamera virtualCamera;
    public float spawnDistance = 5f;
    public float bufferZone = 2f;
    public float spawnInterval = 3f; // Thời gian giữa mỗi lần spawn

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine() //Hàm spawn enemy
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy() //Hàm tạo vị trí spawn enemy
    {
        Vector3 spawnPosition = GetSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetSpawnPosition() //Hàm lấy vị trí spawn enemy
    {
        Camera cam = Camera.main;
        Vector3 camPos = cam.transform.position;

        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        float x = (Random.value > 0.5f) ? camPos.x + camWidth / 2 + bufferZone : camPos.x - camWidth / 2 - bufferZone;
        float y = (Random.value > 0.5f) ? camPos.y + camHeight / 2 + bufferZone : camPos.y - camHeight / 2 - bufferZone;

        return new Vector3(x, y, 0);
    }
}

