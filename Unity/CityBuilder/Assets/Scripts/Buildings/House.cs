using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
    void Start()
    {
        currentlyColliding = new List<GameObject>();
        adjacentTiles = new List<GameObject>();
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
        UpdatePopulation();
    }
    void Update() {
        currentlyColliding = GetCollidingTiles();
        adjacentTiles = GetAdjacentTiles();
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
    public void UpdatePopulation(){
        powerX = powerAllocated / powerCost;
        waterX = waterAllocated / waterCost;
        sewageX = sewageAllocated / sewageCost;
        gasX = gasAllocated / gasCost;
        internetX = internetAllocated / internetCost;
        float utilMod = basePopulation / 10;
        population = (basePopulation / 2) + (int)(utilMod * powerX) + (int)(utilMod * waterX) + (int)(utilMod * sewageX) + (int)(utilMod * gasX) + (int)(utilMod * internetX);
    }

    public void ResetUtilities(){
        powerAllocated = 0;
        waterAllocated = 0;
        sewageAllocated = 0;
        internetAllocated = 0;
        gasAllocated = 0;
        UpdatePopulation();
    }
}
