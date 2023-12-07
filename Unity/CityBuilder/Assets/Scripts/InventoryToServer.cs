using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class InventoryToServer : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public float updateInterval = 5.0f; // Checks for server inventory data every 5 seconds

    /// <summary>
    /// Update inventory item quantity on server, calls UpdateItemRequest, syncs unity inventory with server
    /// </summary>
    /// <param name="userID">Unique id for the user</param>
    /// <param name="itemID">Unique id for the inventory item that is being updated</param>
    /// <param name="amount">Quantity changed for the item</param>
    public void UpdateItemToServer(string userID, string itemID, int amount)
    {
        if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(itemID)) {
            Debug.Log("No userID or itemID information, change will not be updated to server");
            return;
        }
        StartCoroutine(
            UpdateItemRequest(userID, itemID, amount,
                (UnityWebRequest request) =>
                {
                    // call to load game objects after recieved data from get request
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        string data = request.downloadHandler.text;
                        print("Update item read from server recieved: " + data);
                        if (String.IsNullOrEmpty(data))
                        {
                            return;
                        }
                        var item = JsonUtility.FromJson<ServerItemData>(
                            data
                        );
                        inventoryManager.UpdateInventoryItem(item);

                        Debug.Log("Successfully updated item info to server");
                    }
                    else
                    {
                        Debug.LogError("Update item to server failed: " + request.error);
                    }
                }
            )
        );
    }

    /// <summary>
    /// Sends UpdateItemRequest to server, callback after recieve result from server
    /// </summary>
    /// <param name="userID">Unique id for the user</param>
    /// <param name="itemID">Unique id for the inventory item that is being updated</param>
    /// <param name="amount">Quantity changed for the item</param>
    /// <param name="callback">Function to run after recieved result from server</param>
    /// <returns></returns>
    IEnumerator UpdateItemRequest(string userID, string itemID, int amount, Action<UnityWebRequest> callback)
    {
        string action = "";
        if (amount > 0)
        {
            action = "/inc/";
        }
        else
        {
            action = "/dec/";
        }

        UpdateItemQuantity item = new UpdateItemQuantity(amount);
        string item_json = JsonUtility.ToJson(item);

        using (var request = new UnityWebRequest(GlobalVariables.serverAccessBaseURL + "/api/" + userID + action + itemID, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(item_json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }

    /// <summary>
    /// Load full inventory data from server
    /// </summary>
    /// <param name="userID">Unique id for the user</param>
    public void LoadInventoryFromServer(string userID)
    {
        if (string.IsNullOrEmpty(userID)) {
            Debug.Log("No userID information, change will not be loaded from server");
            return;
        }
        StartCoroutine(
            GetInventoryRequest(userID,
                (UnityWebRequest request) =>
                {
                    // call to load inventory information after recieved data from get request
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        string data = request.downloadHandler.text;
                        print("Load inventory from server recieved: " + data);
                        if (String.IsNullOrEmpty(data))
                        {
                            return;
                        }
                        var inventory = JsonUtility.FromJson<ServerInventoryData>(
                            data
                        );
                        inventoryManager.UpdateInventory(inventory);
                        Debug.Log("Successfully loaded inventory from server");
                    }
                    else
                    {
                        Debug.LogError("Load inventory from server failed: " + request.error);
                    }
                }
            )
        );
    }

    /// <summary>
    /// Send get request to server to retrieve inventory information
    /// </summary>
    /// <param name="userID">Unique id for the user</param>
    /// <param name="callback">Function to run after recieved result from server</param>
    /// <returns></returns>
    IEnumerator GetInventoryRequest(string userID, Action<UnityWebRequest> callback)
    {
        // send get request to server, triggers callback after response recieved
        using (UnityWebRequest request = UnityWebRequest.Get(GlobalVariables.serverAccessBaseURL + "/api/" + userID + "/all"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }

    /// <summary>
    /// Regularily loads up to date inventory information from server
    /// </summary>
    /// <param name="userID">Unique id for user in database</param>
    public void RegularUpdateInventory(string userID)
    {
        StartCoroutine(CheckUpdatesForInventory(userID));
    }
    IEnumerator CheckUpdatesForInventory(string userID)
    {
        while (true)
        {
            LoadInventoryFromServer(userID);
            yield return new WaitForSeconds(updateInterval);
        }
    }
}
