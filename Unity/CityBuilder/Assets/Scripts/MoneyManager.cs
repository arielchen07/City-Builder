using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The MoneyManager class is attached to the MoneyManager Object. <br/>
/// This class is a singleton and is globally accessible. <br/>
/// This class is responsible for tracking and updating the total amount of money the player has.
/// </summary>
public class MoneyManager : MonoBehaviour
{
    public int quantity = 0;
    public string itemName = "coins";
    public string category = "resource";
    public string itemID;
    public Text quantityText;
    public InventoryManager inventoryManager;
    public static MoneyManager moneyManager;
    private void Awake()
    {
        if (moneyManager != null && moneyManager != this)
        {
            Destroy(this);
        }
        else
        {
            moneyManager = this;
        }
    }
    void Start()
    {
        UpdateItemQuantity();
    }

    /// <summary>
    /// Updates the coin quantity from the inventory.
    /// </summary>
    /// <returns>As an integer, the number of coins the player currently has.</returns>
    private int UpdateItemQuantity()
    {
        quantity = InventoryInfo.GetItemQuantity(itemName, category);
        quantityText.text = quantity.ToString();
        return quantity;
    }

    /// <summary>
    /// Decrease the number of coins the player has <br/>
    /// This function is currently not being used, but can be used by future developers when implementing building creation, where money is a cost.
    /// </summary>
    /// <param name="amount">The amount of money to decrease by.</param>
    /// <returns>True if the amount of money to decrease is less than the player's current money count, and False otherwise.</returns>
    public bool DecreaseCoins(int amount)
    {
        if (quantity - amount < 0)
        {
            return false;
        }
        itemID = InventoryInfo.GetItemID(itemName, category);
        print("itemID: " + itemID);
        inventoryManager.UpdateItemQuantityToServer(itemID, -amount);
        quantity -= amount;
        quantityText.text = quantity.ToString();
        UpdateItemQuantity();
        return true;
    }

    /// <summary>
    /// Increases the number of coins the player currently has. <br/>
    /// This function is called by the TownHallController at a set interval.
    /// </summary>
    /// <param name="amount">The amount of coins to increase by.</param>
    public void IncreaseCoins(int amount)
    {
        itemID = InventoryInfo.GetItemID(itemName, category);
        inventoryManager.UpdateItemQuantityToServer(itemID, amount);
        quantity += amount;
        quantityText.text = quantity.ToString();
        UpdateItemQuantity();
    }
}
