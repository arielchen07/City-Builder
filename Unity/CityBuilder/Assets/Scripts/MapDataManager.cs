using CoreFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MapDataManager : MonoBehaviour
{
    public SaveFile saveSystem;
    public InventoryList inventory;
    public PlacementSystem placementSystem;
    public InputManager inputManager;
    public HashSet<string> TILES = new HashSet<string>(new string[] { "grass1", "grass2", "grass3" });
    public HashSet<string> DECOR = new HashSet<string>(new string[] { "tree1", "tree2", "treeWall1" });
    public int WIDTH = 10;
    public int LENGTH = 10;
    public float WATER_PERCENTAGE = 30f;
    public int TREES_PER_TILE = 5;
    private void Start()
    {
        print("At start: userID = " + GlobalVariables.UserID + " mapID = " + GlobalVariables.MapID);
        if (!string.IsNullOrEmpty(GlobalVariables.MapID))
        {
            if (GlobalVariables.IsNewUser)
            {
                //LoadGameMapServer(); // will be changed to Generate game map
                //SaveGameMapServer(GlobalVariables.MapID);
                GenerateGameMap();
            }
            else
            {
                LoadGameMapServer(GlobalVariables.MapID);
            }
        }
    }
    private void Update()
    {
        // Key press for save/load the game, will be replaced by click button in UI at later stage
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SaveGameMapLocal();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            LoadGameMapLocal();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SaveGameMapServer();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            LoadGameMapServer();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            RemoveStructureObjs();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ClearGameMap();
        }
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    GenerateGameMap();
        //}
    }
    public void RemoveStructureObjs()
    {
        // Remove all building and roads on game map
        foreach (var item in GameObject.FindObjectsOfType<PlaceableObject>())
        {
            Destroy(item.gameObject);
        }
        foreach (var item in GameObject.FindObjectsOfType<Road>())
        {
            Destroy(item.gameObject);
        }
        foreach (var item in GameObject.FindObjectsOfType<MapTile>())
        {
            item.isOccupied = false;
            item.placedObject = null;
        }
    }
    public void ClearGameMap()
    {
        RemoveStructureObjs();

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            Destroy(tile);
        }

        GameObject[] decorations = GameObject.FindGameObjectsWithTag("Decoration");
        foreach (GameObject decor in decorations)
        {
            Destroy(decor);
        }
    }

    public void GenerateGameMap()
    {
        ClearGameMap();
        List<GameObject> tileObjects = GetObjectsByName(inventory.inventoryLst, TILES);
        List<GameObject> decors = GetObjectsByName(inventory.inventoryLst, DECOR);

        float[,] tileGrid = GeneratePerlinNoiseGrid(WIDTH + 1, LENGTH + 1, 3); /// the last parameter is the scale, the higher it is, the more "zoomed out" and less detailed it is
        float[,] decorGrid = GeneratePerlinNoiseGrid(WIDTH + 1, LENGTH + 1, 6);

        for (int x = 0; x < WIDTH + 1; x++)
        {
            for (int z = 0; z < LENGTH + 1; z++)
            {
                GameObject tile = CreateTile(x, z, tileGrid, tileObjects);
                CreateDecorations(x, z, decorGrid, decors, tile);
            }
        }
    }

    private List<GameObject> GetObjectsByName(List<GameObject> objects, HashSet<string> names)
    {
        return objects.Where(obj => names.Contains(obj.name)).ToList();
    }

    private GameObject CreateTile(int x, int z, float[,] grid, List<GameObject> objects)
    {
        Transform landTransform = GameObject.Find("Land").transform;
        float yHeight = landTransform.position.y;

        int perlin = (int)Math.Floor((grid[x, z] / (1 - (WATER_PERCENTAGE / 100f))) * (float)(objects.Count));

        if (perlin >= 0 && perlin < objects.Count)
        {
            Vector3 position = new Vector3(x - WIDTH / 2, yHeight, z - LENGTH / 2); // center on origin
            GameObject tile = Instantiate(objects[perlin], position, Quaternion.identity);
            tile.transform.SetParent(landTransform, true);
            return tile;
        }

        return null;
    }

    private void CreateDecorations(int x, int z, float[,] grid, List<GameObject> decors, GameObject tile)
    {
        if (tile == null) return;

        for (int c = 0; c < TREES_PER_TILE; c++)
        {
            int perlin = (int)Math.Floor(grid[x, z] * 100);
            if (perlin < 35)
            {
                Vector3 randomOffset = new Vector3(UnityEngine.Random.value - 0.5f, 0, UnityEngine.Random.value - 0.5f);
                Vector3 position = tile.transform.position + randomOffset;
                GameObject decoration = Instantiate(decors[UnityEngine.Random.Range(0, decors.Count)], position, Quaternion.identity);
                decoration.tag = "Decoration";
                tile.GetComponent<MapTile>().isOccupied = true;
            }
        }
    }

    private float[,] GeneratePerlinNoiseGrid(int width, int length, float scale)
    {
        float[,] grid = new float[width, length];
        float offset = UnityEngine.Random.value * 100; // the offset is added since perlin noise is deterministic and would produce the same map for everyone otherwise

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                float xCoord = x / scale + offset;
                float yCoord = y / scale + offset;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                grid[x, y] = sample;
            }
        }

        return grid;
    }

    public void SaveGameMapServer(string mapID = "6553c6411cbcbe4fb138d3bb")
    {
        var mapDataJson = SerializeAllGameObjects();
        saveSystem.SaveDataServer(mapID, mapDataJson);
    }
    public void SaveGameMapLocal()
    {
        var mapDataJson = SerializeAllGameObjects();
        saveSystem.SaveDataLocal(mapDataJson);
    }
    public string SerializeAllGameObjects()
    {
        MapSerialization mapObjs = new MapSerialization();
        PlaceableObject[] placeableObjects = GameObject.FindObjectsOfType<PlaceableObject>();
        foreach (PlaceableObject rb in placeableObjects)
        {
            if (
                rb.gameObject
                != placementSystem.GetComponent<PlacementSystem>().GetCurrentlyPlacing()
            )
            {
                mapObjs.AddStructure(
                    rb.name,
                    rb.transform.position,
                    rb.transform.rotation.eulerAngles
                );
            }
        }
        Road[] roads = GameObject.FindObjectsOfType<Road>();
        foreach (Road rb in roads)
        {
            mapObjs.AddStructure(rb.name, rb.transform.position, rb.transform.rotation.eulerAngles);
        }
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            mapObjs.AddTile(tile.name, tile.transform.position, tile.transform.rotation.eulerAngles, tile.GetComponent<MapTile>().isOccupied);
        }

        GameObject[] decorations = GameObject.FindGameObjectsWithTag("Decoration");
        foreach (GameObject obj in decorations)
        {
            if (obj.name.EndsWith("(Clone)"))
            {
                obj.name = obj.name.Substring(0, obj.name.Length - "(Clone)".Length);
            }
            mapObjs.AddDecor(obj.name, obj.transform.position, obj.transform.rotation.eulerAngles);
        }
        var mapDataJson = JsonUtility.ToJson(mapObjs);
        return mapDataJson;
    }
    public void LoadGameMapServer(string mapID = "6553c6411cbcbe4fb138d3bb")
    {
        saveSystem.LoadDataServer(mapID);
    }
    public void LoadGameMapLocal()
    {
        var mapDataJson = saveSystem.LoadDataLocal();
        ReDrawGameMap(mapDataJson);
    }
    public void ReDrawGameMap(string mapDataJson)
    {
        ClearGameMap();

        if (String.IsNullOrEmpty(mapDataJson))
            return;
        MapSerialization mapObjs = JsonUtility.FromJson<MapSerialization>(
            mapDataJson
        );

        DrawTilesFromJson(mapObjs);
        DrawStructureObjects(mapObjs);
        DrawDecorFromJson(mapObjs);
    }
    public void DrawStructureObjects(MapSerialization mapObjs)
    {
        // Redraw buildings and roads
        foreach (var structure in mapObjs.structureObjData)
        {
            foreach (var placeableObj in inventory.inventoryLst)
            {
                if (structure.name.IndexOf(placeableObj.name) != -1)
                {
                    if (structure.name.IndexOf("Road") != -1)
                    {
                        // Create road object
                        GameObject roadObj = Instantiate(placeableObj);
                        roadObj.transform.position = structure.position.GetValue();
                    }
                    else
                    {
                        // Create building object
                        GameObject building = Instantiate(
                            placeableObj,
                            structure.position.GetValue(),
                            Quaternion.Euler(structure.rotation.GetValue())
                        );
                        // Update colliding tiles and building state
                        building.transform.parent = null;
                        foreach (
                            GameObject tile in building
                                .GetComponent<PlaceableObject>()
                                .GetCollidingTiles()
                        )
                        {
                            tile.GetComponent<MapTile>().isOccupied = true;
                            tile.GetComponent<MapTile>().placedObject = building;
                        }
                        inputManager.placementLayermask =
                            LayerMask.GetMask("Ground") | LayerMask.GetMask("Foreground");
                        building.GetComponent<PlaceableObject>().isHovering = false;
                        building.GetComponent<PlaceableObject>().hasBeenPlaced = true;
                    }
                    break;
                }
            }
        }
    }
    public void DrawTilesFromJson(MapSerialization mapObjs)
    {
        Transform landTransform = GameObject.Find("Land").transform;

        foreach (var tile in mapObjs.tileData)
        {
            foreach (var tileObj in inventory.inventoryLst)
            {
                if (tile.name.IndexOf(tileObj.name) != -1)
                {
                    GameObject tileInst = Instantiate(tileObj, tile.position.GetValue(), Quaternion.Euler(tile.rotation.GetValue()));
                    tileInst.transform.SetParent(landTransform, true);
                    tileInst.GetComponent<MapTile>().isOccupied = tile.isOccupied;
                }
            }
        }
    }
    public void DrawDecorFromJson(MapSerialization mapObjs)
    {
        foreach (var decor in mapObjs.decorData)
        {
            foreach (var decorObj in inventory.inventoryLst)
            {
                if (decor.name.IndexOf(decorObj.name) != -1)
                {
                    if (decor.name.EndsWith("(Clone)"))
                    {
                        decor.name = decor.name.Substring(0, decor.name.Length - "(Clone)".Length);
                    }
                    if (DECOR.Contains(decor.name))
                    {
                        GameObject decoration = Instantiate(decorObj);
                        decoration.transform.position = decor.position.GetValue();
                        decoration.tag = "Decoration";
                        Transform landTransform = GameObject.Find("Land").transform;
                        decoration.transform.SetParent(landTransform, true);
                    }
                }
            }
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    // 65517d52b753aa75060ea633
}