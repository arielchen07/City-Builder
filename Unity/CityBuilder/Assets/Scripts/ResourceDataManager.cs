using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The ResourceDataManager class is responsible for tracking the resources the player has. <br/>
/// This class gets the quantities from the inventory and displays it to the user.
/// </summary>
public class ResourceDataManager : MonoBehaviour
{
    public int woodCount;
    public int stoneCount;
    public InventoryManager inventoryManager;
    public Text woodText;
    public Text stoneText;

    void Start()
    {
        InvokeRepeating("UpdateCounts", 0, 1f);
    }

    /// <summary>
    /// Gets the current quantity of resources and updates UI.
    /// </summary>
    void UpdateCounts()
    {
        int wood = InventoryInfo.GetItemQuantity("wood", "resource");
        int stone = InventoryInfo.GetItemQuantity("stone", "resource");
        woodCount = wood;
        stoneCount = stone;
        woodText.text = wood.ToString();
        stoneText.text = stone.ToString();
    }

    /// <summary>
    /// Reduces the quantity of the specified resource.
    /// </summary>
    /// <param name="itemName">The inventory name of the resource to consume.</param>
    /// <param name="consumedCount">The amount to consume.</param>
    public void ConsumeResource(string itemName, int consumedCount)
    {
        string itemID = InventoryInfo.GetItemID(itemName, "resource");
        inventoryManager.UpdateItemQuantityToServer(itemID, -consumedCount);
    }

    /// <summary>
    /// Increase the quantity of the specified resource.
    /// </summary>
    /// <param name="itemName">The inventory name of the resource to increase.</param>
    /// <param name="gainedCount">The amount to increase.</param>
    public void GainResource(string itemName, int gainedCount)
    {
        if (itemName == "wood")
        {
            woodCount += gainedCount;
        }
        else if (itemName == "stone")
        {
            stoneCount += gainedCount;
        }
        string itemID = InventoryInfo.GetItemID(itemName, "resource");
        inventoryManager.UpdateItemQuantityToServer(itemID, gainedCount);
    }
}
