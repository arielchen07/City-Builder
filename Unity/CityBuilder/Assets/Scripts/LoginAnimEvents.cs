using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class contains a function called by the animator of the login menu. <br/>
/// The game scene is loaded when the user successfully logs in at the end of the login animation.
/// </summary>
public class LoginAnimEvents : MonoBehaviour
{
    public void LoadGame(){
        SceneManager.LoadScene("MainScene");
    }
}
