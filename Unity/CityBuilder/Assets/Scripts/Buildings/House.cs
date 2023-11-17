using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Dictionary<string, int> providers;
    public List<GameObject> providersInRange;
    void Start()
    {
        currentlyColliding = new List<GameObject>();
        providersInRange = new List<GameObject>();
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentlyColliding = GetCollidingTiles();
        canBePlaced = CanBePlaced();
        if (isHovering) {
            if (canBePlaced) {
                HoverValid.SetActive(true);
                HoverInvalid.SetActive(false);
            } else {
                HoverValid.SetActive(false);
                HoverInvalid.SetActive(true);
            }
        } else {
            HoverValid.SetActive(false);
            HoverInvalid.SetActive(false);
        }
    }
    public string GetPopulation(){
        return population.ToString();
    }
    public string GetPower(){
        return powerAllocated.ToString() + " / " + powerCost.ToString();
    }
    public string GetWater(){
        return waterAllocated.ToString() + " / " + waterCost.ToString();
    }
    public string GetSewage(){
        return sewageAllocated.ToString() + " / " + sewageCost.ToString();
    }
    public string GetGas(){
        return gasAllocated.ToString() + " / " + gasCost.ToString();
    }
    public string GetInternet(){
        return internetAllocated.ToString() + " / " + internetCost.ToString();
    }
    public void UpdateUtilities(){
        providers = new Dictionary<string, int>
        {
            { "power", 0 },
            { "water", 0 },
            { "sewage", 0 },
            { "internet", 0 },
            { "gas", 0 }
        };
        foreach (GameObject g in providersInRange) {
            (string, int) t = g.GetComponent<IProvider>().GetProvided();
        }
        int powerAllocated = providers["energy"];
        int waterAllocated = providers["water"];
        int sewageAllocated = providers["sewage"];
        int internetAllocated = providers["internet"];
        int gasAllocated = providers["gas"];
        powerX = powerAllocated / powerCost;
        waterX = waterAllocated / waterCost;
        sewageX = sewageAllocated / sewageCost;
        gasX = gasAllocated / gasCost;
        internetX = internetAllocated / internetCost;
        float utilMod = basePopulation / 10;
        population = (int)(basePopulation / 2) + (int)(utilMod * powerX) + (int)(utilMod * waterX) + (int)(utilMod * sewageX) + (int)(utilMod * gasX) + (int)(utilMod * internetX);
    }
}
