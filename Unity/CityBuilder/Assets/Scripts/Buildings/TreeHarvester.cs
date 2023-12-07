using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The TreeHarvester class is attached to all objects that enable the user to harvest trees. <br/>
/// This class inherits PlaceableObject and implements IHarvester.
/// </summary>
public class TreeHarvester : PlaceableObject, IHarvester
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
    /// Calls AddHarvester to add a tree harvester to the HarvesterManager when this object is initially placed
    /// </summary>
    public override void OnPlace()
    {
        AddHarvester();
    }
    /// <summary>
    /// Calls RemoveHarvester to remove a tree harvester from the HarvesterManager when this object is deleted
    /// </summary>
    public override void OnDelete()
    {
        isActive = false;
        RemoveHarvester();
    }
    public void AddHarvester()
    {
        HarvesterManager.harvesterManager.AddTreeHarvester();
    }

    public void RemoveHarvester()
    {
        HarvesterManager.harvesterManager.RemoveTreeHarvester();
    }
}
