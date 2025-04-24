using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class BSPMapGenerator : MonoBehaviour
{
    public MapConfig mapConfig;
    public Tilemap wallTilemap;
    public Tilemap floorTilemap;
    public Tilemap treeTilemap;

    private List<GameObject> decorList = new(); //List decor prefab

    internal List<RectInt> rooms = new(); //List rooms
    internal RectInt roomBoss = new(); //Boss room
    internal List<RectInt> roomEnemy = new();
    internal List<RectInt> roomChest = new();

    void Start()
    {
        GenerateDungeon();

        SpawnComponent();

        ClassifyRooms();
    }

    void Update()
    {
        //BackFrontEffect();
    }

    #region Pain Map
    void GenerateDungeon() //Create random map
    {
        BSPNode root = new BSPNode(new RectInt(0, 0, mapConfig.width, mapConfig.height));
        root.Split(mapConfig.minRoomSize);
        root.GetRooms(rooms);

        PaintRooms();
        ConnectRooms();
        PaintWalls();
    }

    void PaintRooms() //Paint the rooms
    {
        int roomMargin = 1;
        foreach (var room in rooms)
        {
            for (int x = room.x + roomMargin; x < room.x + room.width - roomMargin; x++)
            {
                for (int y = room.y + roomMargin; y < room.y + room.height - roomMargin; y++)
                {
                    floorTilemap.SetTile(new Vector3Int(x, y, 0), mapConfig.floorTile[Random.Range(0, mapConfig.floorTile.Length)]);
                }
            }
        }
    }

    void PaintWalls() //Paint the walls
    {
        for (int i = 0; i < mapConfig.width; i++)
        {
            for (int j = 0; j < mapConfig.height; j++)
            {
                if (floorTilemap.GetTile(new Vector3Int(i, j, 0)) == null)
                {
                    wallTilemap.SetTile(new Vector3Int(i, j, 0), mapConfig.wallTile);
                }
            }
        }
    }

    void ConnectRooms() //Create corridors between rooms
    {
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            Vector2Int start = Vector2Int.RoundToInt(rooms[i].center);
            Vector2Int end = Vector2Int.RoundToInt(rooms[i + 1].center);
            Vector2Int corner = new Vector2Int(end.x, start.y);

            while (start.x != corner.x)
            {
                start.x += (start.x < corner.x) ? 1 : -1;
                floorTilemap.SetTile(new Vector3Int(start.x, start.y, 0), mapConfig.floorTile[Random.Range(0, mapConfig.floorTile.Length)]);
            }

            while (start.y != end.y)
            {
                start.y += (start.y < end.y) ? 1 : -1;
                floorTilemap.SetTile(new Vector3Int(start.x, start.y, 0), mapConfig.floorTile[Random.Range(0, mapConfig.floorTile.Length)]);
            }
        }
    }
    #endregion

    #region SpawnComponent
    private void SpawnComponent()
    {
        List<Vector2> treesPos = SelectedPositions();
        Spawner(treesPos, mapConfig.SpawnTreePercent, mapConfig.bushPrefabs);

        List<Vector2> bushesPos = SelectedPositions();
        Spawner(bushesPos, mapConfig.SpawnBushPercent, mapConfig.treePrefabs);

        List<Vector2> rocksPos = SelectedPositions();
        Spawner(rocksPos, mapConfig.SpawnRockPercent, mapConfig.rockPrefabs);

        SpawnTeleportAltar();

        SpawnChests();
    }

    private List<Vector2> SelectedPositions() //Create random bushes
    {
        List<Vector2> positions = new();
        for (int i = 0; i < rooms.Count; i++)
        {
            for (int x = rooms[i].x + 1; x < rooms[i].x + rooms[i].width - 1; x++)
            {
                for (int y = rooms[i].y + 1; y < rooms[i].y + rooms[i].height - 1; y++)
                {
                    if (x != rooms[i].center.x || y != rooms[i].center.y) //Position to spawn
                    {
                        positions.Add(new Vector2(x, y));
                    }
                }
            }
        }

        return positions;
    }

    private void Spawner(List<Vector2> positionList, int percent, GameObject[] prefabs)
    {
        for(int i = 0; i < positionList.Count; i++)
        {
            Vector2 currentPos = positionList[i];
            if (Random.Range(0.0f, 1.0f) >= (1 - ((float)percent /100)))
            {
                int countPrefabs = prefabs.Length;
                Vector3 posSpawn = (Vector3)currentPos + new Vector3(0.5f, 0.5f, 0); 

                if (Random.Range(0.0f, 2.0f) > 1.8f)// Cuztomize the rate of spawn
                {
                    float offer = Random.Range(0, 0.1f); //Rate of size
                    GameObject instance = Instantiate(prefabs[Random.Range(0, countPrefabs)], posSpawn, Quaternion.identity);
                    instance.GetComponent<SpriteRenderer>().sortingOrder = -(int)(instance.transform.position.y); //Set the order of the sprite to be in front of the player
                    decorList.Add(instance); //Add the instance to the list

                    if (instance.CompareTag("Tree"))
                    {
                        if (Random.Range(0, 20) == 0)
                        {
                            instance.transform.localScale -= new Vector3(offer, offer, 0);
                        }
                    }
                }
            }
        }
    }

    private void BackFrontEffect()
    {
        foreach (var decor in decorList) //Set the order of the sprite to be in front of the player
        {
            OrderLayerChanging(decor);
        }
    }

    private void OrderLayerChanging(GameObject gameObject)
    {
        GameObject player = GameObject.Find("Player");

        if(gameObject.transform.position.y > player.transform.position.y)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1; //Set the order of the sprite to be in front of the player
        }
        else if (gameObject.transform.position.y < player.transform.position.y)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }

    void SpawnChests() //Create random chests
    {
        if (roomChest != null)
        {
            foreach (var room in roomChest)
            {
                Vector3 chestPos = new Vector3(room.center.x, room.center.y, room.center.y);
                Instantiate(mapConfig.chestPrefab, chestPos, Quaternion.identity);
            }
        }
    }

    private void SpawnTeleportAltar()
    {
        GameObject instanceSpawn1 = Instantiate(mapConfig.teleportationAltar, rooms[rooms.Count -1].center, Quaternion.identity);
    }
    #endregion

    #region BSP
    private void ClassifyRooms() //Classify rooms
    {
        roomBoss = rooms[rooms.Count - 1];
        List<RectInt> totalRoom = rooms.GetRange(1, rooms.Count - 2);
        int numRoomEnemy = Random.Range(5, totalRoom.Count - 1);
        for(int i = 0; i < numRoomEnemy; i++)
        {
            int index = Random.Range(0, totalRoom.Count);
            roomEnemy.Add(totalRoom[index]);
            totalRoom.RemoveAt(index);
        }
        roomChest = totalRoom;
    }

    public class BSPNode
    {
        public RectInt room;
        public BSPNode left;
        public BSPNode right;

        public BSPNode(RectInt rect)
        {
            room = rect;
        }

        public void Split(int minRoomSize) //Split the room into two smaller rooms
        {
            if (room.width <= minRoomSize * 2 && room.height <= minRoomSize * 2)
                return;

            bool splitHorizontal = (room.width > room.height);
            if (room.width == room.height)
                splitHorizontal = Random.Range(0, 2) == 0;

            if (splitHorizontal)
            {
                int split = Random.Range(minRoomSize, room.width - minRoomSize);
                left = new BSPNode(new RectInt(room.x, room.y, split, room.height));
                right = new BSPNode(new RectInt(room.x + split, room.y, room.width - split, room.height));
            }
            else
            {
                int split = Random.Range(minRoomSize, room.height - minRoomSize);
                left = new BSPNode(new RectInt(room.x, room.y, room.width, split));
                right = new BSPNode(new RectInt(room.x, room.y + split, room.width, room.height - split));
            }

            left.Split(minRoomSize);
            right.Split(minRoomSize);
        }

        public void GetRooms(List<RectInt> rooms) //Get all rooms
        {
            if (left == null && right == null)
            {
                rooms.Add(room);
            }
            else
            {
                left?.GetRooms(rooms);
                right?.GetRooms(rooms);
            }
        }
    }
    #endregion
}
