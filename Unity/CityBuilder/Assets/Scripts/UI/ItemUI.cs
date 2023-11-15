using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    public string name;
    public string category;
    public int quantity;
    public string itemID;
    public InventoryManager invenotryManager;

    // Start is called before the first frame update
    void Start()
    {
        UpdateItemQuantity();
        itemID = invenotryManager.GetItemID(name, itemID);
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
            Debug.LogError("ItemUI: You don't have any" + name + "in the inventory!");
        }
    }

    private void UpdateItemQuantity(){
        quantity = invenotryManager.GetItemQuantity(name, category);
    }

    public void PlaceItem(){
        invenotryManager.UpdateItemQuantityToServer(itemID, -1);
        UpdateItemQuantity();
    }

    public void RecycleItem(){
        invenotryManager.UpdateItemQuantityToServer(itemID, 1);
        UpdateItemQuantity();
    }

}
