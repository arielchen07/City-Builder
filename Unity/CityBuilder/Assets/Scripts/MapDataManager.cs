using System;
using System.Collections.Generic;
using UnityEngine;

public class MapDataManager : MonoBehaviour
{
    public SaveFile saveSystem;
    public InventoryManager inventory;
    public PlacementSystem placementSystem;
    public InputManager inputManager;

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
        MapTile[] tiles = GameObject.FindObjectsOfType<MapTile>();
        foreach (MapTile tile in tiles)
        {
            Destroy(tile.gameObject);
        }
    }
    public void SaveGameMapServer()
    {
        var mapDataJson = SerializeAllGameObjects();
        saveSystem.SaveDataServer(mapDataJson);
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

        MapTile[] tiles = GameObject.FindObjectsOfType<MapTile>();
        foreach (MapTile tile in tiles)
        {
            mapObjs.AddTile(tile.gameObject.name, tile.transform.position, tile.transform.rotation.eulerAngles, tile.isOccupied);
        }

        var mapDataJson = JsonUtility.ToJson(mapObjs);
        return mapDataJson;
    }

    public void LoadGameMapServer()
    {
        saveSystem.LoadDataServer();
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
        // Draw map tiles
        Transform landTransform = GameObject.Find("Land").transform;

        foreach (var tile in mapObjs.tileData)
        {
            foreach (var tileObj in inventory.inventoryLst)
            {
                if (tile.name.IndexOf(tileObj.name) != -1)
                {
                    GameObject tileInst = Instantiate(tileObj, tile.position.GetValue(), Quaternion.Euler(tile.rotation.GetValue()));
                    tileInst.transform.SetParent(landTransform, true);
                }
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
