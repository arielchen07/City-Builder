using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallController : MonoBehaviour
{
    int totalPopulation;
    public float interval;
    public float timer;
    public int coinPerCapita;
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer) {
            timer += interval;
            GenerateCoins();
        }
    }

    void GenerateCoins(){
        totalPopulation = 0;
        House[] houses = FindObjectsOfType<House>();
        foreach(House h in houses){
            totalPopulation += h.population;
        }
        MoneyManager.moneyManager.IncreaseCoins(totalPopulation * coinPerCapita);
    }
}
