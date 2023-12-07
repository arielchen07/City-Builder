using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used by the settings button. <br/>
/// The functions in this class are called by the settings button and the close button on the settings window.
/// </summary>
public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;

    void Start()
    {
        menu.SetActive(false);
    }
    /// <summary>
    /// Opens the settings menu.
    /// </summary>
    public void OpenMenu()
    {
        menu.SetActive(true);
    }
    /// <summary>
    /// Closes the settings menu.
    /// </summary>
    public void CloseMenu()
    {
        menu.SetActive(false);
    }
}
