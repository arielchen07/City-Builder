using System.Collections.Generic;
using UnityEngine;

class inventoryItem{
    public string name { get; set; }
    public int quantity { get; set; }
    public string itemID { get; set; }
}

public class InventoryManager : MonoBehaviour
{
    public InventoryToServer toServer;
    public ServerInventoryData inventory;
    public string userID = "65517d2ab753aa75060ea62c";
    Dictionary<string, Dictionary<string, inventoryItem>> itemInventoryDict= new Dictionary<string, Dictionary<string, inventoryItem>>();
    private const int INITQUANTITY = 2;
    private List<string> initResList = new List<string>() {"singleHouse"};

    private void Start()
    {
        // Regularily gets live inventory information from server and updates unity inventory
        //toServer.RegularUpdateInventory(userID);
        // TODO: Initialize inventory with some amount of items

        Dictionary<string, inventoryItem> residentialDict = initializeInventoryCategory(initResList, INITQUANTITY);

        itemInventoryDict.Add("residential", residentialDict);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            // Increase number of item by 1: UpdateItemServer(userID, itemID, 1);
            toServer.UpdateItemToServer(userID, "65517d2ab753aa75060ea62e", 1);
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            // Decrease number of item by 1: UpdateItemServer(userID, itemID, -1);
            toServer.UpdateItemToServer(userID, "65517d2ab753aa75060ea62e", -1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            toServer.LoadInventoryFromServer(userID);
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            // Create a new item (item not in server invenotry): CreateItemServer(category, name, amount);
            toServer.CreateItemToServer(userID, "c2", "n2", 1);
        }
    }

    private Dictionary<string, inventoryItem> initializeInventoryCategory(List<string> objectNameList, int quantity)
    {
        Dictionary<string, inventoryItem> objectNameToInfoDict = new Dictionary<string, inventoryItem>();

        for (int i = 0; i < objectNameList.Count; i++)
        {
            objectNameToInfoDict.Add(objectNameList[i], new inventoryItem { name = objectNameList[i], quantity = quantity, itemID = null });
        }

        return objectNameToInfoDict;
    }

    private Dictionary<string, inventoryItem> createDictByCategory(string objectName, int quantity, string itemID)
    {
        Dictionary<string, inventoryItem> objectNameToInfoDict = new Dictionary<string, inventoryItem>();
        objectNameToInfoDict.Add(objectName, new inventoryItem { name = objectName, quantity = quantity, itemID = itemID });
        return objectNameToInfoDict;
    }

    public void UpdateInventory(ServerInventoryData inventory)
    {
        // TODO: change this to actual updates to the Unity inventory data structure and UI
        this.inventory = inventory;
        foreach (var item in inventory.items){
            UpdateInventoryItem(item);
        }
    }

    public void UpdateInventoryItem(ServerItemData item)
    {
        // TODO: change this to actual updates to the Unity inventory data structure and UI
        if (itemInventoryDict.ContainsKey(item.category))
        {
            if (itemInventoryDict[item.category].ContainsKey(item.name))
            {
                itemInventoryDict[item.category][item.name].quantity = item.quantity;
                itemInventoryDict[item.category][item.name].itemID = item._id;
            }
            else
            {
                itemInventoryDict[item.category][item.name] = new inventoryItem { name = item.name, quantity = item.quantity, itemID = item._id };
            }

        }
        else
        {
            itemInventoryDict[item.category] = createDictByCategory(item.name, item.quantity, item._id);
        }
    }
}