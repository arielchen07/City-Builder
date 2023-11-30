using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHarvester : PlaceableObject, IHarvester
{
    void Start()
    {
        currentlyColliding = new List<GameObject>();
        adjacentTiles = new List<GameObject>();
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
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
    public override void OnPlace(){
        UtilitiesManager.utilManager.UpdateUtilities();
        AddHarvester();
    }
    public override void OnDelete(){
        isActive = false;
        UtilitiesManager.utilManager.UpdateUtilities();
        HarvesterManager.harvesterManager.RemoveTreeHarvester();
    }
    public void AddHarvester(){
        HarvesterManager.harvesterManager.AddRockHarvester();
    }
    public void RemoveHarvester(){
        HarvesterManager.harvesterManager.RemoveRockHarvester();
    }
}
