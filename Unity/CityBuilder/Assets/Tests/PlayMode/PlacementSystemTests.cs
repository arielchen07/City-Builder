using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEditor;

public class PlacementSystemTests
{
    private GameObject house = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Buildings/SingleHouse1.prefab");
    private GameObject houseItem = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/UIButtons/HouseButton.prefab");
    PlacementSystem ps;
    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    [UnityTest]
    public IEnumerator InitialCurrentlyPlacingStateTest()
    {
        yield return null;
        ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>(); 
        Assert.AreEqual(null, ps.currentlyPlacing);
    }

    [UnityTest]
    public IEnumerator HoverObjectTest()
    {
        ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>(); 
        GameObject newHouse = UnityEngine.Object.Instantiate(house);
        ps.HoverObject(houseItem);
        yield return null;
        Assert.AreNotEqual(null, ps.currentlyPlacing);
    }

    [UnityTest]
    public IEnumerator DropObjectTest()
    {
        ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>();
        GameObject newHouse = UnityEngine.Object.Instantiate(house);
        ps.currentlyPlacing = newHouse;
        ps.DropObject();
        yield return null;
        Assert.AreEqual(null, ps.currentlyPlacing);
    }

    // [UnityTest]
    // public IEnumerator PlaceObjectTest()
    // {
    //     ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>();
    //     GameObject newHouse = UnityEngine.Object.Instantiate(house);
    //     ps.HoverObject(houseItem);
    //     yield return null;
    //     ps.PlaceObject();
    //     Assert.AreEqual(null, ps.currentlyPlacing);
    // }

    // [UnityTest]
    // public IEnumerator CurrentlyPlacingStateWhenSelectObjectTest()
    // {
    //     ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>();
    //     GameObject newHouse = UnityEngine.Object.Instantiate(house);
    //     ps.currentlyHovering = newHouse;
    //     ps.SelectObject();
    //     yield return null;
    //     Assert.AreEqual(ps.currentlySelecting, ps.currentlyHovering);
    // }
}
