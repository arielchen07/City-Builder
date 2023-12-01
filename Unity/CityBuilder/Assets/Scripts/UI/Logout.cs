using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    //[SerializeField] private string logoutEndpoint = "http://localhost:3000/api/";
    //private string logoutEndpoint = GlobalVariables.serverAccessBaseURL + "/api/";

    [SerializeField] private Button logoutButton;
    [SerializeField] private MapDataManager mapManager;
    public GameObject logoutCover;
    // [SerializeField] private Transform loginBackgroundTransform; 
    // [SerializeField] private Transform loginWindowTransform;
    public void OnLogoutClick()
    {
        logoutCover.SetActive(true);
        logoutButton.interactable = false;
        print("map id: " + GlobalVariables.MapID);

        if (string.IsNullOrEmpty(GlobalVariables.MapID))
        {
            Debug.Log("No valid MapID, map will not be saved");
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


                        //StartCoroutine(MoveLoginWindowDown());

                        //Application.Quit();

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
    private IEnumerator TryLogout(Action<UnityWebRequest> callback)
    {
        string logoutURL = GlobalVariables.serverAccessBaseURL + "/api/" + GlobalVariables.UserID + "/logout";
        print("logout url: " + logoutURL);

        using (var request = new UnityWebRequest(logoutURL, "POST"))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }



//    private IEnumerator MoveLoginWindowDown()
//     {
//         yield return new WaitForSeconds(2);
//         Vector3 startPosition1 = loginWindowTransform.position;
//         Vector3 endPosition1 = startPosition1 + new Vector3(0, -700, 0);  
//         Vector3 startPosition2 = loginBackgroundTransform.position;
//         Vector3 endPosition2 = startPosition2 + new Vector3(0, 700, 0);  

//         float duration = 1.0f;  
//         float elapsedTime = 0;

//         while (elapsedTime < duration)
//         {
//             loginWindowTransform.position = Vector3.Lerp(startPosition1, endPosition1, (elapsedTime / duration));
//             elapsedTime += Time.deltaTime;
//             loginBackgroundTransform.position = Vector3.Lerp(startPosition2, endPosition2, (elapsedTime / duration));
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }

//         // loginWindowTransform.position = endPosition1;  
//         loginBackgroundTransform.position = endPosition2;
//         logoutButton.interactable = true;
//         mapManager.ClearGameMap();

//     }
}

