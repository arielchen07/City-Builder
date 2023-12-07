using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The ObjectMenuManager is responsible for the menu opened when the user clicks on an object. <br/>
/// There are two separate object menus, one for housing objects to display the utility stats, and one for generic buildings. <br/>
/// For future development, this class can be made a parent class, and make different objects that need to display different things inherit from this class.
/// </summary>
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

    /// <summary>
    /// If the selected building is a house, update its utility stat UIs. Note that this does not update the actual values, just the text.
    /// </summary>
    /// <param name="selectedObject">The object that the user selected.</param>
    public void UpdateInfo(GameObject selectedObject)
    {
        objectName.text = selectedObject.GetComponent<PlaceableObject>().displayName;
        currentlySelecting = selectedObject;
        if (selectedObject.TryGetComponent<House>(out var h))
        {
            population.text = h.GetPopulation();
            power.text = h.GetPower();
            water.text = h.GetWater();
            sewage.text = h.GetSewage();
            internet.text = h.GetInternet();
            naturalGas.text = h.GetGas();
        }
    }
    /// <summary>
    /// Calls the placement system to move the object.<br/>
    /// Called by the move button.
    /// </summary>
    public void Move()
    {
        ps.MoveObject();
    }
    /// <summary>
    /// Calls the placement system to delete the object. <br/>
    /// Called by the delete button.
    /// </summary>
    public void Delete()
    {
        item = currentlySelecting.GetComponent<PlaceableObject>().item;
        RecycleItem();
        ps.DeleteObject();
    }
    /// <summary>
    /// Calls the placement system to rotate the object left.<br/>
    /// Called by the rotate left button.
    /// </summary>
    public void RotateLeft()
    {
        ps.RotateObject(true);
    }
    /// <summary>
    /// Calls the placement system to rotate the object right.<br/>
    /// Called by the rotate right button.
    /// </summary>
    public void RotateRight()
    {
        ps.RotateObject(false);
    }
    /// <summary>
    /// Destroys the gameobject this script is attached to.
    /// This is called by the object's animator when the menu closes.
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// This is called by the Delete function.<br/>
    /// Adds the item back to the inventory.
    /// </summary>
    public void RecycleItem()
    {
        PlaceableObject po = currentlySelecting.GetComponent<PlaceableObject>();
        itemID = InventoryInfo.GetItemID(po.objectName, po.category);
        inventoryManager.UpdateItemQuantityToServer(itemID, 1);
    }
}
