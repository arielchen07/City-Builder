using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int quantity = 0;
    public string itemName = "coins";
    public string category = "resource";
    public string itemID;
    public Text quantityText;
    public InventoryManager inventoryManager;
    public static MoneyManager moneyManager;
    private void Awake(){
        if (moneyManager != null && moneyManager != this){
            Destroy(this);
        } else {
            moneyManager = this;
        }
    }
    void Start(){
        UpdateItemQuantity();
    }
    // void Update()
    // {
    // }

    private int UpdateItemQuantity(){
        quantity = InventoryInfo.GetItemQuantity(itemName, category);
        quantityText.text = quantity.ToString();
        return quantity;
    }

    public bool DecreaseCoins(int amount){
        if(quantity - amount < 0){
            return false;
        }
        itemID = InventoryInfo.GetItemID(itemName, category);
        print("itemID: " + itemID);
        for (int i = 0; i < amount; i++){
            inventoryManager.UpdateItemQuantityToServer(itemID, -1);
        }
        quantity -= amount;
        quantityText.text = quantity.ToString();
        UpdateItemQuantity();
        return true;
    }

    public void IncreaseCoins(int amount){
        itemID = InventoryInfo.GetItemID(itemName, category);
        for (int i = 0; i < amount; i++){
            inventoryManager.UpdateItemQuantityToServer(itemID, 1);
        }
        quantity += amount;
        quantityText.text = quantity.ToString();
        UpdateItemQuantity();
    }
}
