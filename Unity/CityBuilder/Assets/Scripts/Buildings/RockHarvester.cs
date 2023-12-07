using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The RockHarvester class is attached to all buildings that enable the user to harvest rocks. <br/>
/// This class inherits PlaceableObject and implements IHarvester
/// </summary>
public class RockHarvester : PlaceableObject, IHarvester
{
    void Start()
    {
        currentlyColliding = new List<GameObject>();
        adjacentTiles = new List<GameObject>();
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
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
    /// Calls AddHarvester to add a rock harvester to the HarvesterManager when this object is initially placed
    /// </summary>
    public override void OnPlace()
    {
        AddHarvester();
    }
    /// <summary>
    /// Calls RemoveHarvester remove a rock harvester from the HarvesterManager when this object is deleted
    /// </summary>
    public override void OnDelete()
    {
        isActive = false;
        RemoveHarvester();
    }
    public void AddHarvester()
    {
        HarvesterManager.harvesterManager.AddRockHarvester();
    }
    public void RemoveHarvester()
    {
        HarvesterManager.harvesterManager.RemoveRockHarvester();
    }
}
