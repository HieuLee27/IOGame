using UnityEngine;
using System.Collections.Generic;

public class TreeAutoConnector : MonoBehaviour
{
    private List<LineRenderer> lines = new List<LineRenderer>();
    private List<(Transform parent, Transform child)> connections = new List<(Transform, Transform)>();

    public float lineWidth = 5f; // Độ dày đường nối
    public Color lineColor = Color.black; // Màu đường nối

    void Start()
    {
        DrawConnections();
    }

    void DrawConnections()
    {
        // Xóa tất cả LineRenderer cũ trước khi vẽ lại
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
        connections.Clear();

        // Duyệt toàn bộ cây để lưu danh sách kết nối cha - con
        FindConnections(transform);

        // Tạo LineRenderer cho từng kết nối
        foreach (var (parent, child) in connections)
        {
            GameObject lineObj = new GameObject("Line");
            LineRenderer line = lineObj.AddComponent<LineRenderer>();
            line.transform.SetParent(this.transform); // Gắn vào root để dễ quản lý
            line.positionCount = 2;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = lineColor;
            line.endColor = lineColor;
            line.useWorldSpace = true;

            lines.Add(line);
        }
    }

    void FindConnections(Transform parent)
    {
        foreach (Transform child in parent)
        {
            connections.Add((parent, child)); // Lưu node cha - node con
            FindConnections(child); // Đệ quy tìm tiếp các nhánh con
        }
    }

    void Update()
    {
        // Cập nhật lại vị trí đường nối mỗi frame (nếu node di chuyển)
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].SetPosition(0, connections[i].parent.position);
            lines[i].SetPosition(1, connections[i].child.position);
        }
    }
}
