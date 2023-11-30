using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class InventoryUITests : MonoBehaviour
{
    private GameObject GameCanvas;
    private MenuManager menuManager;
    [SetUp]
    public void SetUp(){
        SceneManager.LoadScene("MainScene");
    }

    [UnityTest]
    public IEnumerator InventoryInitialStateTest() {
        yield return null;
        GameCanvas = GameObject.FindWithTag("GameCanvas");
        menuManager = GameCanvas.GetComponent<MenuManager>();
        Assert.AreEqual(menuManager.GetInventoryIsOpen(), false);
        Assert.AreEqual(menuManager.GetIsSubMenuOpen(), false);
        Assert.AreEqual(menuManager.GetIsRightMenuOpen(), true);
    }

    [UnityTest]
    public IEnumerator OpenInventoryStateTest() {
        yield return null;
        GameCanvas = GameObject.FindWithTag("GameCanvas");
        menuManager = GameCanvas.GetComponent<MenuManager>();
        menuManager.ToggleInventory("housing");
        Assert.AreEqual(menuManager.GetInventoryIsOpen(), true);
        Assert.AreEqual(menuManager.GetIsRightMenuOpen(), false);
        Assert.AreEqual(menuManager.GetIsSubMenuOpen(), false);
        Assert.AreEqual(menuManager.inventory.GetComponent<Animator>().GetBool("isOpen"), true);
        Assert.AreEqual(menuManager.rightMenu.GetComponent<Animator>().GetBool("isOpen"), false);
        Assert.AreEqual(menuManager.utilSubMenu.GetComponent<Animator>().GetBool("isOpen"), false);
    }

    [UnityTest]
    public IEnumerator CloseInventoryStateTest() {
        yield return null;
        GameCanvas = GameObject.FindWithTag("GameCanvas");
        menuManager = GameCanvas.GetComponent<MenuManager>();
        //opens the inventory
        menuManager.ToggleInventory("housing");
        //closes the inventory
        menuManager.ToggleInventory("housing");
        Assert.AreEqual(menuManager.GetInventoryIsOpen(), false);
        Assert.AreEqual(menuManager.GetIsRightMenuOpen(), true);
        Assert.AreEqual(menuManager.GetIsSubMenuOpen(), false);
        Assert.AreEqual(menuManager.inventory.GetComponent<Animator>().GetBool("isOpen"), false);
        Assert.AreEqual(menuManager.rightMenu.GetComponent<Animator>().GetBool("isOpen"), true);
        Assert.AreEqual(menuManager.utilSubMenu.GetComponent<Animator>().GetBool("isOpen"), false);
    }

    [UnityTest]
    public IEnumerator OpenSubMenuTest(){
        yield return null;
        GameCanvas = GameObject.FindWithTag("GameCanvas");
        menuManager = GameCanvas.GetComponent<MenuManager>();
        menuManager.ToggleSubMenu(menuManager.utilSubMenu);
        Assert.AreEqual(menuManager.GetInventoryIsOpen(), false);
        Assert.AreEqual(menuManager.GetIsRightMenuOpen(), false);
        Assert.AreEqual(menuManager.GetIsSubMenuOpen(), true);
        Assert.AreEqual(menuManager.inventory.GetComponent<Animator>().GetBool("isOpen"), false);
        Assert.AreEqual(menuManager.rightMenu.GetComponent<Animator>().GetBool("isOpen"), false);
        Assert.AreEqual(menuManager.utilSubMenu.GetComponent<Animator>().GetBool("isOpen"), true);       
    }

    //[UnityTest]
    //public IEnumerator OpenInventoryFromSubMenuTest(){
    //    yield return null;
    //    GameCanvas = GameObject.FindWithTag("GameCanvas");
    //    menuManager = GameCanvas.GetComponent<MenuManager>();
    //    //opens submenu first
    //    menuManager.ToggleSubMenu(menuManager.utilSubMenu);
    //    menuManager.ToggleInventoryFromSubMenu(menuManager.utilSubMenu);
    //    Assert.AreEqual(menuManager.GetInventoryIsOpen(), true);
    //    Assert.AreEqual(menuManager.GetIsRightMenuOpen(), false);
    //    Assert.AreEqual(menuManager.GetIsSubMenuOpen(), false);
    //    Assert.AreEqual(menuManager.inventory.GetComponent<Animator>().GetBool("isOpen"), true);
    //    Assert.AreEqual(menuManager.rightMenu.GetComponent<Animator>().GetBool("isOpen"), false);
    //    Assert.AreEqual(menuManager.utilSubMenu.GetComponent<Animator>().GetBool("isOpen"), false); 
    //}

    //[UnityTest]
    //public IEnumerator CloseInventoryFromSubMenuTest(){
    //    yield return null;
    //    GameCanvas = GameObject.FindWithTag("GameCanvas");
    //    menuManager = GameCanvas.GetComponent<MenuManager>();
    //    //opens the submenu
    //    menuManager.ToggleSubMenu(menuManager.utilSubMenu);
    //    //opens inventory from submenu
    //    menuManager.ToggleInventoryFromSubMenu(menuManager.utilSubMenu);
    //    //closes submenu
    //    menuManager.ToggleInventory("housing");
    //    Assert.AreEqual(menuManager.GetInventoryIsOpen(), false);
    //    Assert.AreEqual(menuManager.GetIsRightMenuOpen(), true);
    //    Assert.AreEqual(menuManager.GetIsSubMenuOpen(), false);
    //    Assert.AreEqual(menuManager.inventory.GetComponent<Animator>().GetBool("isOpen"), false);
    //    Assert.AreEqual(menuManager.rightMenu.GetComponent<Animator>().GetBool("isOpen"), true);
    //    Assert.AreEqual(menuManager.utilSubMenu.GetComponent<Animator>().GetBool("isOpen"), false); 
    //}
}
