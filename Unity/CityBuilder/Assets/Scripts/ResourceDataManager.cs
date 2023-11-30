using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDataManager : MonoBehaviour
{
    public int woodCount = 0;
    public int stoneCount = 0;
    public InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateCounts", 0, 1f);
    }

    // Update is called once per frame
    void UpdateCounts()
    {
        int wood = InventoryInfo.GetItemQuantity("wood", "resource");
        int stone = InventoryInfo.GetItemQuantity("stone", "resource");
        Debug.Log("wood = " + wood.ToString() + ", stone = " + stone.ToString());
    }

    public void ConsumeResource(string itemName, int consumedCount)
    {
        string itemID = InventoryInfo.GetItemID(itemName, "resource");
        inventoryManager.UpdateItemQuantityToServer(itemID, - consumedCount);
    }

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
        //string itemID = InventoryInfo.GetItemID(itemName, "resource");
        //inventoryManager.UpdateItemQuantityToServer(itemID, gainedCount);
    }
}
