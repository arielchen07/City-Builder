using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    void Start()
    {

    }

    void Update()
    {
        if(Time.time > timer){
            timer = Time.time + updateInterval;
            UpdatePollutionIndex();
        }
    }
    public void UpdateTreeCount(){
        numTrees = 0;
        Decoration[] decos = FindObjectsOfType<Decoration>();
        foreach(Decoration d in decos){
            if(d.resourceType == "wood"){
                numTrees++;
            }
        }
    }
    public void UpdatePolluters(){
        polluters = new List<Polluter>(FindObjectsOfType<Polluter>());
    }

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
