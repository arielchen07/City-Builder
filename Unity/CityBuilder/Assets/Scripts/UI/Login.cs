using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Login;
public static class GlobalVariables
{
    public static string UserID { get; set; }
    public static string MapID {get; set;}
    public static bool IsNewUser { get; set;}
    public static string serverAccessBaseURL { get; set;}
}

public class Login : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signupButton;  
    [SerializeField] private TMP_InputField usernameInputField;  
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Toggle serverToggle;
    public CloudManager cloudManager;
    public Animator anim;

    [System.Serializable]

    public class LoginData
    {
        public string email;
        public string password;
    }
    public class SignupData
    {
        public string name;
        public string email;
        public string password;
    }

    [System.Serializable]
    public class MapData
    {
        public string mapData;
        public string mapName;
        

    }
    [System.Serializable]
    public class LoginResponse
    {
        public string userID;
        public string mapID;

    }

    [System.Serializable]   
    public class SignupResponse
    {
        public string userID;
    }

    public class CreateMapResponse{
        public string mapID;
    }

    private void Start()
    {
        usernameInputField.text = "";  // Clear the username field
        emailInputField.text = "";     // Clear the email field
        passwordInputField.text = "";  // Clear the password field
        loginButton.interactable = true;
        signupButton.interactable = true;
        alertText.text = "Welcome";
    }

    private void Update(){
        if(serverToggle.isOn){
            GlobalVariables.serverAccessBaseURL = "https://unity-game-server.onrender.com";
        } else {
            GlobalVariables.serverAccessBaseURL = "http://localhost:3000";
        }
    }
    public void OnLoginClick()
    {
        alertText.text = "Signing in ...";
        StartCoroutine(TryLogin());
        
    }
    public void OnSignupClick()  
    {
        Debug.Log("serever port: " + GlobalVariables.serverAccessBaseURL + "/api/register");
        alertText.text = "Signing up ...";
        StartCoroutine(TrySignup());
        
    }

    /// <summary>
    /// Verifies email and password, on success, gets mapID from server to prepare for load map 
    /// </summary>
    /// <returns></returns>
    private IEnumerator TryLogin()
    {
        LoginData loginData = new LoginData
        {
            email = emailInputField.text,
            password = passwordInputField.text
        };

        if (string.IsNullOrEmpty(loginData.email) || string.IsNullOrEmpty(loginData.password))
        {
            Debug.LogError("Email or password is empty");
            Debug.Log(loginData.email);
            Debug.Log(loginData.password);
            alertText.text = "Some fields are empty";
            loginButton.interactable = true;
            yield break;  
        }
         
        string jsonData = JsonUtility.ToJson(loginData, true);  
        Debug.Log("Sending JSON Data: \n" + jsonData);
 
        UnityWebRequest request = new UnityWebRequest(GlobalVariables.serverAccessBaseURL + "/api/login", "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.timeout = 300;

        yield return request.SendWebRequest();

        print(GlobalVariables.serverAccessBaseURL + "/api/login");
        print(request.result);

        if (request.result == UnityWebRequest.Result.Success)
        {
            
            if (!string.IsNullOrEmpty(request.downloadHandler.text))
            {
                LoginResponse login = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
                GlobalVariables.UserID = login.userID;
                string mapIDName = login.mapID;
                int colonIndex = mapIDName.IndexOf(':');
                if (colonIndex != -1) {
                    string mapID = mapIDName.Substring(0, colonIndex);
                    GlobalVariables.MapID = mapID;
                }else{
                    Debug.Log("Fail to catch MapID");
                    yield break;
                }
                
                GlobalVariables.IsNewUser = false;

                alertText.text = "Welcome";
        
                loginButton.interactable = false;
 
                StartLoginAnimations();
            }
            else
            {
                alertText.text = "Invalid Credentials";
                loginButton.interactable = true;
            }
        }
        else
        {
            alertText.text = "Login Info Incorrect";
            loginButton.interactable = true;
        }
    }

    /// <summary>
    /// Saves new user information to server, on success, calls TryCreateMap
    /// </summary>
    /// <returns></returns>
    private IEnumerator TrySignup()
    {
        SignupData signupData = new SignupData
        {
            name = usernameInputField.text,
            email = emailInputField.text,
            password = passwordInputField.text
        };

        if (string.IsNullOrEmpty(signupData.name) || string.IsNullOrEmpty(signupData.email) || string.IsNullOrEmpty(signupData.password))
        {
            Debug.LogError("Name, email or password is empty");
            alertText.text = "Some fields are empty";
            signupButton.interactable = true;
            yield break;  
        }
        
        string jsonData = JsonUtility.ToJson(signupData, true);  
        Debug.Log("Sending JSON Data: \n" + jsonData);

        using (UnityWebRequest request = UnityWebRequest.Post(GlobalVariables.serverAccessBaseURL + "/api/register", jsonData))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 300;

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (!string.IsNullOrEmpty(request.downloadHandler.text))
                {
                    SignupResponse signup = JsonUtility.FromJson<SignupResponse>(request.downloadHandler.text);
                    GlobalVariables.UserID = signup.userID;
                    Debug.Log("UID:" + GlobalVariables.UserID);
                    alertText.text = "Signup Successful";
                    signupButton.interactable = false;

                    StartCoroutine(TryCreateMap());
                }
                else
                {
                    alertText.text = "Signup Failed, Try Again";
                    signupButton.interactable = true;
                }
            }
            else
            {
                Debug.LogError($"Error during signup: {request.error}");
                alertText.text = $"Signup Error: {request.error}";
                signupButton.interactable = true;
            }
        }
    }

    /// <summary>
    /// Creates a new map on server for the user, retrieves mapID of new map for future map saving to server
    /// </summary>
    /// <returns></returns>
    private IEnumerator TryCreateMap()
    {
        string createMapUrl = GlobalVariables.serverAccessBaseURL + "/api/" + GlobalVariables.UserID + "/createmap";
        print(createMapUrl);
        UnityWebRequest request = new UnityWebRequest(createMapUrl, "POST");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.timeout = 60;

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            {
                if (!string.IsNullOrEmpty(request.downloadHandler.text))
                {
                    CreateMapResponse createmap = JsonUtility.FromJson<CreateMapResponse>(request.downloadHandler.text);
                    GlobalVariables.MapID = createmap.mapID;
                    Debug.Log("MapID: " + GlobalVariables.MapID);
                    
                    GlobalVariables.IsNewUser = true;
                    StartLoginAnimations();
                }
                else
                {
                    alertText.text = "Create Map Failed";
                }
            }
            else
            {
                Debug.LogError($"Error during create map: {request.error}");
                alertText.text = $"Create Map Error: {request.error}";
            }

        
    }

    /// <summary>
    /// Starts cloud animation
    /// </summary>
    public void StartLoginAnimations(){
        cloudManager.OnLogin();
        anim.SetTrigger("login");
    }
}

