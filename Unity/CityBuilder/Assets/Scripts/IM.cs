using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IM : MonoBehaviour
{
    public InventoryToServer toServer;
    public ServerInventoryData inventory;
    public string userID = "653eb62ed980902850c0580a";

    private void Start()
    {
        // Regularily gets live inventory information from server and updates unity inventory
        toServer.RegularUpdateInventory(userID);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            // Increase number of item by 1: UpdateItemServer(userID, itemID, 1);
            toServer.UpdateItemToServer(userID, "654d8d0d7454d18638a21ec5", 1);
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            // Decrease number of item by 1: UpdateItemServer(userID, itemID, -1);
            toServer.UpdateItemToServer(userID, "654d8d0d7454d18638a21ec5", -1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            toServer.LoadInventoryFromServer(userID);
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            // Create a new item (item not in server invenotry): CreateItemServer(category, name, amount);
            toServer.CreateItemToServer(userID, "c2", "n2", 1);
        }
    }

    public void UpdateInventory(ServerInventoryData inventory)
    {
        // TODO: change this to actual updates to the Unity inventory data structure and UI
        this.inventory = inventory;
    }
}
