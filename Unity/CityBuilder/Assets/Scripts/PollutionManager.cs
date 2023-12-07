using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The PollutionManager class is responsible for tracking the total pollution. <br/>
/// This class is a singleton and is globally accessible.
/// </summary>
public class PollutionManager : MonoBehaviour
{
    public float pollutionIndex = 0;
    public int numTrees = 0;
    public static PollutionManager pollutionManager;
    public List<Polluter> polluters;
    public Text pollutionText;
    public float pollutionPerCapita;
    public float numTreesPerPollutionPoint;
    public float timer = 0;
    public float updateInterval;
    private void Awake(){
        if (pollutionManager != null && pollutionManager != this){
            Destroy(this);
        } else {
            pollutionManager = this;
        }
    }
    void Update()
    {
        if(Time.time > timer){
            timer = Time.time + updateInterval;
            UpdatePollutionIndex();
        }
    }
    /// <summary>
    /// Checks the entire map for trees. This function is only called once when the map is loaded. <br/>
    /// Trees that are spawned or removed afterwards directly modify the numTrees attribute to save performance.
    /// </summary>
    public void UpdateTreeCount(){
        numTrees = 0;
        Decoration[] decos = FindObjectsOfType<Decoration>();
        foreach(Decoration d in decos){
            if(d.resourceType == "wood"){
                numTrees++;
            }
        }
    }
    /// <summary>
    /// Finds all polluters on the map. This function is only called once when the map is loaded.
    /// </summary>
    public void UpdatePolluters(){
        polluters = new List<Polluter>(FindObjectsOfType<Polluter>());
    }

    /// <summary>
    /// Updates the pollution index value by getting the pollution generated from each polluter object and population. <br/>
    /// Then, decreases the pollution index based on the number of trees. <br/>
    /// Updates the pollution index text.
    /// </summary>
    public void UpdatePollutionIndex(){
        pollutionIndex = 0;
        foreach (Polluter p in polluters) {
            pollutionIndex += p.pollution;
        }
        pollutionIndex += PopulationManager.populationManager.basePopulation * pollutionPerCapita;
        pollutionIndex -= numTrees / numTreesPerPollutionPoint;
        pollutionIndex = Mathf.Clamp(pollutionIndex, 0, Mathf.Infinity);
        pollutionText.text = ((int)pollutionIndex).ToString();
    }
}
