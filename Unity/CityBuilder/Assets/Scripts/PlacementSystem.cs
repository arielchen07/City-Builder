using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlacementSystem : MonoBehaviour
{
    public int testInt = 10;
    [SerializeField] private GameObject pointer;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject[] placeableObjects;
    public GameObject road;
    public GameObject currentlyPlacing;
    public GameObject currentlySelecting;
    public GameObject currentlyHovering;
    public GameObject gameCanvas;
    public GameObject objectMenu;
    public GameObject objectMenuPrefab;
    public GameObject objectMenuHousingPrefab;
    public CameraController cameraController;
    public MenuManager menuManager;
    public InventoryManager inventoryManager;
    Vector3 currentRotation;
    Vector3 oldPosition;
    Vector3 oldRotation;
    bool beginPlacingContinuousObjects = false;
    public bool isSelectingObject = false;
    public bool isPlacingContinousObjects = true;
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
            currentlyHovering = inputManager.hitObject;
        }
        if(!EventSystem.current.IsPointerOverGameObject()){
            if (beginPlacingContinuousObjects) {
                menuManager.ToggleRoadPlacementModeButtonVisual(isPlacingContinousObjects);
                if(isPlacingContinousObjects){
                    CursorManager.cursorManager.SetCursorMode("placing");
                    PlaceContinuousObjects(road);
                } else {
                    CursorManager.cursorManager.SetCursorMode("deleting");
                    DeleteContinuousObjects();
                }
            }
            if(currentlyPlacing != null && !beginPlacingContinuousObjects){
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    PlaceObject();
                }
                else if(Input.GetKeyDown(KeyCode.Escape)){
                    DropObject();
                }
            } else if (currentlyHovering != null) {
                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    if (currentlyHovering.CompareTag("Object")) {
                        SelectObject();
                    } else if (currentlySelecting != null && currentlyPlacing != null){
                        DeselectObject();
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

    public void PlaceRoads(){
        beginPlacingContinuousObjects = true;
        menuManager.ToggleRightMenu();
    }

    public void StopPlacingRoads(){
        beginPlacingContinuousObjects = false;
        CursorManager.cursorManager.SetCursorMode(""); //default
    }
    public void DeleteObject(){
        foreach(GameObject tile in currentlySelecting.GetComponent<PlaceableObject>().currentlyColliding){
            tile.GetComponent<MapTile>().isOccupied = false;
            tile.GetComponent<MapTile>().placedObject = null;
        }
        if(currentlySelecting != null){
            currentlySelecting.GetComponent<PlaceableObject>().OnDelete();
            Destroy(currentlySelecting);
            currentlySelecting = null;
        }
        DeselectObject();
    }
    public void RotateObject(bool rotateLeft){
        float yRot = -90f;
        if(rotateLeft){
            yRot = 90f;
        }
        currentlySelecting.transform.Rotate(new Vector3(0,yRot,0));
        currentRotation = currentlySelecting.transform.rotation.eulerAngles;
    }
    /*
    PlaceObject is called when the user is currently selecting an object and wants to place it.
    */
    public void PlaceObject(){
        CursorManager.cursorManager.SetCursorMode("");
        if (currentlyPlacing.GetComponent<PlaceableObject>().CanBePlaced()){
            DeselectObject();
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
    public void HoverObject(GameObject itemObject){
        CursorManager.cursorManager.SetCursorMode("placing");
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
        isSelectingObject = true;
        if(currentlyPlacing != null){
            Destroy(currentlyPlacing);
            currentlyPlacing = null;
        }
        ItemUI item = itemObject.GetComponent<ItemUI>();
        GameObject newBuilding = Instantiate(item.objectPrefab, pointer.GetComponent<PointerDetector>().indicator.transform);
        newBuilding.GetComponent<PlaceableObject>().item = item;
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
            PlaceableObject po = currentlyPlacing.GetComponent<PlaceableObject>();
            string itemID = InventoryInfo.GetItemID(po.objectName, po.category);
            inventoryManager.UpdateItemQuantityToServer(itemID, 1);
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
        isSelectingObject = true;
        currentlySelecting = currentlyHovering;
        ToggleObjectMenu();
        cameraController.ZoomToItem(currentlySelecting.transform.position);
        menuManager.CloseInventory();
        objectMenu.GetComponent<ObjectMenuManager>().UpdateInfo(currentlySelecting);
        objectMenu.GetComponent<ObjectMenuManager>().inventoryManager = inventoryManager;
    }
    public void ToggleObjectMenu(){
        if(objectMenu != null){
            objectMenu.GetComponent<Animator>().SetBool("isOpen", isSelectingObject);
            objectMenu.GetComponent<Animator>().SetTrigger("toggle");
        } else {
            if (currentlySelecting != null){
                if(currentlySelecting.TryGetComponent<House>(out var h)){
                    objectMenu = Instantiate(objectMenuHousingPrefab, gameCanvas.transform);
                } else {
                    objectMenu = Instantiate(objectMenuPrefab, gameCanvas.transform);
                }
                objectMenu.GetComponent<ObjectMenuManager>().ps = this;
                Animator objectMenuAnim = objectMenu.GetComponent<Animator>();
                objectMenuAnim.SetBool("isOpen", isSelectingObject);
                objectMenuAnim.SetTrigger("toggle");
            }
        }
    }

    public void DeselectObject(){
        isSelectingObject = false;
        currentlySelecting = null;
        ToggleObjectMenu();
        cameraController.isLocked = false;
        menuManager.OpenInventory();
    }

    public void MoveObject(){
        CursorManager.cursorManager.SetCursorMode("placing");
        currentlyPlacing = currentlySelecting;
        isSelectingObject = false;
        ToggleObjectMenu();
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
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
            bool canPlace = true;
            Collider[] cols = Physics.OverlapSphere(pointerIndicator.transform.position, 0.1f, LayerMask.GetMask("Ground"));
            GameObject closest = cols[0].gameObject;
            float minDistance = Mathf.Infinity;
            foreach(Collider col in cols){
                if(col.TryGetComponent<MapTile>(out var m)){
                    float distance = Mathf.Abs((col.transform.position-pointerIndicator.transform.position).sqrMagnitude);
                    if(distance < minDistance){
                        minDistance = distance;
                        closest = col.gameObject;
                    }
                }
            }
            if (closest.GetComponent<MapTile>().isOccupied){
                canPlace = false;
            }
            if(canPlace){
                GameObject road = Instantiate(objectToPlace, pointerIndicator.transform.position, Quaternion.identity);
                closest.GetComponent<MapTile>().placedObject = road;
                closest.GetComponent<MapTile>().isOccupied = true;
            }
        }
    }

    void DeleteContinuousObjects(){
        GameObject pointerIndicator = pointer.GetComponent<PointerDetector>().indicator;
        if (Input.GetKey(KeyCode.Mouse0)){
            Collider[] cols = Physics.OverlapSphere(pointerIndicator.transform.position, 0.1f, LayerMask.GetMask("Ground"));
            foreach (Collider col in cols){
                if(col.TryGetComponent<MapTile>(out var m)){
                    if(m.placedObject != null && m.placedObject.TryGetComponent<Road>(out var r)){
                        Destroy(r.gameObject);
                        m.isOccupied = false;
                        m.placedObject = null;
                    }
                }
            }
        }
    }

    public void SetRoadsPlaceMode(){
        isPlacingContinousObjects = true;
    }

    public void SetRoadsDeleteMode(){
        isPlacingContinousObjects = false;
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
