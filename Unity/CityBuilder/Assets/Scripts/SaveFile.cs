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
    [Range(0, 10)]
    public int saveDataIndex = 1;
    public MapDataManager mapDataManager;

    /// <summary>
    /// Calls WriteToFile to save map data to local file
    /// </summary>
    /// <param name="dataToSave">Data to be saved</param>
    public void SaveDataLocal(string dataToSave)
    {
        if (WriteToFile(saveName + saveDataIndex, dataToSave))
        {
            Debug.Log("Successfully saved data to file");
        }
    }

    /// <summary>
    /// Saves data to local file: name
    /// </summary>
    /// <param name="name">Filename to store data</param> 
    /// <param name="content">Data be be stored</param>
    /// <returns>Whether save is successful</returns>
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

    /// <summary>
    /// Calls PostRequestServer and logs whether save is successful after recieved result from server
    /// </summary>
    /// <param name="mapID">Unique id for map in database</param>
    /// <param name="dataToSave">Data to be saved</param>
    public void SaveDataServer(string mapID, string dataToSave)
    {
        if (string.IsNullOrEmpty(mapID)) {
            Debug.Log("No mapID information, map data will not be saved to server");
            return;
        }
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

    /// <summary>
    /// Send post request to server to save map information, triggers callback after response recieved
    /// </summary>
    /// <param name="mapID">Unique id for map in database</param>
    /// <param name="data">Data to be saved</param>
    /// <param name="callback">Function to run after recieved result from server</param>
    /// <returns></returns>
    public IEnumerator PostRequestServer(string mapID, string data, Action<UnityWebRequest> callback)
    {
        using (var request = new UnityWebRequest(GlobalVariables.serverAccessBaseURL + "/api/" + mapID + "/savemap", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }

    /// <summary>
    /// Calls ReadFromFile to load map data from local file
    /// </summary>
    /// <returns>Data loaded from file</returns>
    public string LoadDataLocal()
    {
        string data = "";
        if (ReadFromFile(saveName + saveDataIndex, out data))
        {
            Debug.Log("Successfully loaded data from file");
        }
        return data;
    }

    /// <summary>
    /// Reads data from local file: name
    /// </summary>
    /// <param name="name">Filename to read data from</param>
    /// <param name="content">Data read from file</param>
    /// <returns>Whether read is successful</returns>
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

    /// <summary>
    /// Calls GetRequestServer and calls ReDrawGameMap in MapDataManager to redraw the map after recieved data from server
    /// </summary>
    /// <param name="mapID">Unique id for map in database</param>
    public void LoadDataServer(string mapID)
    {
        if (string.IsNullOrEmpty(mapID)) {
            Debug.Log("No mapID, map data will not be loaded from server");
            return;
        }
        StartCoroutine(
            GetRequestServer(mapID,
                (UnityWebRequest request) =>
                {
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

    /// <summary>
    /// Send get request to server to get map information, triggers callback after response recieved
    /// </summary>
    /// <param name="mapID">Unique id for map in database</param>
    /// <param name="callback">Function to run after recieved result from server</param>
    public IEnumerator GetRequestServer(string mapID, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GlobalVariables.serverAccessBaseURL + "/api/" + mapID + "/map"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }
}
