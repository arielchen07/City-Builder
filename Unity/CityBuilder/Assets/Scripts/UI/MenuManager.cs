using System.Collections;
using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject rightMenu;
    public GameObject utilSubMenu;
    bool inventoryIsOpen = false;
    bool isSubMenuOpen = false;
    bool isRightMenuOpen = true;
    bool inventoryWasOpen = false;
    public List<GameObject> roads;
    public List<GameObject> housing;
    public List<GameObject> power;
    public List<GameObject> sewage;
    public List<GameObject> water;
    public GameObject content;
    public PlacementSystem ps;
    public InventoryManager inventoryManager;
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
            newButton.GetComponent<Button>().onClick.AddListener(() => newButton.GetComponent<ItemUI>().PlaceItem());
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

    public bool GetInventoryIsOpen(){
        return inventoryIsOpen;
    }

    public bool GetIsSubMenuOpen(){
        return isSubMenuOpen;
    }

    public bool GetIsRightMenuOpen(){
        return isRightMenuOpen;
    }
}
