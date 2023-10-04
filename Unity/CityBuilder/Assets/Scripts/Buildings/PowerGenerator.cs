using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : PlaceableObject
{
    public GameObject powerRange;
    public int maxPower;
    public int currPowerAllocated;
    public float updateInterval = 2f;
    public float timer = 0;

    void Start()
    {
        currentlyColliding = new List<GameObject>();
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
    }
    void Update() {
        if (Time.time > timer) {
            OnPlace();
            timer = Time.time + updateInterval;
        }
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
        BoxCollider collider = powerRange.GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = collider.transform.TransformVector(collider.size * 0.5f);
        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation);
        currPowerAllocated = 0;
        foreach (Collider col in cols) {
            House h = col.gameObject.GetComponent<House>();
            if (h != null) {
                if (currPowerAllocated + h.powerCost <= maxPower) {
                    currPowerAllocated += h.powerCost;
                    h.powerAllocated = h.powerCost;
                } else {
                    h.powerAllocated = maxPower - currPowerAllocated;
                    currPowerAllocated = maxPower;
                    break;
                }
            }
        }
    }
}
