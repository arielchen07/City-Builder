using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : PlaceableObject, IProvider
{
    public GameObject serviceRange;
    public int maxPower;
    public int currPowerAllocated;
    public List<GameObject> housesInRange;

    void Start()
    {
        currentlyColliding = new List<GameObject>();
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
        housesInRange = new List<GameObject>();
    }
    void Update() {
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

    public void Allocate() {
        BoxCollider serviceRangeCollider = serviceRange.GetComponent<BoxCollider>();
        Vector3 worldCenter = serviceRangeCollider.transform.TransformPoint(serviceRangeCollider.center);
        Vector3 worldHalfExtents = Vector3.Scale(serviceRangeCollider.size, serviceRangeCollider.transform.lossyScale) * 0.5f;
        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, serviceRangeCollider.transform.rotation);
        currPowerAllocated = 0;
        
        foreach (Collider col in cols) {
            if (col.gameObject.TryGetComponent<House>(out var h)) {
                if(currPowerAllocated == maxPower) {
                    break;
                }
                int powerNeeded = h.powerCost - h.powerAllocated;
                if (currPowerAllocated + powerNeeded <= maxPower) {     //can fill up
                    currPowerAllocated += powerNeeded;
                    h.powerAllocated = h.powerCost;
                } else {                                                //can't fully fill up
                    h.powerAllocated = maxPower - currPowerAllocated;   // the remaining left in this distributor
                    currPowerAllocated = maxPower;
                }
                h.UpdatePopulation();
            }
        }
    }
}
