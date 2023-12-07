using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    [SerializeField] private Button logoutButton;
    [SerializeField] private MapDataManager mapManager;
    public GameObject logoutCover;

    /// <summary>
    /// Saves map information to server, on success, calls LogoutAfterSave
    /// </summary>
    public void OnLogoutClick()
    {
        logoutCover.SetActive(true);
        logoutButton.interactable = false;
        print("map id: " + GlobalVariables.MapID);

        if (string.IsNullOrEmpty(GlobalVariables.MapID))
        {
            Debug.Log("No MapID information, map will not be saved");
            logoutButton.interactable = true;
            return;
        }

        string mapData = mapManager.SerializeAllGameObjects();
        print("mapdata: " + mapData);
        MapRequestBody data = new MapRequestBody(mapData);
        var mapDataRequestBody = JsonUtility.ToJson(data);

        StartCoroutine(
            mapManager.saveSystem.PostRequestServer(GlobalVariables.MapID, mapDataRequestBody,
                (UnityWebRequest request) =>
                {
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log("Logout: Successfully saved data to server");
                        LogoutAfterSave();
                    }
                    else
                    {
                        Debug.LogError("Logout: Save to server failed: " + request.error);
                    }
                }
            )
        );

    }

    /// <summary>
    /// Return to the login screen, reset user information variables
    /// </summary>
    public void LogoutAfterSave()
    {
        StartCoroutine(
            TryLogout(
                (UnityWebRequest request) =>
                {
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log("Successfully logout on server");

                        // Reset or clear session-related data
                        GlobalVariables.UserID = string.Empty;
                        GlobalVariables.MapID = string.Empty;
                        GlobalVariables.IsNewUser = true;

                        // Return to the login or main scene
                        SceneManager.LoadScene("LoginScene"); // Change "LoginScene" to your actual scene name
                    }
                    else
                    {
                        Debug.LogError("Failed to logout: " + request.error);
                    }

                }
            )
        );
        
    }

    /// <summary>
    /// Sends logout post request to server
    /// </summary>
    /// <param name="callback">Function to run after recieved result from server</param>
    /// <returns></returns>
    private IEnumerator TryLogout(Action<UnityWebRequest> callback)
    {
        string logoutURL = GlobalVariables.serverAccessBaseURL + "/api/" + GlobalVariables.UserID + "/logout";

        using (var request = new UnityWebRequest(logoutURL, "POST"))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }
}

