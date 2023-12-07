using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The GasDistributor class is attached to any building that provides the gas utility.
/// This class inherits from PlaceableObject and implements IProvider
/// </summary>
public class GasDistributor : PlaceableObject, IProvider
{
    public GameObject serviceRange;
    public int maxGas;
    public int currGasAllocated;

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
        currGasAllocated = 0;
        foreach (Collider col in cols)
        {
            if (col.gameObject.TryGetComponent<House>(out var h))
            {
                if (currGasAllocated == maxGas)
                {
                    break;
                }
                int gasNeeded = h.gasCost - h.gasAllocated;
                if (currGasAllocated + gasNeeded <= maxGas)
                {
                    currGasAllocated += gasNeeded;
                    h.gasAllocated = h.gasCost;
                }
                else
                {
                    h.gasAllocated = maxGas - currGasAllocated;
                    currGasAllocated = maxGas;
                }
                h.UpdatePopulation();
            }
        }
    }
}
