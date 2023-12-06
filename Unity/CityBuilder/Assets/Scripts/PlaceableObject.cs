using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool CanBePlaced(){
        if(currentlyColliding.Count < dimensions.x * dimensions.y){
            return false;
        }
        foreach(GameObject collidingObject in currentlyColliding){
            MapTile tile = collidingObject.GetComponent<MapTile>();
            if (tile != null) {
                if (tile.isOccupied || tile.hasDecorations){
                    return false;
                }
            }
        }
        bool hasRoad = false;
        foreach(GameObject adjacentTile in adjacentTiles){
            MapTile tile = adjacentTile.GetComponent<MapTile>();
            if (tile.placedObject != null && tile.placedObject.TryGetComponent<Road>(out var r)){
                hasRoad = true;
            }
        }
        if(!hasRoad){
            return false;
        }
        return true;
    }

    public List<GameObject> GetCollidingTiles(){
        currentlyColliding.Clear();
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = Vector3.Scale(collider.size, collider.transform.lossyScale) * 0.5f;
        
        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation);
        foreach(Collider col in cols){
            if(col.CompareTag("Tile"))
            {
                currentlyColliding.Add(col.gameObject);
            }   
        }
        return currentlyColliding;
    }

    public List<GameObject> GetAdjacentTiles(){
        adjacentTiles.Clear();
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = Vector3.Scale(collider.size, collider.transform.lossyScale) * 0.5f;
        
        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents + new Vector3(1,0,1), collider.transform.rotation);
        foreach(Collider col in cols){
            if(col.CompareTag("Tile"))
            {
                adjacentTiles.Add(col.gameObject);
            }   
        }
        return adjacentTiles;
    }
    public virtual void OnPlace() {
        UtilitiesManager.utilManager.UpdateUtilities();
        if(gameObject.TryGetComponent<Polluter>(out var p)){
            if(!PollutionManager.pollutionManager.polluters.Contains(p)){
                PollutionManager.pollutionManager.polluters.Add(p);
            }
        }
        if(gameObject.TryGetComponent<House>(out var h)){
            if(!PollutionManager.pollutionManager.houses.Contains(h)){
                PollutionManager.pollutionManager.houses.Add(h);
            }
        }
    }

    public virtual void OnDelete(){
        isActive = false;
        UtilitiesManager.utilManager.UpdateUtilities();
        if(gameObject.TryGetComponent<Polluter>(out var p)){
            PollutionManager.pollutionManager.polluters.Remove(p);
        }
        if(gameObject.TryGetComponent<House>(out var h)){
            PollutionManager.pollutionManager.houses.Remove(h);
        }
    }

    public virtual void OnSelect(){
        UtilitiesManager.utilManager.UpdateUtilities();
    }
}
