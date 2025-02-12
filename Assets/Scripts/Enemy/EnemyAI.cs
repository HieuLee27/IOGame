using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float updatePathTime = 0.5f;

    private AStarPathfinding pathfinding;
    private List<Vector2Int> path;
    private int pathIndex = 0;

    void Start()
    {
        pathfinding = GetComponent<AStarPathfinding>();
        StartCoroutine(UpdatePathRoutine());
    }

    IEnumerator UpdatePathRoutine()
    {
        while (true)
        {
            UpdatePath();
            yield return new WaitForSeconds(updatePathTime);
        }
    }

    void UpdatePath()
    {
        Vector2Int start = Vector2Int.RoundToInt(transform.position);
        Vector2Int target = Vector2Int.RoundToInt(player.position);
        path = pathfinding.FindPath(start, target);
        pathIndex = 0;
    }

    void Update()
    {
        if (path == null || pathIndex >= path.Count) return;

        Vector3 targetPos = new Vector3(path[pathIndex].x, path[pathIndex].y, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            pathIndex++;
    }
}
