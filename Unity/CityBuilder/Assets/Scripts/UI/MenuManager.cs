using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject rightMenu;
    public GameObject utilSubMenu;
    bool inventoryIsOpen = false;
    bool isSubMenuOpen = false;
    bool isRightMenuOpen = true;
    bool inventoryWasOpen = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void ToggleInventory(){
        inventoryIsOpen = !inventoryIsOpen;
        isRightMenuOpen = !isRightMenuOpen;
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

    public void ToggleInventoryFromSubMenu(GameObject subMenu){
        inventoryIsOpen = !inventoryIsOpen;
        isSubMenuOpen = !isSubMenuOpen;
        inventory.GetComponent<Animator>().SetTrigger("toggle");
        inventory.GetComponent<Animator>().SetBool("isOpen", inventoryIsOpen);
        subMenu.GetComponent<Animator>().SetTrigger("toggle");
        subMenu.GetComponent<Animator>().SetBool("isOpen", isSubMenuOpen);
    }
}
