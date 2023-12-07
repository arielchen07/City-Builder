using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The WaterDistributor class is attached to all objects that provide the water utility. <br/>
/// This class inherits PlaceableObject and implements IProvider
/// </summary>
public class WaterDistributor : PlaceableObject, IProvider
{
    public GameObject serviceRange;
    public int maxWater;
    public int currWaterAllocated;

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

    public void Allocate()
    {
        BoxCollider serviceRangeCollider = serviceRange.GetComponent<BoxCollider>();
        Vector3 worldCenter = serviceRangeCollider.transform.TransformPoint(serviceRangeCollider.center);
        Vector3 worldHalfExtents = Vector3.Scale(serviceRangeCollider.size, serviceRangeCollider.transform.lossyScale) * 0.5f;
        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, serviceRangeCollider.transform.rotation);
        currWaterAllocated = 0;
        foreach (Collider col in cols)
        {
            if (col.gameObject.TryGetComponent<House>(out var h))
            {
                if (currWaterAllocated == maxWater)
                {
                    break;
                }
                int waterNeeded = h.waterCost - h.waterAllocated;
                if (currWaterAllocated + waterNeeded <= maxWater)
                {         //can fill up
                    currWaterAllocated += waterNeeded;
                    h.waterAllocated = h.waterCost;
                }
                else
                {                                                    //can't fully fill up
                    h.waterAllocated = maxWater - currWaterAllocated;    // the remaining left in this distributor
                    currWaterAllocated = maxWater;
                }
                h.UpdatePopulation();
            }
        }
    }
}
