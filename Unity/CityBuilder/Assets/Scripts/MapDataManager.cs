using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MapDataManager : MonoBehaviour
{
    public SaveFile saveSystem;
    public InventoryManager inventory;
    public PlacementSystem placementSystem;
    [SerializeField] private InputManager inputManager;

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

    public void ClearGameMap()
    {
        foreach (var item in GameObject.FindObjectsOfType<PlaceableObject>())
        {
            //if (item.gameObject != placementSystem.GetComponent<PlacementSystem>().GetCurrentlyPlacing())
            //{
            //    Destroy(item.gameObject);
            //}
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

    public void SaveGameObjects()
    {
        //    if (placementSystem.GetComponent<PlacementSystem>().GetCurrentlyPlacing() != null)
        //    {
        //        Debug.Log("Cannot save, please put all items on map");
        //        return;
        //    }
        StructureObjsSerialization structureObjs = new StructureObjsSerialization();

        PlaceableObject[] placeableObjects = GameObject.FindObjectsOfType<PlaceableObject>();
        foreach (PlaceableObject rb in placeableObjects)
        {
            if (rb.gameObject != placementSystem.GetComponent<PlacementSystem>().GetCurrentlyPlacing())
            {
                structureObjs.AddObj(rb.name, rb.transform.position, rb.transform.rotation.eulerAngles);
            }
            //structureObjs.AddObj(rb.name, rb.transform.position, rb.transform.rotation.eulerAngles);
        }


        Road[] roads = GameObject.FindObjectsOfType<Road>();
        foreach (Road rb in roads)
        {
            structureObjs.AddObj(rb.name, rb.transform.position, rb.transform.rotation.eulerAngles);
        }

        var structureObjJson = JsonUtility.ToJson(structureObjs);
        saveSystem.SaveData(structureObjJson);
    }

    public void LoadGameObjects()
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

                        // Update colliding tiles and building state
                        building.transform.parent = null;
                        foreach (GameObject tile in building.GetComponent<PlaceableObject>().GetCollidingTiles())
                        {
                            tile.GetComponent<MapTile>().isOccupied = true;
                            tile.GetComponent<MapTile>().placedObject = building;
                        }
                        inputManager.placementLayermask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Foreground");

                        building.GetComponent<PlaceableObject>().isHovering = false;
                        building.GetComponent<PlaceableObject>().hasBeenPlaced = true;
                    }
                        
                    break;
                }
            }
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
