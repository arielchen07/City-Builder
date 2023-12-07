using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SewagePlant class is attached to all objects that provide the sewage utility. <br/>
/// This class inherits from PlaceableObject and implements IProvider
/// </summary>
public class SewagePlant : PlaceableObject, IProvider
{
    public GameObject serviceRange;
    public int maxSewage;
    public int currSewageAllocated;

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
        currSewageAllocated = 0;
        foreach (Collider col in cols)
        {
            if (col.gameObject.TryGetComponent<House>(out var h))
            {
                if (currSewageAllocated == maxSewage)
                {
                    break;
                }
                int sewageNeeded = h.sewageCost - h.sewageAllocated;
                if (currSewageAllocated + sewageNeeded <= maxSewage)
                {
                    currSewageAllocated += sewageNeeded;
                    h.sewageAllocated = h.sewageCost;
                }
                else
                {
                    h.sewageAllocated = maxSewage - currSewageAllocated;
                    currSewageAllocated = maxSewage;
                }
                h.UpdatePopulation();
            }
        }
    }
}

