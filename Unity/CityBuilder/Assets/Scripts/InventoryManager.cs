using System.Collections.Generic;
using UnityEngine;

public class inventoryItem{
    public string name { get; set; }
    public int quantity { get; set; }
    public string itemID { get; set; }
}

public static class InventoryInfo
{
    public static Dictionary<string, Dictionary<string, inventoryItem>> itemInventoryDict = new Dictionary<string, Dictionary<string, inventoryItem>>();
    public static bool isNew = true;

    public static int GetItemQuantity(string itemName, string category)
    {
        if (InventoryInfo.itemInventoryDict.ContainsKey(category))
        {
            if (InventoryInfo.itemInventoryDict[category].ContainsKey(itemName))
            {
                Debug.Log("get quantity of item: " + itemName + "in category: " + category);
                return InventoryInfo.itemInventoryDict[category][itemName].quantity;
            }
        }
        return 0;
    }

    public static string GetItemID(string itemName, string category)
    {
        if (InventoryInfo.itemInventoryDict.ContainsKey(category))
        {
            Debug.Log("get id for category: " + category);
            if (InventoryInfo.itemInventoryDict[category].ContainsKey(itemName))
            {
                Debug.Log("get id for item: " + itemName);
                return InventoryInfo.itemInventoryDict[category][itemName].itemID;
            }
        }
        return null;
    }
}

public class InventoryManager : MonoBehaviour
{
    public InventoryToServer toServer;
    public ServerInventoryData inventory;
    public string userID = "65517d2ab753aa75060ea62c";
    //Dictionary<string, Dictionary<string, inventoryItem>> itemInventoryDict= new Dictionary<string, Dictionary<string, inventoryItem>>();

    private void Awake()
    {
        // Regularily gets live inventory information from server and updates unity inventory
        //toServer.RegularUpdateInventory(userID);
        // TODO: Initialize inventory with some amount of items

        if (!string.IsNullOrEmpty(GlobalVariables.UserID)){
            toServer.LoadInventoryFromServer(GlobalVariables.UserID);
        }
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            foreach (var category in InventoryInfo.itemInventoryDict.Keys)
            {
                print("category: " + category);
                foreach( var item in InventoryInfo.itemInventoryDict[category].Keys)
                {
                    print("item name: " + item + ", quantity:" + InventoryInfo.itemInventoryDict[category][item].quantity + ", itemID:" + InventoryInfo.itemInventoryDict[category][item].itemID);
                }
            }
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
        print("call UpdateInventory");
        // TODO: change this to actual updates to the Unity inventory data structure and UI
        this.inventory = inventory;
        foreach (var item in inventory.items){
            UpdateInventoryItem(item);
        }
    }

    public void UpdateInventoryItem(ServerItemData item)
    {
        print("call UpdateInventoryItem");
        // TODO: change this to actual updates to the Unity inventory data structure and UI
        if (InventoryInfo.itemInventoryDict.ContainsKey(item.category))
        {
            if (InventoryInfo.itemInventoryDict[item.category].ContainsKey(item.name))
            {
                InventoryInfo.itemInventoryDict[item.category][item.name].quantity = item.quantity;
                InventoryInfo.itemInventoryDict[item.category][item.name].itemID = item._id;
            }
            else
            {
                InventoryInfo.itemInventoryDict[item.category][item.name] = new inventoryItem { name = item.name, quantity = item.quantity, itemID = item._id };
            }

        }
        else
        {
            InventoryInfo.itemInventoryDict[item.category] = createDictByCategory(item.name, item.quantity, item._id);
        }
    }

    public void UpdateItemQuantityToServer(string itemID, int quantityChanged){
        // Update number of item by +/-1: UpdateItemServer(userID, itemID, +/-1);
        toServer.UpdateItemToServer(userID, itemID, quantityChanged);
        print("update item quantity change" + quantityChanged + "to server");
    }
}