using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private string authenticationEndpoint = "http://localhost:3000/api/login";
    [SerializeField] private string registrationEndpoint = "http://localhost:3000/api/register"; 
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signupButton;  
    [SerializeField] private TMP_InputField usernameInputField;  
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Transform loginWindowTransform;  

    [SerializeField] private Transform loginBackgroundTransform;  

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
    

    public void OnLoginClick()
    {
        alertText.text = "Signing in ...";
        loginButton.interactable = false;
        StartCoroutine(TryLogin());
    }
    public void OnSignupClick()  
    {
        alertText.text = "Signing up ...";
        signupButton.interactable = false;
        StartCoroutine(TrySignup());
    }


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
            yield break;  
        }
         
        string jsonData = JsonUtility.ToJson(loginData, true);  
        Debug.Log("Sending JSON Data: \n" + jsonData);
 
        UnityWebRequest request = new UnityWebRequest(authenticationEndpoint, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.timeout = 10;

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            
            if (!string.IsNullOrEmpty(request.downloadHandler.text))
            {
                alertText.text = "Welcome";
                loginButton.interactable = false;

              
                StartCoroutine(MoveLoginWindowUp());
            }
            else
            {
                alertText.text = "Invalid Credentials";
                loginButton.interactable = true;
            }
        }
        else
        {
            alertText.text = "Incorrect";
            loginButton.interactable = true;
        }
    }

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
            yield break;  
        }
        
        string jsonData = JsonUtility.ToJson(signupData, true);  
        Debug.Log("Sending JSON Data: \n" + jsonData);

        using (UnityWebRequest request = UnityWebRequest.Post(registrationEndpoint, jsonData))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 10;

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (!string.IsNullOrEmpty(request.downloadHandler.text))
                {
                    alertText.text = "Signup Successful";
                    signupButton.interactable = false;
                    StartCoroutine(MoveLoginWindowUp());
                }
                else
                {
                    alertText.text = "Signup Failed";
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

    

    private IEnumerator MoveLoginWindowUp()
    {
        yield return new WaitForSeconds(2);
        Vector3 startPosition1 = loginWindowTransform.position;
        Vector3 endPosition1 = startPosition1 + new Vector3(0, 700, 0);  
        Vector3 startPosition2 = loginBackgroundTransform.position;
        Vector3 endPosition2 = startPosition2 + new Vector3(0, -700, 0);  

        float duration = 1.0f;  
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            loginWindowTransform.position = Vector3.Lerp(startPosition1, endPosition1, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            loginBackgroundTransform.position = Vector3.Lerp(startPosition2, endPosition2, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        loginWindowTransform.position = endPosition1;  
        loginBackgroundTransform.position = endPosition2;
    }
}
