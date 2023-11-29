using UnityEngine;
using UnityEngine.UI;
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
    public GameObject objectPrefab;
    public float updateInterval = 5.0f;
    public bool isNew = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemQuantity();
    }

    private int UpdateItemQuantity(){
        quantity = InventoryInfo.GetItemQuantity(itemName, category);
        quantityText.text = quantity.ToString();
        if (quantity == 0)
        {
            gameObject.GetComponent<Button>().interactable = false;
            transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1f,1f,1f);
        }
        return quantity;
    }

    public void TakeItem(){
        itemID = InventoryInfo.GetItemID(itemName, category);
        print("itemID: " + itemID);
        inventoryManager.UpdateItemQuantityToServer(itemID, -1);
        quantity = quantity - 1;
        quantityText.text = quantity.ToString();
        UpdateItemQuantity();
    }

    public void StoreItem(){
        itemID = InventoryInfo.GetItemID(itemName, category);
        inventoryManager.UpdateItemQuantityToServer(itemID, 1);
        quantity = quantity + 1;
        quantityText.text = quantity.ToString();
        UpdateItemQuantity();
    }

}
