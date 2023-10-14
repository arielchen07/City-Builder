using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataManager : MonoBehaviour
{
    public SaveFile saveSystem;
    public InventoryManager inventory;

    private void Update()
    {
        // Key press for save/load the game, testing purpose only, will be replaced by click button in UI
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SaveGameObjects();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            LoadGameObjects();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ClearGameMap();
        }
    }

    void ClearGameMap()
    {
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

    void SaveGameObjects()
    {
        StructureObjsSerialization structureObjs = new StructureObjsSerialization();

        PlaceableObject[] placeableObjects = GameObject.FindObjectsOfType<PlaceableObject>();
        foreach (PlaceableObject rb in placeableObjects)
        {
            structureObjs.AddObj(rb.name, rb.transform.position, rb.transform.rotation.eulerAngles);
        }


        Road[] roads = GameObject.FindObjectsOfType<Road>();
        foreach (Road rb in roads)
        {
            structureObjs.AddObj(rb.name, rb.transform.position, rb.transform.rotation.eulerAngles);
        }

        var structureObjJson = JsonUtility.ToJson(structureObjs);
        saveSystem.SaveData(structureObjJson);
    }

    void LoadGameObjects()
    {
        ClearGameMap();
        var structureObjJson = saveSystem.LoadData();
        if (String.IsNullOrEmpty(structureObjJson))
            return;
        StructureObjsSerialization structureObjs = JsonUtility.FromJson<StructureObjsSerialization>(structureObjJson);

        foreach (var structure in structureObjs.structureObjData)
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
                        GameObject building = Instantiate(placeableObj, structure.position.GetValue(), Quaternion.Euler(structure.rotation.GetValue()));

                        // Update colliding tiles
                        foreach (GameObject tile in building.GetComponent<PlaceableObject>().GetCollidingTiles())
                        {
                            tile.GetComponent<MapTile>().isOccupied = true;
                            tile.GetComponent<MapTile>().placedObject = building;
                        }
                    }
                        
                    break;
                }
            }
        }
    }
}
