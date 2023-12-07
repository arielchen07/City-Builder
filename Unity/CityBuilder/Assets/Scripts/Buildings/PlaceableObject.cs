using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The PlaceableObject class is the parent class of all objects that can be placed <br/>
/// Note that in Unity, Start and Update functions cannot be inherited by child classes of this class as they are MonoBehavior functions
/// </summary>
public class PlaceableObject : MonoBehaviour
{
    public List<GameObject> currentlyColliding;
    public List<GameObject> adjacentTiles;
    public GameObject HoverValid;
    public GameObject HoverInvalid;
    public Vector3 worldCenter;
    public Vector3 worldHalfExtents;
    public Vector2 dimensions;
    public bool canBePlaced;
    public bool isHovering;
    public bool evenDimensions;
    public bool hasBeenPlaced = false;
    public string objectName = "Placeable Object";
    public string displayName;
    public string category;
    public ItemUI item;
    public bool isActive = true;

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

    /// <summary>
    /// Checks if the object can be placed at its current position. <br/>
    /// The tiles must be not occupied by another object and all tiles under the building must exist (this prevents objects from being placed ontop of water). <br/>
    /// The building must also be adjacent to a road. <br/>
    /// </summary>
    /// <returns>True if the building can be placed and False otherwise.</returns>
    public bool CanBePlaced()
    {
        if (currentlyColliding.Count < dimensions.x * dimensions.y)
        {
            return false;
        }
        foreach (GameObject collidingObject in currentlyColliding)
        {
            MapTile tile = collidingObject.GetComponent<MapTile>();
            if (tile != null)
            {
                if (tile.isOccupied || tile.hasDecorations)
                {
                    return false;
                }
            }
        }
        bool hasRoad = false;
        foreach (GameObject adjacentTile in adjacentTiles)
        {
            MapTile tile = adjacentTile.GetComponent<MapTile>();
            if (tile.placedObject != null && tile.placedObject.TryGetComponent<Road>(out var r))
            {
                hasRoad = true;
            }
        }
        if (!hasRoad)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Uses the object's collider to find all tiles that are currently colliding with it. <br/>
    /// Note that this is not implemented via OnTriggerEnter as it may be unreliable when we move buildings by directly modifying their position.
    /// </summary>
    /// <returns>A list of Tile gameobjects that the building is currently colliding with</returns>
    public List<GameObject> GetCollidingTiles()
    {
        currentlyColliding.Clear();
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = Vector3.Scale(collider.size, collider.transform.lossyScale) * 0.5f;

        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation);
        foreach (Collider col in cols)
        {
            if (col.CompareTag("Tile"))
            {
                currentlyColliding.Add(col.gameObject);
            }
        }
        return currentlyColliding;
    }

    /// <summary>
    /// Uses the object's collider to find all tiles that are adjacent to it. <br/>
    /// Note that this is not implemented via OnTriggerEnter as it may be unreliable when we move buildings by directly modifying their position.
    /// </summary>
    /// <returns>A list of Tile gameobjects that the building is adjacent to</returns>
    public List<GameObject> GetAdjacentTiles()
    {
        adjacentTiles.Clear();
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = Vector3.Scale(collider.size, collider.transform.lossyScale) * 0.5f;

        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents + new Vector3(1, 0, 1), collider.transform.rotation);
        foreach (Collider col in cols)
        {
            if (col.CompareTag("Tile"))
            {
                adjacentTiles.Add(col.gameObject);
            }
        }
        return adjacentTiles;
    }
    /// <summary>
    /// This function is called by PlacementSystem whenever the user places an object. <br/>
    /// Updates the utilities on the map. <br/>
    /// If the building is a polluter or house, add it to the respective manager so the manager has a reference to this object.
    /// </summary>
    public virtual void OnPlace()
    {
        UtilitiesManager.utilManager.UpdateUtilities();
        if (gameObject.TryGetComponent<Polluter>(out var p))
        {
            if (!PollutionManager.pollutionManager.polluters.Contains(p))
            {
                PollutionManager.pollutionManager.polluters.Add(p);
            }
        }
        if (gameObject.TryGetComponent<House>(out var h))
        {
            if (!PopulationManager.populationManager.houses.Contains(h))
            {
                PopulationManager.populationManager.houses.Add(h);
            }
        }
    }
    /// <summary>
    /// This function is called by PlacementSystem whenever the user deletes an object. <br/>
    /// Updates the utilities on the map. <br/>
    /// If the building is a polluter or house, remove it from the respective manager.
    /// </summary>
    public virtual void OnDelete()
    {
        isActive = false;
        UtilitiesManager.utilManager.UpdateUtilities();
        if (gameObject.TryGetComponent<Polluter>(out var p))
        {
            PollutionManager.pollutionManager.polluters.Remove(p);
        }
        if (gameObject.TryGetComponent<House>(out var h))
        {
            PopulationManager.populationManager.houses.Remove(h);
        }
    }

    /// <summary>
    /// This function is called whenever an object is selected by the user <br/>
    /// Updates all house utility values whenever an object is selected.
    /// </summary>
    public virtual void OnSelect()
    {
        UtilitiesManager.utilManager.UpdateUtilities();
    }
}
