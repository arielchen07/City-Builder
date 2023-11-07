using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject rightMenu;
    bool inventoryIsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleInventory(){
        inventoryIsOpen = !inventoryIsOpen;
        inventory.GetComponent<Animator>().SetTrigger("toggle");
        inventory.GetComponent<Animator>().SetBool("isOpen", inventoryIsOpen);
        rightMenu.GetComponent<Animator>().SetTrigger("toggle");
        rightMenu.GetComponent<Animator>().SetBool("isOpen", !inventoryIsOpen);
    }
}
