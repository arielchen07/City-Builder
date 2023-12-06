using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class InventoryToServer : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public float updateInterval = 5.0f; // Checks for server inventory data every 5 seconds

    //string serverAccessEndpoint = "http://localhost:3000/api/";
    //string serverAccessEndpoint = GlobalVariables.serverAccessBaseURL + "/api/";

    public void CreateItemToServer(string userID, string category, string name, int quantity = 2)
    {
        StartCoroutine(
            CreateItemRequest(userID, category, name, quantity,
                (UnityWebRequest request) =>
                {
                    // call to load game objects after recieved data from get request
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        string data = request.downloadHandler.text;
                        print("Create item read from server recieved: " + data);
                        if (String.IsNullOrEmpty(data))
                        {
                            return;
                        }
                        var item = JsonUtility.FromJson<ServerItemData>(
                            data
                        );
                        print(item.name);
                        inventoryManager.UpdateInventoryItem(item);

                        Debug.Log("Successfully updated new item info to server");
                        //LoadInventoryFromServer(userID);
                    }
                    else
                    {
                        Debug.LogError("Update new item to server failed: " + request.error);
                    }
                }
            )
        );
    }

    IEnumerator CreateItemRequest(string userID, string category, string name, int quantity, Action<UnityWebRequest> callback)
    {
        // Currently using hard coded URL to store to a specific map id of a specific user in database
        // will change this later
        InitServerItemData item = new InitServerItemData(category, name, quantity);
        string item_json = JsonUtility.ToJson(item);

        using (var request = new UnityWebRequest(GlobalVariables.serverAccessBaseURL + "/api/" + userID + "/create", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(item_json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }
    public void UpdateItemToServer(string userID, string itemID, int amount)
    {
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
                        print(item.name);
                        inventoryManager.UpdateInventoryItem(item);

                        Debug.Log("Successfully updated item info to server");
                        //LoadInventoryFromServer(userID);
                    }
                    else
                    {
                        Debug.LogError("Update item to server failed: " + request.error);
                    }
                }
            )
        );
    }

    IEnumerator UpdateItemRequest(string userID, string itemID, int amount, Action<UnityWebRequest> callback)
    {
        // Currently using hard coded URL to store to a specific map id of a specific user in database
        // will change this later
        string action = "";
        if (amount > 0)
        {
            action = "/inc/";
        } else
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

    public void LoadInventoryFromServer(string userID)
    {
        StartCoroutine(
            GetInventoryRequest(userID,
                (UnityWebRequest request) =>
                {
                    // call to load game objects after recieved data from get request
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
                        foreach (var item in inventory.items)
                        {
                            print(item.name + ", " + item.quantity + ", " + item._id);
                        }
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

    IEnumerator GetInventoryRequest(string userID, Action<UnityWebRequest> callback)
    {
        // Currently using hard coded URL to load from a specific map id of a specific user in database
        // will change this later

        // send get request to server, triggers callback after response recieved
        using (UnityWebRequest request = UnityWebRequest.Get(GlobalVariables.serverAccessBaseURL + "/api/" + userID + "/all"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }

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
