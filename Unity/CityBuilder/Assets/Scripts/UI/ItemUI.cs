using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// The ItemUI class is attached to all inventory item buttons. <br/>
/// This class is responsible for allowing users to place objects when clicking on an object in their inventory. <br/>
/// </summary>
public class ItemUI : MonoBehaviour
{
    public string itemName;
    public string category;
    public int quantity;
    public string itemID;
    public InventoryManager inventoryManager;
    public TMPro.TMP_Text quantityText;
    public GameObject objectPrefab;
    public float updateInterval = 5.0f;
    public bool isNew = true;

    /// <summary>
    /// Update the quantity of this item at every game frame 
    /// to make sure the displayed value is real-time
    /// </summary>
    void Update()
    {
        UpdateItemQuantity();
    }

    /// <summary>
    /// Gets the item's quantity from the inventory. <br/>
    /// Disables the item's button if the user runs out.
    /// </summary>
    /// <returns>The quantity of this item as an integer.</returns>
    private int UpdateItemQuantity()
    {
        quantity = InventoryInfo.GetItemQuantity(itemName, category);
        quantityText.text = quantity.ToString();
        if (quantity == 0)
        {
            gameObject.GetComponent<Button>().interactable = false;
            transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
        return quantity;
    }

    /// <summary>
    /// Removes an item from the inventory. <br/>
    /// This function is called when the user places an item from their inventory.
    /// </summary>
    public void TakeItem()
    {
        itemID = InventoryInfo.GetItemID(itemName, category);
        print("itemID: " + itemID);
        inventoryManager.UpdateItemQuantityToServer(itemID, -1);
        quantity = quantity - 1;
        quantityText.text = quantity.ToString();
        UpdateItemQuantity();
    }

    /// <summary>
    /// Adds an item to the inventory. <br/>
    /// This function is called when the user deletes an object from their map, 
    /// which puts it back into their inventory.
    /// </summary>
    public void StoreItem()
    {
        itemID = InventoryInfo.GetItemID(itemName, category);
        inventoryManager.UpdateItemQuantityToServer(itemID, 1);
        quantity = quantity + 1;
        quantityText.text = quantity.ToString();
        UpdateItemQuantity();
    }

}
