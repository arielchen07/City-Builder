using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject[] placeableObjects;
    GameObject currentlyPlacing;
    GameObject currentlySelecting;
    Vector3 currentRotation;
    Vector3 oldPosition;
    Vector3 oldRotation;
    void Start()
    {
        currentRotation = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePos;
        if(inputManager.hitObject != null){
            currentlySelecting = inputManager.hitObject;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            HoverObject(placeableObjects[0]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            HoverObject(placeableObjects[1]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            HoverObject(placeableObjects[2]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            HoverObject(placeableObjects[3]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            HoverObject(placeableObjects[4]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            HoverObject(placeableObjects[5]);
        }

        if(currentlyPlacing != null){
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                PlaceObject();
            }
            else if(Input.GetKeyDown(KeyCode.Escape)){
                if(currentlyPlacing.GetComponent<PlaceableObject>().hasBeenPlaced == true) {
                    currentlyPlacing.transform.SetPositionAndRotation(oldPosition, Quaternion.Euler(oldRotation));
                    currentlyPlacing.GetComponent<PlaceableObject>().isHovering = false;
                    currentlyPlacing.transform.parent = null;
                } else {
                    Destroy(currentlyPlacing);
                }
                currentlyPlacing = null;
                inputManager.placementLayermask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Foreground");    
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)){
                currentlyPlacing.transform.Rotate(new Vector3(0,90,0));
                currentRotation = currentlyPlacing.transform.rotation.eulerAngles;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)){
                currentlyPlacing.transform.Rotate(new Vector3(0,-90,0));
                currentRotation = currentlyPlacing.transform.rotation.eulerAngles;
            }
        } else if (currentlySelecting != null) {
            if(currentlySelecting.CompareTag("Object")) {
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    SelectObject();
                }
            }
        }
    }

    void PlaceObject(){
        if (currentlyPlacing.GetComponent<PlaceableObject>().CanBePlaced()){
            currentlyPlacing.transform.parent = null;
            foreach(GameObject tile in currentlyPlacing.GetComponent<PlaceableObject>().currentlyColliding){
                tile.GetComponent<MapTile>().isOccupied = true;
                tile.GetComponent<MapTile>().placedObject = currentlyPlacing;
            }
            inputManager.placementLayermask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Foreground");
            currentlyPlacing.GetComponent<PlaceableObject>().isHovering = false;
            currentlyPlacing.GetComponent<PlaceableObject>().hasBeenPlaced = true;
            currentlyPlacing.GetComponent<PlaceableObject>().OnPlace();
            currentlyPlacing = null;
        }
    }

    void HoverObject(GameObject objectToPlace){
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
        if(currentlyPlacing != null){
            Destroy(currentlyPlacing);
            currentlyPlacing = null;
        }
        GameObject newBuilding = Instantiate(objectToPlace, mouseIndicator.GetComponent<PointerDetector>().indicator.transform);
        currentlyPlacing = newBuilding;
        currentlyPlacing.transform.Rotate(currentRotation);
        AssignObjectToCursor();
    }

    void AssignObjectToCursor(){
        mouseIndicator.GetComponent<PointerDetector>().currentlyPlacing = currentlyPlacing;
        mouseIndicator.GetComponent<PointerDetector>().AlignObject();
        currentlyPlacing.GetComponent<PlaceableObject>().isHovering = true;
    }

    void SelectObject() {
        currentlyPlacing = currentlySelecting;
        oldPosition = currentlyPlacing.transform.position;
        oldRotation = currentlyPlacing.transform.rotation.eulerAngles;
        foreach(GameObject tile in currentlyPlacing.GetComponent<PlaceableObject>().currentlyColliding){
            tile.GetComponent<MapTile>().isOccupied = false;
            tile.GetComponent<MapTile>().placedObject = null;
        }
        currentlyPlacing.transform.parent = mouseIndicator.GetComponent<PointerDetector>().indicator.transform;
        currentlyPlacing.transform.localPosition = new Vector3(0,0,0);
        AssignObjectToCursor();
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
    }
}
