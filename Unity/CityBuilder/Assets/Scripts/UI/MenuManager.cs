using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is attached the MenuManager gameobject. <br/>
/// This class is responsible for managing which menu should be open depending on the user's input. <br/>
/// This class contains functions called by the animators of the menus.
/// </summary>
public class MenuManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject rightMenu;
    public GameObject utilSubMenu;
    public GameObject roadMenu;
    public Image placeRoads;
    public Image deleteRoads;
    bool inventoryIsOpen = false;
    bool isSubMenuOpen = false;
    bool isRightMenuOpen = true;
    bool inventoryWasOpen = false;
    bool isRoadMenuOpen = false;
    public List<GameObject> roads;
    public List<GameObject> housing;
    public List<GameObject> power;
    public List<GameObject> sewage;
    public List<GameObject> water;
    public List<GameObject> internet;
    public List<GameObject> gas;
    public List<GameObject> harvester;
    public List<GameObject> government;
    public GameObject content;
    public PlacementSystem ps;
    public InventoryManager inventoryManager;
    public GameObject cover;
    void Start()
    {
        cover.SetActive(true);
    }

    /// <summary>
    /// Loads the corresponding inventory given which button the user pressed.
    /// </summary>
    /// <param name="category">The category of objects the button that the user clicked opens to</param>
    public void LoadInventory(string category)
    {
        List<GameObject> buttonList;
        Debug.Log(category);
        switch (category)
        {
            case "roads":
                buttonList = roads;
                break;
            case "housing":
                buttonList = housing;
                break;
            case "power":
                buttonList = power;
                break;
            case "sewage":
                buttonList = sewage;
                break;
            case "water":
                buttonList = water;
                break;
            case "internet":
                buttonList = internet;
                break;
            case "gas":
                buttonList = gas;
                break;
            case "harvester":
                buttonList = harvester;
                break;
            case "government":
                buttonList = government;
                break;
            default:
                buttonList = housing;
                break;
        }
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (GameObject button in buttonList)
        {
            GameObject newButton = Instantiate(button, content.transform);
            newButton.GetComponent<ItemUI>().inventoryManager = inventoryManager;
            newButton.GetComponent<Button>().onClick.AddListener(() => ps.HoverObject(newButton));
            newButton.GetComponent<Button>().onClick.AddListener(() => newButton.GetComponent<ItemUI>().TakeItem());
        }
    }
    /// <summary>
    /// Closes the inventory and tracks if the inventory was previously opened. <br/>
    /// This allows the inventory to be opened again if this function was called by another menu function, as opposed to if the user closed the inventory themselves. <br/>
    /// </summary>
    public void CloseInventory()
    {
        if (inventoryIsOpen)
        {
            inventoryWasOpen = true;
        }
        else
        {
            inventoryWasOpen = false;
        }
        inventory.GetComponent<Animator>().SetTrigger("toggle");
        inventory.GetComponent<Animator>().SetBool("isOpen", false);
    }
    /// <summary>
    /// Opens the inventory after the inventory is loaded with the correct items.
    /// </summary>
    public void OpenInventory()
    {
        if (inventoryWasOpen)
        {
            inventory.GetComponent<Animator>().SetTrigger("toggle");
            inventory.GetComponent<Animator>().SetBool("isOpen", true);
        }
    }
    /// <summary>
    /// Toggles the inventory to open or close. <br/>
    /// </summary>
    /// <param name="category">The category of items to load</param>
    public void ToggleInventory(string category)
    {
        inventoryIsOpen = !inventoryIsOpen;
        isRightMenuOpen = !isRightMenuOpen;
        if (inventoryIsOpen)
        {
            LoadInventory(category);
        }
        inventory.GetComponent<Animator>().SetTrigger("toggle");
        inventory.GetComponent<Animator>().SetBool("isOpen", inventoryIsOpen);
        rightMenu.GetComponent<Animator>().SetTrigger("toggle");
        rightMenu.GetComponent<Animator>().SetBool("isOpen", isRightMenuOpen);
    }

    /// <summary>
    /// Toggles a submenu, i.e the utilities submenu on the right after the user clicks on the utilities button.
    /// </summary>
    /// <param name="subMenu">The specific submenu to open</param>
    public void ToggleSubMenu(GameObject subMenu)
    {
        isSubMenuOpen = !isSubMenuOpen;
        isRightMenuOpen = !isRightMenuOpen;
        subMenu.GetComponent<Animator>().SetTrigger("toggle");
        subMenu.GetComponent<Animator>().SetBool("isOpen", isSubMenuOpen);
        rightMenu.GetComponent<Animator>().SetTrigger("toggle");
        rightMenu.GetComponent<Animator>().SetBool("isOpen", isRightMenuOpen);
    }

    /// <summary>
    /// Toggles the inventory from clicking a button in a submenu. <br/>
    /// This functions differently from toggling the inventory from the main menu as it needs to open or close the submenu.
    /// </summary>
    /// <param name="callingButton">the button that is calling this function</param>
    public void ToggleInventoryFromSubMenu(GameObject callingButton)
    {
        GameObject subMenu = callingButton.transform.parent.gameObject;
        inventoryIsOpen = !inventoryIsOpen;
        isSubMenuOpen = !isSubMenuOpen;
        if (inventoryIsOpen)
        {
            LoadInventory(callingButton.name);
        }
        inventory.GetComponent<Animator>().SetTrigger("toggle");
        inventory.GetComponent<Animator>().SetBool("isOpen", inventoryIsOpen);
        subMenu.GetComponent<Animator>().SetTrigger("toggle");
        subMenu.GetComponent<Animator>().SetBool("isOpen", isSubMenuOpen);
    }

    /// <summary>
    /// Toggles the main menu on the right.
    /// </summary>
    public void ToggleRightMenu()
    {
        isRightMenuOpen = !isRightMenuOpen;
        rightMenu.GetComponent<Animator>().SetTrigger("toggle");
        rightMenu.GetComponent<Animator>().SetBool("isOpen", isRightMenuOpen);
    }

    /// <summary>
    /// Toggles the road menu on the right, which is opened via the road button.
    /// </summary>
    public void ToggleRoadMenu()
    {
        isRoadMenuOpen = !isRoadMenuOpen;
        roadMenu.GetComponent<Animator>().SetTrigger("toggle");
        roadMenu.GetComponent<Animator>().SetBool("isOpen", isRoadMenuOpen);
    }
    /// <summary>
    /// Changes the color of the road placing buttons.
    /// </summary>
    /// <param name="isPlacing">Whether the user is placing or deleting roads.</param>
    public void ToggleRoadPlacementModeButtonVisual(bool isPlacing)
    {
        if (isPlacing)
        {
            placeRoads.color = new Color(1, 1, 1);
            deleteRoads.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            placeRoads.color = new Color(0.5f, 0.5f, 0.5f);
            deleteRoads.color = new Color(1, 1, 1);
        }
    }
    /// <summary>
    /// Checks if the inventory is currently open.
    /// </summary>
    /// <returns>True if the inventory is open and False otherwise.</returns>
    public bool GetInventoryIsOpen()
    {
        return inventoryIsOpen;
    }
    /// <summary>
    /// Checks if a sub menu is open.
    /// </summary>
    /// <returns>True if a sub menu is open and False otherwise.</returns>
    public bool GetIsSubMenuOpen()
    {
        return isSubMenuOpen;
    }

    /// <summary>
    /// Checks if the main menu on the right is open.
    /// </summary>
    /// <returns>True if the main menu is open and False otherwise.</returns>
    public bool GetIsRightMenuOpen()
    {
        return isRightMenuOpen;
    }

    /// <summary>
    /// Deletes the cover object. The cover object is the white image that fades away when the game loads. It covers the screen until the map is finished loading. <br/>
    /// This function is called by its animator via an animation event.
    /// </summary>
    public void DestroyCover()
    {
        Destroy(cover);
    }
}
