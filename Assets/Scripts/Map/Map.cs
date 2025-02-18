using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Tilemap firstFloor;
    [SerializeField] private Tilemap secondFloor;

    [Header("Square components")]
    [SerializeField] private GameObject square;

    [Space]
    [Header("Wall components")]
    [SerializeField] private TileBase wallX;
    [SerializeField] private TileBase wallY;
    [SerializeField] private TileBase corner;

    [Space]
    [Header("Tree components")]
    [SerializeField] private GameObject[] treeArray;
    [SerializeField] private int percenOfTree;

    [Space]
    [Header("Size map")]
    public int maxX, maxY;
    internal HashSet<Vector2> blankPos;

    internal static Map mapInstance;

    private void Start()
    {
        blankPos = new HashSet<Vector2>();
        PainMap();
        mapInstance = this;
    }

    private void PaintWall()
    {
        for(int i = -maxX / 2; i < maxX; i++)
        {
            for (int y = -maxY / 2; y < maxY; y++)
            {
                if((i == -maxX / 2 || i == maxX / 2) && 
                    (y > -maxY/2 && y < maxY / 2))
                {
                    Vector3Int position = new(i, y, 0);
                    firstFloor.SetTile(position, wallY);
                }
                else if((y == -maxY/2 || y == maxY/2) && 
                    (i < maxX/2  && i > -maxX / 2))
                {
                    Vector3Int position = new(i, y, 0);
                    firstFloor.SetTile(position, wallX);
                }
                else if((i == -maxX /2 || i == maxX / 2) && 
                    (y == -maxY/2 || y == maxY / 2))
                {
                    Vector3Int position = new(i, y, 0);
                    secondFloor.SetTile(position, corner);
                }
            }
        }
    }

    private void PainTree()
    {
        for (int x = -Math.Abs(maxX / 2 - 1); x < maxX / 2; x++)
        {
            for (int y = -Math.Abs(maxY / 2 - 1); y < maxY / 2; y++)
            {
                int i = UnityEngine.Random.Range(0, percenOfTree);
                if(i <= 1)
                {
                    int t = UnityEngine.Random.Range(0, treeArray.Length);
                    Vector3 position = new(x, y, y);
                    if (treeArray[t].gameObject.CompareTag("Tree"))
                    {
                        Instantiate(treeArray[t], position + 
                            new Vector3(0.5f, 0.5f, 0.2f), 
                            transform.rotation);
                    }
                    else if(treeArray[t].gameObject.CompareTag("Wood"))
                    {
                        Instantiate(treeArray[t], position + 
                            new Vector3(0.5f, 0.5f, 0.5f), 
                            transform.rotation);
                    }
                }
                else
                {
                    blankPos.Add(new Vector2(x + 0.5f, y + 0.5f));
                }
            }
        }
    }

    public void PainMap()
    {
        GameObject[] listTree = GameObject.FindGameObjectsWithTag("Tree");
        GameObject[] listWood = GameObject.FindGameObjectsWithTag("Wood");
        foreach (GameObject tree in listTree)
        {
            DestroyImmediate(tree);
        }
        foreach (GameObject wood in listWood)
        {
            DestroyImmediate(wood);
        }
        ClearMap();
        PaintWall();
        PainTree();
    }

    private void PainFloor()
    {
        DestroyImmediate(GameObject.FindWithTag("Floor"));
        square.transform.localScale = new Vector2(maxX + 5, maxY + 10);
        Instantiate(square, new Vector3(0, 0, maxY + 10), 
            transform.rotation);
    }

    private void ClearMap()
    {
        PainFloor();
        firstFloor.ClearAllTiles();
        secondFloor.ClearAllTiles();
    }

    
}
