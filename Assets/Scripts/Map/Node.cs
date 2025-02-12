using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int position;
    public int gCost, hCost, fCost;
    public Node parent;

    public Node(Vector2Int pos, int g, int h, Node p)
    {
        position = pos;
        gCost = g;
        hCost = h;
        fCost = g + h;
        parent = p;
    }
}

