using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public List<GameObject> harvester;
    public GameObject content;
    public PlacementSystem ps;
    public InventoryManager inventoryManager;
    public GameObject cover;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadInventory(string category){
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
            case "harvester":
                buttonList = harvester;
                break;
            default:
                buttonList = housing;
            break;
        }
        foreach(Transform child in content.transform){
            Destroy(child.gameObject);
        }
        foreach(GameObject button in buttonList){
            GameObject newButton = Instantiate(button, content.transform);
            newButton.GetComponent<ItemUI>().inventoryManager = inventoryManager;
            newButton.GetComponent<Button>().onClick.AddListener(() => ps.HoverObject(newButton));
            newButton.GetComponent<Button>().onClick.AddListener(() => newButton.GetComponent<ItemUI>().TakeItem());
        }
    }
    public void CloseInventory(){
        if(inventoryIsOpen){
            inventoryWasOpen = true;
        } else {
            inventoryWasOpen = false;
        }
        inventory.GetComponent<Animator>().SetTrigger("toggle");
        inventory.GetComponent<Animator>().SetBool("isOpen", false);
    }
    public void OpenInventory(){
        if(inventoryWasOpen){
            inventory.GetComponent<Animator>().SetTrigger("toggle");
            inventory.GetComponent<Animator>().SetBool("isOpen", true);      
        }  
    }
    public void ToggleInventory(string category){
        inventoryIsOpen = !inventoryIsOpen;
        isRightMenuOpen = !isRightMenuOpen;
        if(inventoryIsOpen){
            LoadInventory(category);
        }
        inventory.GetComponent<Animator>().SetTrigger("toggle");
        inventory.GetComponent<Animator>().SetBool("isOpen", inventoryIsOpen);
        rightMenu.GetComponent<Animator>().SetTrigger("toggle");
        rightMenu.GetComponent<Animator>().SetBool("isOpen", isRightMenuOpen);
    }

    public void ToggleSubMenu(GameObject subMenu){
        isSubMenuOpen = !isSubMenuOpen;
        isRightMenuOpen = !isRightMenuOpen;
        subMenu.GetComponent<Animator>().SetTrigger("toggle");
        subMenu.GetComponent<Animator>().SetBool("isOpen", isSubMenuOpen);
        rightMenu.GetComponent<Animator>().SetTrigger("toggle");
        rightMenu.GetComponent<Animator>().SetBool("isOpen", isRightMenuOpen);
    }

    public void ToggleInventoryFromSubMenu(GameObject callingButton){
        GameObject subMenu = callingButton.transform.parent.gameObject;
        inventoryIsOpen = !inventoryIsOpen;
        isSubMenuOpen = !isSubMenuOpen;
        if(inventoryIsOpen){
            LoadInventory(callingButton.name);
        }
        inventory.GetComponent<Animator>().SetTrigger("toggle");
        inventory.GetComponent<Animator>().SetBool("isOpen", inventoryIsOpen);
        subMenu.GetComponent<Animator>().SetTrigger("toggle");
        subMenu.GetComponent<Animator>().SetBool("isOpen", isSubMenuOpen);
    }

    public void ToggleRightMenu(){
        isRightMenuOpen = !isRightMenuOpen;
        rightMenu.GetComponent<Animator>().SetTrigger("toggle");
        rightMenu.GetComponent<Animator>().SetBool("isOpen", isRightMenuOpen);
    }

    public void ToggleRoadMenu(){
        isRoadMenuOpen = !isRoadMenuOpen;
        roadMenu.GetComponent<Animator>().SetTrigger("toggle");
        roadMenu.GetComponent<Animator>().SetBool("isOpen", isRoadMenuOpen);
    }
    public void ToggleRoadPlacementModeButtonVisual(bool isPlacing){
        if(isPlacing){
            placeRoads.color = new Color(1,1,1);
            deleteRoads.color = new Color(0.5f,0.5f,0.5f);
        }else {
            placeRoads.color = new Color(0.5f,0.5f,0.5f);
            deleteRoads.color = new Color(1,1,1);
        }
    }
    public bool GetInventoryIsOpen(){
        return inventoryIsOpen;
    }

    public bool GetIsSubMenuOpen(){
        return isSubMenuOpen;
    }

    public bool GetIsRightMenuOpen(){
        return isRightMenuOpen;
    }

    public void DestroyCover(){
        Destroy(cover);
    }
}
