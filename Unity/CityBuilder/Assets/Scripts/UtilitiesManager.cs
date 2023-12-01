using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UtilitiesManager : MonoBehaviour
{
    public static UtilitiesManager utilManager {get; private set;}
    private void Awake(){
        if (utilManager != null && utilManager != this){
            Destroy(this);
        } else {
            utilManager = this;
        }
    }
    public void Start(){
        UpdateUtilities();
    }

    public void UpdateUtilities(){
        List<GameObject> objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Object"));
        List<GameObject> houses = new List<GameObject>();
        List<GameObject> providers = new List<GameObject>();
        foreach (GameObject g in objects){
            if(g.GetComponent<PlaceableObject>().isActive){
                if(g.TryGetComponent<House>(out var h)){
                    houses.Add(g);
                }
                else if(g.TryGetComponent<IProvider>(out var p)){
                    providers.Add(g);
                }
            }
        }
        foreach (GameObject h in houses){
            h.GetComponent<House>().ResetUtilities();
        }
        foreach (GameObject p in providers){
            p.GetComponent<IProvider>().Allocate();
        }
    }
}
