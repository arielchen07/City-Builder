using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The PlacementSystem class is responsible for implementing all object placement features.
/// Note: this class can be implemented as a singleton, which would simplify some code in classes that utilize this class.
/// </summary>
public class PlacementSystem : MonoBehaviour
{
    //testInt is used in the unit tests.
    public int testInt = 10;
    [SerializeField] private GameObject pointer;
    [SerializeField] private InputManager inputManager;
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
        currentRotation = new Vector3(0, 0, 0);
    }
    void Update()
    {
        //update pointer position
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        pointer.transform.position = mousePos;
        //get object the cursor is currently colliding with
        if (inputManager.hitObject != null)
        {
            currentlyHovering = inputManager.hitObject;
        }
        //Gets user input if the cursor is not currently hovering over a UI elements, i.e the cursor hovering over the map. 
        //Placement of continuous objects such as roads is implemented differently from single objects.
        //Thus, the placement is also differentiated here.
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (beginPlacingContinuousObjects)
            {
                menuManager.ToggleRoadPlacementModeButtonVisual(isPlacingContinousObjects);
                if (isPlacingContinousObjects)
                {
                    CursorManager.cursorManager.SetCursorMode("placing");
                    PlaceContinuousObjects(road);
                }
                else
                {
                    CursorManager.cursorManager.SetCursorMode("deleting");
                    DeleteContinuousObjects();
                }
            }
            //Place an object if currentlyPlacing points to an object.
            if (currentlyPlacing != null && !beginPlacingContinuousObjects)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    PlaceObject();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DropObject();
                }
                //Otherwise, select or deselect objects depending on if the cursor is hovering over an object or not when left click is pressed.
            }
            else if (currentlyHovering != null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (currentlyHovering.CompareTag("Object"))
                    {
                        SelectObject();
                    }
                    else if (currentlySelecting != null)
                    {
                        DeselectObject();
                    }
                }
            }

        }
        //If an object is selected, ignore objects in the foreground layer and only collide with tiles in the ground layer.
        if (isSelectingObject)
        {
            inputManager.placementLayermask = LayerMask.GetMask("Ground");
        }
        else
        {
            inputManager.placementLayermask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Foreground");
        }
    }

    /// <summary>
    /// Called when the user clicks the road button. <br/>
    /// Begins placing or deleting roads.
    /// </summary>
    public void PlaceRoads()
    {
        beginPlacingContinuousObjects = true;
        menuManager.ToggleRightMenu();
    }
    /// <summary>
    /// Called when the user exits the road placement/deletion menu. <br/>
    /// Stops placing or deleting roads.
    /// </summary>
    public void StopPlacingRoads()
    {
        beginPlacingContinuousObjects = false;
        CursorManager.cursorManager.SetCursorMode(""); //default
    }
    /// <summary>
    /// Deletes the object that is currently selected.<br/>
    /// Frees all tiles the object was on.
    /// </summary>
    public void DeleteObject()
    {
        foreach (GameObject tile in currentlySelecting.GetComponent<PlaceableObject>().currentlyColliding)
        {
            tile.GetComponent<MapTile>().isOccupied = false;
            tile.GetComponent<MapTile>().placedObject = null;
        }
        if (currentlySelecting != null)
        {
            currentlySelecting.GetComponent<PlaceableObject>().OnDelete();
            Destroy(currentlySelecting);
            currentlySelecting = null;
        }
        DeselectObject();
    }
    /// <summary>
    /// Rotate the selected object.
    /// </summary>
    /// <param name="rotateLeft">True if rotating left, false if rotating right.</param>
    public void RotateObject(bool rotateLeft)
    {
        float yRot = -90f;
        if (rotateLeft)
        {
            yRot = 90f;
        }
        currentlySelecting.transform.Rotate(new Vector3(0, yRot, 0));
        currentRotation = currentlySelecting.transform.rotation.eulerAngles;
    }
    /// <summary>
    /// Attempts to place the object that is currently selected. <br/>
    /// Does nothing if the object cannot be placed. <br/>
    /// Updates the occupancy of tiles beneath the placed object. <br/>
    /// Resets the layermask of the inputManager to be able to detect both tiles and building objects. <br/>
    /// Deselects the placed object.
    /// </summary>
    public void PlaceObject()
    {
        if (currentlyPlacing.GetComponent<PlaceableObject>().CanBePlaced())
        {
            CursorManager.cursorManager.SetCursorMode("");
            DeselectObject();
            currentlyPlacing.transform.parent = null;
            foreach (GameObject tile in currentlyPlacing.GetComponent<PlaceableObject>().currentlyColliding)
            {
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

    /// <summary>
    /// This function is called when the user wants to place an object from their inventory. <br/>
    /// Gets the object prefab from itemObject. <br/>
    /// Spawns and attaches the object to the cursor. <br/>
    /// </summary>
    /// <param name="itemObject">The inventory object prefab that contains ItemUI</param>
    public void HoverObject(GameObject itemObject)
    {
        CursorManager.cursorManager.SetCursorMode("placing");
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
        isSelectingObject = true;
        if (currentlyPlacing != null)
        {
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

    /// <summary>
    /// Drop the object that the user is currently moving. <br/>
    /// Called when the user wanted to move an object, but cancels it by pressing escape, 
    /// or when the user is placing an object from their inventory but no longer wants the place it. <br/>
    /// Returns the object to its original position or puts it back into the inventory. 
    /// </summary>
    public void DropObject()
    {
        if (currentlyPlacing.GetComponent<PlaceableObject>().hasBeenPlaced == true)
        {
            currentlyPlacing.transform.SetPositionAndRotation(oldPosition, Quaternion.Euler(oldRotation));
            currentlyPlacing.GetComponent<PlaceableObject>().isHovering = false;
            currentlyPlacing.transform.parent = null;
        }
        else
        {
            PlaceableObject po = currentlyPlacing.GetComponent<PlaceableObject>();
            string itemID = InventoryInfo.GetItemID(po.objectName, po.category);
            inventoryManager.UpdateItemQuantityToServer(itemID, 1);
            Destroy(currentlyPlacing);
        }
        currentlyPlacing = null;
        isSelectingObject = false;
    }

    /// <summary>
    /// Attaches the selected object to the cursor, aligned based on if the object has even or odd dimensions.
    /// </summary>
    void AssignObjectToCursor()
    {
        pointer.GetComponent<PointerDetector>().currentlyPlacing = currentlyPlacing;
        pointer.GetComponent<PointerDetector>().AlignObject();
        currentlyPlacing.GetComponent<PlaceableObject>().isHovering = true;
    }

    /// <summary>
    /// Selects the object the cursor is currently hovering over. <br/>
    /// Opens the object menu and zooms to the selected object. <br/>
    /// Updates the object's information before showing the menu.
    /// </summary>
    public void SelectObject()
    {
        isSelectingObject = true;
        currentlySelecting = currentlyHovering;
        currentlySelecting.GetComponent<PlaceableObject>().OnSelect();
        ToggleObjectMenu();
        cameraController.ZoomToItem(currentlySelecting.transform.position);
        menuManager.CloseInventory();
        objectMenu.GetComponent<ObjectMenuManager>().UpdateInfo(currentlySelecting);
        objectMenu.GetComponent<ObjectMenuManager>().inventoryManager = inventoryManager;
    }

    /// <summary>
    /// Opens or closes the object menu showed when the user selects an object.
    /// </summary>
    public void ToggleObjectMenu()
    {
        if (objectMenu != null)
        {
            objectMenu.GetComponent<Animator>().SetBool("isOpen", isSelectingObject);
            objectMenu.GetComponent<Animator>().SetTrigger("toggle");
        }
        else
        {
            if (currentlySelecting != null)
            {
                if (currentlySelecting.TryGetComponent<House>(out var h))
                {
                    objectMenu = Instantiate(objectMenuHousingPrefab, gameCanvas.transform);
                }
                else
                {
                    objectMenu = Instantiate(objectMenuPrefab, gameCanvas.transform);
                }
                objectMenu.GetComponent<ObjectMenuManager>().ps = this;
                Animator objectMenuAnim = objectMenu.GetComponent<Animator>();
                objectMenuAnim.SetBool("isOpen", isSelectingObject);
                objectMenuAnim.SetTrigger("toggle");
            }
        }
    }
    /// <summary>
    /// Deselects the currently selected object, closing its menu and unlocks the camera.
    /// </summary>
    public void DeselectObject()
    {
        isSelectingObject = false;
        currentlySelecting = null;
        ToggleObjectMenu();
        cameraController.isLocked = false;
        menuManager.OpenInventory();
    }

    /// <summary>
    /// Picks up a placed object and attaches it to the cursor. Frees all tiles underneath the object and allows the cursor to only interact with tiles.
    /// </summary>
    public void MoveObject()
    {
        CursorManager.cursorManager.SetCursorMode("placing");
        currentlyPlacing = currentlySelecting;
        isSelectingObject = false;
        cameraController.isLocked = false;
        ToggleObjectMenu();
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
        oldPosition = currentlyPlacing.transform.position;
        oldRotation = currentlyPlacing.transform.rotation.eulerAngles;
        foreach (GameObject tile in currentlyPlacing.GetComponent<PlaceableObject>().currentlyColliding)
        {
            tile.GetComponent<MapTile>().isOccupied = false;
            tile.GetComponent<MapTile>().placedObject = null;
        }
        currentlyPlacing.transform.parent = pointer.GetComponent<PointerDetector>().indicator.transform;
        currentlyPlacing.transform.localPosition = new Vector3(0, 0, 0);
        AssignObjectToCursor();
    }

    /// <summary>
    /// Places continuous objects when the user holds down the left mouse button.
    /// </summary>
    /// <param name="objectToPlace">The object to place, i.e. the road prefab.</param>
    void PlaceContinuousObjects(GameObject objectToPlace)
    {
        GameObject pointerIndicator = pointer.GetComponent<PointerDetector>().indicator;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            bool canPlace = true;
            Collider[] cols = Physics.OverlapSphere(pointerIndicator.transform.position, 0.1f, LayerMask.GetMask("Ground"));
            GameObject closest = cols[0].gameObject;
            float minDistance = Mathf.Infinity;
            foreach (Collider col in cols)
            {
                if (col.TryGetComponent<MapTile>(out var m))
                {
                    float distance = Mathf.Abs((col.transform.position - pointerIndicator.transform.position).sqrMagnitude);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closest = col.gameObject;
                    }
                }
            }
            if (closest.GetComponent<MapTile>().isOccupied || closest.GetComponent<MapTile>().hasDecorations)
            {
                canPlace = false;
            }
            if (canPlace)
            {
                GameObject road = Instantiate(objectToPlace, pointerIndicator.transform.position, Quaternion.identity);
                closest.GetComponent<MapTile>().placedObject = road;
                closest.GetComponent<MapTile>().isOccupied = true;
            }
        }
    }
    /// <summary>
    /// Deletes continuous objects when the user holds down the left mouse button. <br/>
    /// Frees all tiles under the deleted objects.
    /// </summary>
    void DeleteContinuousObjects()
    {
        GameObject pointerIndicator = pointer.GetComponent<PointerDetector>().indicator;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Collider[] cols = Physics.OverlapSphere(pointerIndicator.transform.position, 0.1f, LayerMask.GetMask("Ground"));
            foreach (Collider col in cols)
            {
                if (col.TryGetComponent<MapTile>(out var m))
                {
                    if (m.placedObject != null && m.placedObject.TryGetComponent<Road>(out var r))
                    {
                        Destroy(r.gameObject);
                        m.isOccupied = false;
                        m.placedObject = null;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Changes road placement mode to placing roads.
    /// This function is called by the place road button in the road submenu.
    /// </summary>
    public void SetRoadsPlaceMode()
    {
        isPlacingContinousObjects = true;
    }
    /// <summary>
    /// Changes road placement mode to deleting roads.
    /// This function is called by the delete road button in the road submenu.
    /// </summary>
    public void SetRoadsDeleteMode()
    {
        isPlacingContinousObjects = false;
    }

    /// <summary>
    /// Gets the object that is currently being placed by the user.
    /// </summary>
    /// <returns>The gameobject the player is currently trying to place.</returns>
    public GameObject GetCurrentlyPlacing()
    {
        return this.currentlyPlacing;
    }
    /// <summary>
    /// Gets the object that is currently selected by the user.
    /// </summary>
    /// <returns>The gameobject the player currently selected.</returns>
    public GameObject GetCurrentlySelecting()
    {
        return this.currentlySelecting;
    }
}
