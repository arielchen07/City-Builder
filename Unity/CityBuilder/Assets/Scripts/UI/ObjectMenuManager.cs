using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour
{
    public PlacementSystem ps;
    public Text objectName;
    public Text population;
    public Text power;
    public Text water;
    public Text sewage;
    public Text internet;
    public Text naturalGas;
    public GameObject currentlySelecting;
    public InventoryManager inventoryManager;
    public string itemID;
    ItemUI item;

    public void UpdateInfo(GameObject selectedObject){
        objectName.text = selectedObject.GetComponent<PlaceableObject>().displayName;
        currentlySelecting = selectedObject;
        if(selectedObject.TryGetComponent<House>(out var h)){
            population.text = h.GetPopulation();
            power.text = h.GetPower();
            water.text = h.GetWater();
            sewage.text = h.GetSewage();
            internet.text = h.GetInternet();
            naturalGas.text = h.GetGas();
        }
        else {
            
        }
    }

    public void Move(){
        ps.MoveObject();
    }

    public void Delete(){
        item = currentlySelecting.GetComponent<PlaceableObject>().item;
        RecycleItem();
        ps.DeleteObject();
    }

    public void RotateLeft(){
        ps.RotateObject(true);
    }

    public void RotateRight(){
        ps.RotateObject(false);
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }

    public void RecycleItem(){
        PlaceableObject po = currentlySelecting.GetComponent<PlaceableObject>();
        itemID = InventoryInfo.GetItemID(po.objectName, po.category);
        inventoryManager.UpdateItemQuantityToServer(itemID, 1);
    }
}
