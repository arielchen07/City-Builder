using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The House class is attached to all housing type objects<br/>
/// This class inherits PlaceableObject
/// </summary>

public class House : PlaceableObject
{
    public int basePopulation;
    public int population;
    public int powerCost;
    public int powerAllocated;
    public int waterCost;
    public int waterAllocated;
    public int sewageCost;
    public int sewageAllocated;
    public int gasCost;
    public int gasAllocated;
    public int internetCost;
    public int internetAllocated;
    public float powerX = 1f;
    public float waterX = 1f;
    public float sewageX = 1f;
    public float gasX = 1f;
    public float internetX = 1f;
    public float pollutionTreshold;
    public float pollutionModifier;
    void Start()
    {
        currentlyColliding = new List<GameObject>();
        adjacentTiles = new List<GameObject>();
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
        UpdatePopulation();
    }
    void Update()
    {
        currentlyColliding = GetCollidingTiles();
        adjacentTiles = GetAdjacentTiles();
        canBePlaced = CanBePlaced();
        if (isHovering)
        {
            if (canBePlaced)
            {
                HoverValid.SetActive(true);
                HoverInvalid.SetActive(false);
            }
            else
            {
                HoverValid.SetActive(false);
                HoverInvalid.SetActive(true);
            }
        }
        else
        {
            HoverValid.SetActive(false);
            HoverInvalid.SetActive(false);
        }
    }
    /// <summary>
    /// Get the current population of this house as a string
    /// </summary>
    /// <returns>A string representing the house's current population</returns>
    public string GetPopulation()
    {
        return population.ToString();
    }
    /// <summary>
    /// Get the current power allocated to the house out of its total cost
    /// </summary>
    /// <returns>A string formatted in the following way<br/>
    /// powerAllocated / powerCost<br/>
    /// i.e 5 / 10<br/>
    /// </returns>
    public string GetPower()
    {
        return powerAllocated.ToString() + " / " + powerCost.ToString();
    }
    /// <summary>
    /// Get the current water allocated to the house out of its total cost
    /// </summary>
    /// <returns>A string formatted in the following way<br/>
    /// waterAllocated / waterCost<br/>
    /// i.e 5 / 10<br/>
    /// </returns>
    public string GetWater()
    {
        return waterAllocated.ToString() + " / " + waterCost.ToString();
    }
    /// <summary>
    /// Get the current sewage allocated to the house out of its total cost
    /// </summary>
    /// <returns>A string formatted in the following way<br/>
    /// sewageAllocated / sewageCost<br/>
    /// i.e 5 / 10<br/>
    /// </returns>
    public string GetSewage()
    {
        return sewageAllocated.ToString() + " / " + sewageCost.ToString();
    }
    /// <summary>
    /// Get the current gas allocated to the house out of its total cost
    /// </summary>
    /// <returns>A string formatted in the following way<br/>
    /// gasAllocated / gasCost<br/>
    /// i.e 5 / 10<br/>
    /// </returns>
    public string GetGas()
    {
        return gasAllocated.ToString() + " / " + gasCost.ToString();
    }
    /// <summary>
    /// Get the current internet allocated to the house out of its total cost
    /// </summary>
    /// <returns>A string formatted in the following way <br/>
    /// internetAllocated / internetCost <br/>
    /// i.e 5 / 10 <br/>
    /// </returns>
    public string GetInternet()
    {
        return internetAllocated.ToString() + " / " + internetCost.ToString();
    }
    /// <summary>
    /// Recalculates the population of this house. <br/>
    /// With no utilities, a house provides half of its base population. <br/>
    /// Each utility contributes to 10% of a building's population. <br/>
    /// Fulfilling all 5 utilities will make the house provide its base population. <br/>
    /// </summary>/
    public void UpdatePopulation()
    {
        powerX = powerAllocated / powerCost;
        waterX = waterAllocated / waterCost;
        sewageX = sewageAllocated / sewageCost;
        gasX = gasAllocated / gasCost;
        internetX = internetAllocated / internetCost;
        float utilMod = basePopulation / 10;
        population = (basePopulation / 2) + (int)(utilMod * powerX) + (int)(utilMod * waterX) + (int)(utilMod * sewageX) + (int)(utilMod * gasX) + (int)(utilMod * internetX);
        if (PollutionManager.pollutionManager != null)
        {
            population -= (int)(Mathf.Clamp(PollutionManager.pollutionManager.pollutionIndex - pollutionTreshold, 0, Mathf.Infinity) * pollutionModifier);
        }
    }
    /// <summary>
    /// This function is called by the UtilitiesManager. <br/>
    /// All utilities are reset before being recalculated in UtilitiesManager. <br/>
    /// The population is then updated accordingly. <br/>
    /// </summary>/
    public void ResetUtilities()
    {
        powerAllocated = 0;
        waterAllocated = 0;
        sewageAllocated = 0;
        internetAllocated = 0;
        gasAllocated = 0;
        UpdatePopulation();
    }
}
