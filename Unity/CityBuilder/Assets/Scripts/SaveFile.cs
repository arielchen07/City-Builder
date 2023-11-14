using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public class SaveFile : MonoBehaviour
{
    public string saveName = "SaveData_";
    public string tileSaveName = "TileData_";
    [Range(0, 10)]
    public int saveDataIndex = 1;
    public MapDataManager mapDataManager;
    public void SaveDataLocal(string dataToSave)
    {
        if (WriteToFile(saveName + saveDataIndex, dataToSave))
        {
            Debug.Log("Successfully saved data to file");
        }
    }
    public bool WriteToFile(string name, string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, name);
        try
        {
            File.WriteAllText(fullPath, content);
            Debug.Log("Filepath: " + fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving to a file " + e.Message);
        }
        return false;
    }
    public void SaveDataServer(string mapID, string dataToSave, bool logout = false)
    {
        MapRequestBody data = new MapRequestBody(dataToSave);
        var mapDataRequestBody = JsonUtility.ToJson(data);
        StartCoroutine(
            PostRequestServer(mapID, mapDataRequestBody,
                (UnityWebRequest request) =>
                {
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log("Successfully saved data to server");
                    }
                    else
                    {
                        Debug.LogError("Save to server failed: " + request.error);
                    }
                }
            )
        );
    }
    public static IEnumerator PostRequestServer(string mapID, string data, Action<UnityWebRequest> callback)
    {
        // Currently using hard coded URL to store to a specific map id of a specific user in database
        // will change this later
        using (var request = new UnityWebRequest("http://localhost:3000/api/" + mapID + "/savemap", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }
    public string LoadDataLocal()
    {
        string data = "";
        if (ReadFromFile(saveName + saveDataIndex, out data))
        {
            Debug.Log("Successfully loaded data from file");
        }
        return data;
    }
    private bool ReadFromFile(string name, out string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, name);
        try
        {
            content = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error when loading the file " + e.Message);
            content = "";
        }
        return false;
    }
    public void LoadDataServer(string mapID)
    {
        StartCoroutine(
            GetRequestServer(mapID,
                (UnityWebRequest request) =>
                {
                    // call to load game objects after recieved data from get request
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        string data = request.downloadHandler.text;
                        print("Readfromserver recieved: " + data);
                        if (String.IsNullOrEmpty(data))
                            return;
                        var mapRequestBody = JsonUtility.FromJson<MapRequestBody>(
                            data
                        );
                        Debug.Log("Successfully loaded data from server");
                        mapDataManager.ReDrawGameMap(mapRequestBody.mapData);
                    }
                    else
                    {
                        Debug.LogError("Load from server failed: " + request.error);
                    }
                }
            )
        );
    }
    public static IEnumerator GetRequestServer(string mapID, Action<UnityWebRequest> callback)
    {
        // Currently using hard coded URL to load from a specific map id of a specific user in database
        // will change this later
        // send get request to server, triggers callback after response recieved
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/api/" + mapID + "/map"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }
}