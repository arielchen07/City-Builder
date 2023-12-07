using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The UtilitiesManager class is responsible for tracking the current utility providers on the map and updating the utility values of houses. <br/>
/// This class is a singleton and is globally accessible.
/// </summary>
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

    /// <summary>
    /// Finds all houses and provider objects in the scene. Resets the utility values of all housing and redistributes the utilities provided by the providers. <br/>
    /// This function is not called at a regular interval. Instead, it is called when the map is loaded and when objects are placed or deleted. This saves performance.
    /// </summary>
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
