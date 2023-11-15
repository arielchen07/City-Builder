using UnityEngine;
using TMPro;
using System;

public class ItemUI : MonoBehaviour
{
    public string itemName;
    public string category;
    public int quantity;
    public string itemID;
    public InventoryManager inventoryManager;
    public TMPro.TMP_Text quantityText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateItemQuantity();
        itemID = inventoryManager.GetItemID(itemName, itemID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        if(quantity > 0){
            PlaceItem();
        }
        else
        {
            Debug.LogError("ItemUI: You don't have any" + itemName + "in the inventory!");
        }
    }

    private void UpdateItemQuantity(){
        quantity = inventoryManager.GetItemQuantity(itemName, category);
        quantityText.text = quantity.ToString();
    }

    public void PlaceItem(){
        inventoryManager.UpdateItemQuantityToServer(itemID, -1);
        UpdateItemQuantity();
    }

    public void RecycleItem(){
        inventoryManager.UpdateItemQuantityToServer(itemID, 1);
        UpdateItemQuantity();
    }

}
