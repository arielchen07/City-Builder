using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDistributor : PlaceableObject
{
    public GameObject waterRange;
    public int maxWater;
    public int currWaterAllocated;
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
        BoxCollider collider = waterRange.GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = Vector3.Scale(collider.size, collider.transform.lossyScale) * 0.5f;
        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation);
        currWaterAllocated = 0;
        foreach (Collider col in cols) {
            House h = col.gameObject.GetComponent<House>();
            if (h != null) {
                if (currWaterAllocated + h.waterCost <= maxWater) {
                    currWaterAllocated += h.waterCost;
                    h.waterAllocated = h.waterCost;
                } else {
                    h.waterAllocated = maxWater - currWaterAllocated;
                    currWaterAllocated = maxWater;
                    break;
                }
            }
        }
    }
}
