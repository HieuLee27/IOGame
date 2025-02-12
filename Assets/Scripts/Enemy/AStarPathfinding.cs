using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    public int gridWidth = 10, gridHeight = 10;
    public LayerMask obstacleLayer; // Layer chứa chướng ngại vật

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
    {
        List<Node> openList = new List<Node>();
        HashSet<Vector2Int> closedList = new HashSet<Vector2Int>();

        Node startNode = new Node(start, 0, GetHeuristic(start, target), null);
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => a.fCost.CompareTo(b.fCost)); // Sắp xếp theo F-cost
            Node currentNode = openList[0];

            if (currentNode.position == target)
                return RetracePath(currentNode);

            openList.Remove(currentNode);
            closedList.Add(currentNode.position);

            foreach (Vector2Int neighbor in GetNeighbors(currentNode.position))
            {
                if (closedList.Contains(neighbor) || IsObstacle(neighbor))
                    continue;

                int newGCost = currentNode.gCost + 1;
                Node neighborNode = new Node(neighbor, newGCost, GetHeuristic(neighbor, target), currentNode);

                Node existingNode = openList.Find(n => n.position == neighbor);
                if (existingNode == null || newGCost < existingNode.gCost)
                {
                    openList.Add(neighborNode);
                }
            }
        }
        return new List<Vector2Int>(); // Trả về đường đi rỗng nếu không tìm thấy đường
    }

    private List<Vector2Int> RetracePath(Node node)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        while (node != null)
        {
            path.Add(node.position);
            node = node.parent;
        }
        path.Reverse();
        return path;
    }

    private int GetHeuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y); // Heuristic Manhattan
    }

    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>
        {
            position + Vector2Int.up,
            position + Vector2Int.down,
            position + Vector2Int.left,
            position + Vector2Int.right
        };
        return neighbors;
    }

    private bool IsObstacle(Vector2Int position)
    {
        return Physics2D.OverlapCircle((Vector2)position, 0.1f, obstacleLayer) != null;
    }
}
