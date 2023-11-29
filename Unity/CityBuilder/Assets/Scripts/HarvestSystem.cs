using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestSystem : MonoBehaviour
{
    private bool isHoverMode = false;
    private List<GameObject> hoveredTiles = new List<GameObject>();
    public GameObject HoverValid;
    public GameObject HoverInvalid;
    [SerializeField] private GameObject pointer;
    public int diameter = 3;
    public InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isHoverMode = !isHoverMode;

            if (isHoverMode)
            {
                HighlightTiles();
            }
            else
            {
                Harvest();
            }
        }
    }

    void HighlightTiles()
    {
        
    }

    void Harvest()
    {/*
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
