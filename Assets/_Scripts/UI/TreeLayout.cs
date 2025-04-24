using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TreeLayoutGroup : LayoutGroup
{
    public float spacingX = 100f; // Khoảng cách ngang giữa các node
    public float spacingY = 150f; // Khoảng cách dọc giữa các cấp độ

    public override void CalculateLayoutInputHorizontal() { }
    public override void CalculateLayoutInputVertical() { }

    public override void SetLayoutHorizontal()
    {
        ArrangeNodes(transform, Vector2.zero);
    }

    public override void SetLayoutVertical()
    {
        ArrangeNodes(transform, Vector2.zero);
    }

    private void ArrangeNodes(Transform parent, Vector2 position)
    {
        int childCount = parent.childCount;
        if (childCount == 0) return;

        // Tính khoảng cách cần chia đều giữa các node con
        float startX = -((childCount - 1) * spacingX) / 2;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            Vector2 childPos = new Vector2(startX + (i * spacingX), position.y - spacingY);
            child.localPosition = childPos;

            // Gọi đệ quy để sắp xếp tiếp các node con của nó
            ArrangeNodes(child, childPos);
        }
    }
}
