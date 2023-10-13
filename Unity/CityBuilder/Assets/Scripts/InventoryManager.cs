using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> inventoryLst = new List<GameObject>();
}

public class InventoryItem
{
    public string name;
    public GameObject item;
}