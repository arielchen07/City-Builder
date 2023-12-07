using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MapDataManager : MonoBehaviour
{
    public SaveFile saveSystem;
    public InventoryList inventory;
    public DecorationSpawner decorationSpawner;
    public PlacementSystem placementSystem;
    public InputManager inputManager;
    public HashSet<string> TILES = new HashSet<string>(new string[] { "grass1", "grass2", "grass3"});
    public HashSet<string> DECOR = new HashSet<string>(new string[] { "tree1", "tree2", "treeWall1", "rock1", "treeLarge1" });
    public int WIDTH = 10;
    public int LENGTH = 10;
    public float WATER_PERCENTAGE = 30f;
    public int DECORATIONS_PER_TILE = 5;
    float timer = 0;
    public float interval;
    public bool finish_loading_map;
    public GameObject cover;
    public GameObject cloudsOnLoad;
    public bool spawnDecorations;
    private void Start()
    {
        print("At start: userID = " + GlobalVariables.UserID + " mapID = " + GlobalVariables.MapID);
        if (!string.IsNullOrEmpty(GlobalVariables.MapID))
        {
            if (GlobalVariables.IsNewUser)
            {
                GenerateGameMap();
                finish_loading_map = true;
                cover.GetComponent<Animator>().SetTrigger("loadScene");
                cloudsOnLoad.SetActive(true);
                PollutionManager.pollutionManager.UpdateTreeCount();
                PollutionManager.pollutionManager.UpdatePolluters();
                PopulationManager.populationManager.UpdateHouses();
                HarvesterManager.harvesterManager.UpdateHarvester();
            }
            else
            {
                LoadGameMapServer(GlobalVariables.MapID);
            }
        }
    }
    private void Update()
    {
        if (Time.timeSinceLevelLoad > timer && finish_loading_map)
        {
            timer = Time.timeSinceLevelLoad + interval;
            if(spawnDecorations){
                decorationSpawner.SpawnDecoration();
            }
            if (!string.IsNullOrEmpty(GlobalVariables.MapID))
            {
                print("save start");
                SaveGameMapServer(GlobalVariables.MapID);
            } else
            {
                Debug.Log("Cannot save: No avaliable MapID");
            }
        }
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

    /// <summary>
    /// Clears tiles and decorations.
    /// </summary>
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

    /// <summary>
    /// Generates a WIDTH x HEIGHT map with roughly WATER_PERCENTAGE% of water using a perlin noise grid, <br/>
    /// then places DECORATIONS_PER_TILE decorations per tile using a different perlin noise grid.
    /// </summary>
    public void GenerateGameMap()
    {
        ClearGameMap();
        List<GameObject> tileObjects = GetObjectsByName(inventory.inventoryLst, TILES);
        List<GameObject> decors = GetObjectsByName(inventory.inventoryLst, DECOR);

        float[,] tileGrid = GeneratePerlinNoiseGrid(WIDTH + 1, LENGTH + 1, 7); /// the last parameter is the scale, the higher it is, the more "zoomed out" and less detailed it is
        float[,] decorGrid = GeneratePerlinNoiseGrid(WIDTH + 1, LENGTH + 1, 15);

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

    /// <summary>
    /// Creates a tile, using a different tile or no tile at all depending on perlin noise.
    /// </summary>
    /// <param name="x">The x coordinate of the tile</param>
    /// <param name="z">The z coordinate of the tile</param>
    /// <param name="grid">The value from perlin noise at that coordinate</param>
    /// <param name="tiles">The list of tiles in inventory</param>
    /// <returns>The tile object if instantiated, null if it was not instantiated (water)</returns>
    private GameObject CreateTile(int x, int z, float[,] grid, List<GameObject> tiles)
    {
        Transform landTransform = GameObject.Find("Land").transform;
        float yHeight = landTransform.position.y;

        int perlin = (int)Math.Floor((grid[x, z] / (1 - (WATER_PERCENTAGE / 100f))) * (float)(tiles.Count));

        if (perlin >= 0 && perlin < tiles.Count)
        {
            Vector3 position = new Vector3(x - WIDTH / 2, yHeight, z - LENGTH / 2); // center on origin
            GameObject tile = Instantiate(tiles[perlin], position, Quaternion.identity);
            tile.transform.SetParent(landTransform, true);
            return tile;
        }

        return null;
    }

    /// <summary>
    /// Spawns DECORATIONS_PER_TILE random decorations on the given tile.
    /// </summary>
    /// <param name="x">The x coordinate of the tile</param>
    /// <param name="z">The z coordinate of the tile</param>
    /// <param name="grid">The value from perlin noise at that coordinate</param>
    /// <param name="decors">List of decors in inventory</param>
    /// <param name="tile">The tile that the decorations will be spawned on</param>
    private void CreateDecorations(int x, int z, float[,] grid, List<GameObject> decors, GameObject tile)
    {
        if (tile == null) return;

        for (int c = 0; c < DECORATIONS_PER_TILE; c++)
        {
            int perlin = (int)Math.Floor(grid[x, z] * 100);
            if (perlin < 35)
            {
                Vector3 randomOffset = new Vector3(UnityEngine.Random.value - 0.5f, 0, UnityEngine.Random.value - 0.5f);
                Vector3 position = tile.transform.position + randomOffset;
                int randomRot = UnityEngine.Random.Range(0,4);
                GameObject decoration = Instantiate(decors[UnityEngine.Random.Range(0, decors.Count)], position, Quaternion.identity);
                decoration.tag = "Decoration";
                decoration.transform.Rotate(new Vector3(0, randomRot * 90, 0));
                tile.GetComponent<MapTile>().isOccupied = true;
            }
        }
    }

    /// <summary>
    /// Generates a perlin noise grid. <br/>
    /// While perlin noise grids are deterministic, we introduce a random offset to generate unique maps.
    /// </summary>
    /// <param name="width">The width of the grid (x-axis)</param>
    /// <param name="length">The length of the grid (z-axis)</param>
    /// <param name="scale">The higher it is, the more "zoomed out" and less detailed it is</param>
    /// <returns>A 2d array of floats between 0 and 1, representing a perlin grid</returns>
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
        finish_loading_map = true;
        cover.GetComponent<Animator>().SetTrigger("loadScene");
        cloudsOnLoad.SetActive(true);
        PollutionManager.pollutionManager.UpdateTreeCount();
        PollutionManager.pollutionManager.UpdatePollutionIndex();
        PollutionManager.pollutionManager.UpdatePolluters();
        PopulationManager.populationManager.UpdateHouses();
        HarvesterManager.harvesterManager.UpdateHarvester();
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
                        decoration.transform.Rotate(decor.rotation.GetValue());
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