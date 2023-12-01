using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;

    void Start(){
        menu.SetActive(false);
    }
    public void OpenMenu(){
        menu.SetActive(true);
    }
    public void CloseMenu(){
        menu.SetActive(false);
    }
}
