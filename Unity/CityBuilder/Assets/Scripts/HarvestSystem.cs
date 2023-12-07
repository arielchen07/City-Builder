using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The HarvestSystem class is attached to the HarvestSystem object. <br/>
/// This class is responsible for implementing the resource harvesting feature.
/// </summary>
public class HarvestSystem : MonoBehaviour
{
    private bool isHovering = false;
    public GameObject HoverValid;
    public GameObject HoverInvalid;
    [SerializeField] private GameObject pointer;
    public InputManager inputManager;
    List<GameObject> treesColliding;
    List<GameObject> rocksColliding;
    public ResourceDataManager resourceDataManager;
    public HarvesterManager harvesterManager;

    void Start()
    {
        treesColliding = new List<GameObject>();
        rocksColliding = new List<GameObject>();
        isHovering = false;
    }

    /// <summary>
    /// Responsible for listening to user input, and following pointer.
    /// </summary>
    void Update()
    {
        HoverValid.transform.position = pointer.GetComponent<PointerDetector>().indicator.transform.position + new Vector3(0, 0.5f, 0);
        HoverInvalid.transform.position = pointer.GetComponent<PointerDetector>().indicator.transform.position + new Vector3(0, 0.5f, 0);

        if (isHovering)
        {
            if (Highlight() == true && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Harvest();
                isHovering = false;
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                isHovering = false;
            }
        }
        else
        {
            HoverValid.SetActive(false);
            HoverInvalid.SetActive(false);
        }
    }

    /// <summary>
    /// Allows other scripts to begin harvesting hover.
    /// </summary>
    public void Hover()
    {
        isHovering = true;
    }

    /// <summary>
    /// Displays HoverValid object if the user can harvest something in range and they have sufficient harvest charges. <br/>
    /// Displays HoverInvalid otherwise.
    /// </summary>
    /// <returns>true if user can harvest, false otherwise</returns>
    bool Highlight()
    {
        GetCollidingDecorations();
        if (treesColliding.Count > 0 && harvesterManager.numTreeHarvester - harvesterManager.numOccupiedTreeHarvesters > 0
            || rocksColliding.Count > 0 && harvesterManager.numRockHarvester - harvesterManager.numOccupiedRockHarvesters > 0)
        {
            HoverValid.SetActive(true);
            HoverInvalid.SetActive(false);
            return true;
        }
        else
        {
            HoverInvalid.SetActive(true);
            HoverValid.SetActive(false);
            return false;
        }
    }

    /// <summary>
    /// Harvests available resources, deletings their objects, and updating resource counts with ResourceDataManager.
    /// </summary>
    void Harvest()
    {
        HoverValid.SetActive(false);
        GameObject centerTile = pointer.GetComponent<PointerDetector>().currentlyColliding;
        centerTile.GetComponent<MapTile>().isOccupied = false;
        int woodCount = 0;
        int stoneCount = 0;
        if (treesColliding.Count > 0 && harvesterManager.AddActiveTreeHarvester())
        {
            foreach (GameObject tree in treesColliding)
            {
                woodCount++;
                Destroy(tree);
                PollutionManager.pollutionManager.numTrees--;
            }
            treesColliding.Clear();
        }
        if (rocksColliding.Count > 0 && harvesterManager.AddActiveRockHarvester())
        {
            foreach (GameObject rock in rocksColliding)
            {
                stoneCount++;
                Destroy(rock);
            }
            rocksColliding.Clear();
        }
        resourceDataManager.GainResource("wood", woodCount);
        resourceDataManager.GainResource("stone", stoneCount);
    }

    /// <summary>
    /// Updates treesColliding and rocksColliding appropriately.
    /// </summary>
    private void GetCollidingDecorations()
    {
        treesColliding.Clear();
        rocksColliding.Clear();
        BoxCollider collider = HoverValid.GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = Vector3.Scale(collider.size, collider.transform.lossyScale) * 0.5f;

        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation);
        foreach (Collider col in cols)
        {
            if (col.CompareTag("Decoration"))
            {
                if (col.gameObject.GetComponent<Decoration>().resourceType == "wood")
                {
                    treesColliding.Add(col.gameObject);
                }
                else if (col.gameObject.GetComponent<Decoration>().resourceType == "stone")
                {
                    rocksColliding.Add(col.gameObject);
                }
            }
        }
    }
}
