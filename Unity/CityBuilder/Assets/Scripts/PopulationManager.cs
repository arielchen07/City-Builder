using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationManager : MonoBehaviour
{  
    public int population;
    public int basePopulation;
    public static PopulationManager populationManager;
    public List<House> houses;
    public Text populationText;
    public float timer;
    public float interval;
    private void Awake(){
        if (populationManager != null && populationManager != this){
            Destroy(this);
        } else {
            populationManager = this;
        }
    }
 // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer){
            timer = Time.time + interval;
            UpdatePopulation();
        }
    }

    public void UpdatePopulation(){
        UtilitiesManager.utilManager.UpdateUtilities();
        population = 0;
        basePopulation = 0;
        foreach(House h in houses){
            population += h.population;
            basePopulation += h.basePopulation;
        }
        populationText.text = population.ToString();
    }

    public void UpdateHouses(){
        houses = new List<House>(FindObjectsOfType<House>());
    }
}
