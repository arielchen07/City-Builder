using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestSystem : MonoBehaviour
{
    private bool isHovering = false;
    public GameObject HoverValid;
    public GameObject HoverInvalid;
    [SerializeField] private GameObject pointer;
    public InputManager inputManager;
    List<GameObject> currentlyColliding;
    public ResourceDataManager resourceDataManager;

    void Start()
    {
        currentlyColliding = new List<GameObject>();
    }

    void Update()
    {
        HoverValid.transform.position = pointer.GetComponent<PointerDetector>().indicator.transform.position + new Vector3(0, 0.5f, 0);
        HoverInvalid.transform.position = pointer.GetComponent<PointerDetector>().indicator.transform.position + new Vector3(0, 0.5f, 0);

        if (Input.GetKeyDown(KeyCode.X))
        {
            isHovering = !isHovering;

            if (!isHovering)
            {
                if (currentlyColliding.Count > 0)
                {
                    Harvest();
                }
                else
                {
                    HoverInvalid.SetActive(false);
                }
                
            }
        }

        if (isHovering)
        {
            Highlight();
        }
    }

    void Highlight()
    {
        GetCollidingDecorations();
        if (currentlyColliding.Count > 0)
        {
            HoverValid.SetActive(true);
            HoverInvalid.SetActive(false);
        }
        else
        {
            HoverInvalid.SetActive(true);
            HoverValid.SetActive(false);
        }
    }

    void Harvest()
    {
        HoverValid.SetActive(false);
        GameObject centerTile = pointer.GetComponent<PointerDetector>().currentlyColliding;
        centerTile.GetComponent<MapTile>().isOccupied = false;
        int woodCount = 0;
        int stoneCount = 0;
        foreach (GameObject decor in currentlyColliding)
        {
            if (decor.GetComponent<Decoration>().resourceType == "wood")
            {
                woodCount++;
            }
            else if (decor.GetComponent<Decoration>().resourceType == "stone")
            {
                stoneCount++;
            }
            Destroy(decor);
        }
        currentlyColliding.Clear();
        Debug.Log("wood = " + woodCount.ToString() + "stone = " + stoneCount.ToString());
        resourceDataManager.GainResource("wood", woodCount);
        resourceDataManager.GainResource("stone", stoneCount);
    }

    private void GetCollidingDecorations()
    {
        currentlyColliding.Clear();
        BoxCollider collider = HoverValid.GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = Vector3.Scale(collider.size, collider.transform.lossyScale) * 0.5f;

        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation);
        foreach (Collider col in cols)
        {
            if (col.CompareTag("Decoration"))
            {
                currentlyColliding.Add(col.gameObject);
            }
        }
    }
}
