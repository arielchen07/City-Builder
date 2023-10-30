using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementSystem : MonoBehaviour
{
    public int testInt = 10;
    [SerializeField] private GameObject pointer;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject[] placeableObjects;
    public GameObject road;
    public GameObject currentlyPlacing;
    public GameObject currentlySelecting;
    Vector3 currentRotation;
    Vector3 oldPosition;
    Vector3 oldRotation;
    bool beginPlacingContinuousObjects = false;
    bool isSelectingObject = false;
    void Start()
    {
        currentRotation = new Vector3(0,0,0);
    }
    void Update()
    {
        //update pointer position
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        pointer.transform.position = mousePos;
        //get object the cursor is currently colliding with
        if(inputManager.hitObject != null){
            currentlySelecting = inputManager.hitObject;
        }
        //checks for key inputs to spawn objects
        SpawnObjectOnKey();


        if(!EventSystem.current.IsPointerOverGameObject()){
            if (beginPlacingContinuousObjects) {
                PlaceContinuousObjects(road);
            }

            if(currentlyPlacing != null && !beginPlacingContinuousObjects){
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    PlaceObject();
                }
                else if(Input.GetKeyDown(KeyCode.Escape)){
                    DropObject();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow)){
                    currentlyPlacing.transform.Rotate(new Vector3(0,90,0));
                    currentRotation = currentlyPlacing.transform.rotation.eulerAngles;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow)){
                    currentlyPlacing.transform.Rotate(new Vector3(0,-90,0));
                    currentRotation = currentlyPlacing.transform.rotation.eulerAngles;
                }
                else if (Input.GetKeyDown(KeyCode.Delete)) {
                    Destroy(currentlyPlacing);
                    currentlyPlacing = null;
                }
            } else if (currentlySelecting != null) {
                if (currentlySelecting.CompareTag("Object")) {
                    if (Input.GetKeyDown(KeyCode.Mouse0)) {
                        SelectObject();
                    }
                }
            }
        }

        if(isSelectingObject) {
            inputManager.placementLayermask = LayerMask.GetMask("Ground");
        } else {
            inputManager.placementLayermask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Foreground");
        }
    }
    /*
    For manual testing
    */
    void SpawnObjectOnKey() {
        if(!isSelectingObject){
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
            if(Input.GetKeyDown(KeyCode.R)){ //place roads
                beginPlacingContinuousObjects = true;
                if(currentlyPlacing != null) {
                    DropObject();
                }
            }
        }
    }
    /*
    PlaceObject is called when the user is currently selecting an object and wants to place it.
    */
    public void PlaceObject(){
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
        isSelectingObject = false;
    }

    /*
    HoverObject is called when the user creates a new building and needs to place it.
    */
    public void HoverObject(GameObject objectToPlace){
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
        isSelectingObject = true;
        if(currentlyPlacing != null){
            Destroy(currentlyPlacing);
            currentlyPlacing = null;
        }
        GameObject newBuilding = Instantiate(objectToPlace, pointer.GetComponent<PointerDetector>().indicator.transform);
        currentlyPlacing = newBuilding;
        currentlyPlacing.transform.Rotate(currentRotation);
        AssignObjectToCursor();
    }

    /*
    DropObject is called when the user selects an object and presses escape. Ie the player wants to move an object but decides
    to not move it.
    */
    public void DropObject() {
        if(currentlyPlacing.GetComponent<PlaceableObject>().hasBeenPlaced == true) {
            currentlyPlacing.transform.SetPositionAndRotation(oldPosition, Quaternion.Euler(oldRotation));
            currentlyPlacing.GetComponent<PlaceableObject>().isHovering = false;
            currentlyPlacing.transform.parent = null;
        } else {
            Destroy(currentlyPlacing);
        }
        currentlyPlacing = null;
        isSelectingObject = false;
    }

    void AssignObjectToCursor(){
        pointer.GetComponent<PointerDetector>().currentlyPlacing = currentlyPlacing;
        pointer.GetComponent<PointerDetector>().AlignObject();
        currentlyPlacing.GetComponent<PlaceableObject>().isHovering = true;
    }
    /*
    SelectObject is called when a user hovers their cursor over an object and wants to select it.
    */
    public void SelectObject() {
        Debug.Log("selecting object");
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
        isSelectingObject = true;
        currentlyPlacing = currentlySelecting;
        oldPosition = currentlyPlacing.transform.position;
        oldRotation = currentlyPlacing.transform.rotation.eulerAngles;
        foreach(GameObject tile in currentlyPlacing.GetComponent<PlaceableObject>().currentlyColliding){
            tile.GetComponent<MapTile>().isOccupied = false;
            tile.GetComponent<MapTile>().placedObject = null;
        }
        currentlyPlacing.transform.parent = pointer.GetComponent<PointerDetector>().indicator.transform;
        currentlyPlacing.transform.localPosition = new Vector3(0,0,0);
        AssignObjectToCursor();
    }

    void PlaceContinuousObjects(GameObject objectToPlace){
        GameObject pointerIndicator = pointer.GetComponent<PointerDetector>().indicator;
        if (Input.GetKey(KeyCode.Mouse0)){
            Collider[] cols = Physics.OverlapSphere(pointerIndicator.transform.position, 0.1f, LayerMask.GetMask("Foreground"));
            if(cols.Length == 0){
                Instantiate(objectToPlace, pointerIndicator.transform.position, Quaternion.identity);
                return;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            beginPlacingContinuousObjects = false;
        }
    }

    public GameObject GetCurrentlyPlacing()
    {
        return this.currentlyPlacing;
    }

    public GameObject GetCurrentlySelecting()
    {
        return this.currentlySelecting;
    }
}
