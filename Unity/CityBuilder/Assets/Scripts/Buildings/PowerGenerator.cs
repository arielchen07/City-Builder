using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : PlaceableObject, IProvider
{
    public GameObject serviceRange;
    public int maxPower;
    public int currPowerAllocated;
    // public float updateInterval = 2f;
    // public float timer = 0;
    public List<GameObject> housesInRange;

    void Start()
    {
        currentlyColliding = new List<GameObject>();
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
        housesInRange = new List<GameObject>();
    }
    void Update() {
        // if (Time.time > timer) {
        //     OnPlace();
        //     timer = Time.time + updateInterval;
        // }
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

    public override void OnPlace()
    {
        BoxCollider serviceRangeCollider = serviceRange.GetComponent<BoxCollider>();
        Vector3 worldCenter = serviceRangeCollider.transform.TransformPoint(serviceRangeCollider.center);
        Vector3 worldHalfExtents = Vector3.Scale(serviceRangeCollider.size, serviceRangeCollider.transform.lossyScale) * 0.5f;
        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, serviceRangeCollider.transform.rotation);
        foreach(Collider col in cols){
            if (col.gameObject.TryGetComponent<House>(out var h)) {
                housesInRange.Add(h.gameObject);
                h.UpdateUtilities();
            }
        }
        currPowerAllocated = 0;
        
        // foreach (Collider col in cols) {
        //     if (col.gameObject.TryGetComponent<House>(out var h)) {
        //         if (currPowerAllocated + h.powerCost <= maxPower) {
        //             currPowerAllocated += h.powerCost;
        //             h.powerAllocated = h.powerCost;
        //         } else {
        //             h.powerAllocated = maxPower - currPowerAllocated;
        //             currPowerAllocated = maxPower;
        //             break;
        //         }
        //     }
        // }
    }

    public void OnRemove(){
        List<GameObject> objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Object"));
        foreach(GameObject g in objects){
            if (g.TryGetComponent<House>(out var h)) {
                h.UpdateUtilities();
            }
        }
    }

    public (string, int) GetProvided() {
        (string type, int val) t = ("power", maxPower - currPowerAllocated);
        return t;
    }

    public void UpdateAllocation(int consumption) {

    }
}
