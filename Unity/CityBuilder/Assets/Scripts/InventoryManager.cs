using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The inventoryItem class is a data structure designed to store the name, quantity and id of an item in the inventory.
/// Each attribute of the item can be taken or updated through get/set functions.
/// </summary>
public class inventoryItem{
    public string name { get; set; }
    public int quantity { get; set; }
    public string itemID { get; set; }
}

/// <summary>
/// The inventoryInfo class is data structure that designed to manage all item information currently in the inventory. 
/// It contains itemInventoryDict, a dictionry of dictionaries of inventoryItem to store information.
/// </summary>
public static class InventoryInfo
{
    public static Dictionary<string, Dictionary<string, inventoryItem>> itemInventoryDict = new Dictionary<string, Dictionary<string, inventoryItem>>();

    /// <summary>
    /// Gets the item's quantity from the itemInventoryDict. <br/>
    /// </summary>
    /// <param name="itemName">The name of the required item</param>
    /// <param name="category">The catogory that the required item belongs to</param>
    /// <returns>The quantity of this item as an integer. Return 0 if the item does not exist in current inventory.</returns>
    public static int GetItemQuantity(string itemName, string category)
    {
        if (InventoryInfo.itemInventoryDict.ContainsKey(category))
        {
            if (InventoryInfo.itemInventoryDict[category].ContainsKey(itemName))
            {
                return InventoryInfo.itemInventoryDict[category][itemName].quantity;
            }
        }
        return 0;
    }

    /// <summary>
    /// Gets the item's ID from the itemInventoryDict. <br/>
    /// </summary>
    /// <param name="itemName">The name of the required item</param>
    /// <param name="category">The catogory that the required item belongs to</param>
    /// <returns>The ID of this item as a string. Return null if the item does not exist in current inventory.</returns>
    public static string GetItemID(string itemName, string category)
    {
        if (InventoryInfo.itemInventoryDict.ContainsKey(category))
        {
            if (InventoryInfo.itemInventoryDict[category].ContainsKey(itemName))
            {
                return InventoryInfo.itemInventoryDict[category][itemName].itemID;
            }
        }
        return null;
    }
}

/// <summary>
/// The InventoryManager class is attached to the InventoryManager Object. <br/>
/// This class is a singleton and is globally accessible. <br/>
/// This class is responsible for tracking and updating the information in the inventory the player has,
/// including catogories of items, item names, item ids and quantities.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public InventoryToServer toServer;
    public ServerInventoryData inventory;

    /// <summary>
    /// Load inventory information from server based on the userID of the current user
    /// </summary>
    private void Awake()
    {
        if (!string.IsNullOrEmpty(GlobalVariables.UserID)){
            toServer.LoadInventoryFromServer(GlobalVariables.UserID);
        }
    }

    void Update()
    {

    }

    /// <summary>
    /// Initailize a new dictionary for a catogory with an inventoryItem, 
    /// which is created based on given objectName, quantity and itemID.
    /// This function is intended to be called by UpdateInventoryItem when the category of 
    /// the item received from the server does not already exist in the current inventory.    
    /// </summary>
    /// <param name="objectName">The name of the object item</param>
    /// <param name="quantity">The quantity of the object item</param>
    /// <param name="itemID">The itemID of the object item</param>
    /// <returns></returns>
    private Dictionary<string, inventoryItem> createDictByCategory(string objectName, int quantity, string itemID)
    {
        Dictionary<string, inventoryItem> objectNameToInfoDict = new Dictionary<string, inventoryItem>();
        objectNameToInfoDict.Add(objectName, new inventoryItem { name = objectName, quantity = quantity, itemID = itemID });
        return objectNameToInfoDict;
    }

    /// <summary>
    /// Update InventoryInfo based on the information sent back from the server.
    /// </summary>
    /// <param name="inventory">the inventory information sent back from server</param>
    public void UpdateInventory(ServerInventoryData inventory)
    {
        this.inventory = inventory;
        foreach (var item in inventory.items){
            UpdateInventoryItem(item);
        }
    }

    /// <summary>
    /// Update InventoryInfo based on the given item information that sent back from server.
    /// This function will be called by UpdateInventory to update information of one item each time.
    /// </summary>
    /// <param name="item">the item information sent back from server</param>
    public void UpdateInventoryItem(ServerItemData item)
    {
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

    /// <summary>
    /// Update the amount of item quantity that has changed to the server
    /// </summary>
    /// <param name="itemID">the id of the item</param>
    /// <param name="quantityChanged">the amount of quantity that has changed</param>
    public void UpdateItemQuantityToServer(string itemID, int quantityChanged){
        toServer.UpdateItemToServer(GlobalVariables.UserID, itemID, quantityChanged);
        print("update item quantity change" + quantityChanged + "to server");
    }
}