using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The InternetDistributor class is attached to all Internet providing objects.
/// This class inherits from PlaceableObject and implemented IProvider
/// </summary>
public class InternetDistributor : PlaceableObject, IProvider
{
    public GameObject serviceRange;
    public int maxInternet;
    public int currInternetAllocated;

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
        currInternetAllocated = 0;
        foreach (Collider col in cols)
        {
            if (col.gameObject.TryGetComponent<House>(out var h))
            {
                if (currInternetAllocated == maxInternet)
                {
                    break;
                }
                int internetNeeded = h.internetCost - h.internetAllocated;
                if (currInternetAllocated + internetNeeded <= maxInternet)
                {
                    currInternetAllocated += internetNeeded;
                    h.internetAllocated = h.internetCost;
                }
                else
                {
                    h.internetAllocated = maxInternet - currInternetAllocated;
                    currInternetAllocated = maxInternet;
                }
                h.UpdatePopulation();
            }
        }
    }
}
