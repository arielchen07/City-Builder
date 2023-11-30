using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestSystem : MonoBehaviour
{
    private bool isHovering = false;
    private List<GameObject> hoveredTiles = new List<GameObject>();
    public GameObject HoverValid;
    public GameObject HoverInvalid;
    [SerializeField] private GameObject pointer;
    public int diameter = 3;
    public InputManager inputManager;

    void Start()
    {

    }

    void Update()
    {
        HoverValid.transform.position = pointer.GetComponent<PointerDetector>().indicator.transform.position + new Vector3(0, 0.5f, 0);

        HoverInvalid.transform.position = pointer.GetComponent<PointerDetector>().indicator.transform.position + new Vector3(0, 0.5f, 0);
        if (Input.GetKeyDown(KeyCode.X))
        {
            isHovering = !isHovering;

            if (isHovering)
            {
                Highlight();
            }
            else
            {
                Harvest();
            }
        }
    }

    void Highlight()
    {
        HoverValid.SetActive(true);
        
    }

    void Harvest()
    {
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
        GameObject centerTile = pointer.GetComponent<PointerDetector>().currentlyColliding;
        centerTile.GetComponent<MapTile>().isOccupied = false;
        


        /*
        // Get the center tile from the mouse position or hovered tile
        Vector3Int centerTilePos = pointer.GetComponent<PointerDetector>().indicator.transform.position;

        // Calculate the bounds of the 3x3 area based on the center tile
        Vector3Int minTileBounds = centerTilePos - new Vector3Int(1, 1, 0);
        Vector3Int maxTileBounds = centerTilePos + new Vector3Int(1, 1, 0);

        for (int x = minTileBounds.x; x <= maxTileBounds.x; x++)
        {
            for (int y = minTileBounds.y; y <= maxTileBounds.y; y++)
            {
                Vector3Int currentTilePos = new Vector3Int(x, y, 0);
                // Check if the tile is within your tilemap and get the MapTile script
                MapTile mapTile = GetMapTileAtPosition(currentTilePos);
                if (mapTile != null && mapTile.isOccupied)
                {
                    // Remove the decoration
                    Destroy(mapTile.placedObject);
                    // Reset the tile's isOccupied property
                    mapTile.isOccupied = false;
                    mapTile.placedObject = null;
                }
            }
        }

        // Clear the highlighted tiles after removing decorations
        ClearHighlightedTiles();*/
    }
}
