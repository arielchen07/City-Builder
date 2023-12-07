using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The TownHallController is attached to the Town Hall object. <br/>
/// Increase the number of coins relative to the total population at a set interval.
/// </summary>
public class TownHallController : MonoBehaviour
{
    public float interval;
    public float timer;
    public int coinPerCapita;
    void Start()
    {
        timer = Time.time;
    }

    void Update()
    {
        if (GetComponent<PlaceableObject>().hasBeenPlaced)
        {
            if (Time.time > timer)
            {
                timer += interval;
                GenerateCoins();
            }
        }
    }

    /// <summary>
    /// Gets the current population from PopulationManager and increases the coin count.
    /// </summary>
    void GenerateCoins()
    {
        MoneyManager.moneyManager.IncreaseCoins(PopulationManager.populationManager.population * coinPerCapita);
    }
}
