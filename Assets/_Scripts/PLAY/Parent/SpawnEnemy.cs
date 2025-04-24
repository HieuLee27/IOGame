using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của enemy
    public CinemachineVirtualCamera virtualCamera; // Camera ảo để lấy vị trí camera
    public float spawnDistance = 5f; // Khoảng cách spawn enemy so với camera
    public float bufferZone = 2f; // Khoảng cách buffer giữa vị trí spawn và biên camera
    public float spawnInterval = 3f; // Thời gian giữa mỗi lần spawn

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine()); // Bắt đầu spawn enemy
    }

    IEnumerator SpawnEnemyRoutine() //Hàm spawn enemy
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Chờ 3 giây
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
        Camera cam = Camera.main; // Lấy camera chính
        Vector3 camPos = cam.transform.position;

        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        // Random vị trí x spawn enemy
        float x = (Random.value > 0.5f) ? camPos.x + camWidth / 2 + bufferZone : camPos.x - camWidth / 2 - bufferZone;
        // Random vị trí y spawn enemy
        float y = (Random.value > 0.5f) ? camPos.y + camHeight / 2 + bufferZone : camPos.y - camHeight / 2 - bufferZone; 

        return new Vector3(x, y, 0);
    }
}

