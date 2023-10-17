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
    private GameObject house = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/SingleHouse1.prefab");
    PlacementSystem ps;
    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    [UnityTest]
    public IEnumerator InitialCurrentlyPlacingStateTest()
    {
        yield return new WaitForSeconds(1);
        ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>(); 
        Assert.AreEqual(null, ps.currentlyPlacing);
    }

    [UnityTest]
    public IEnumerator HoverObjectTest()
    {
        ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>(); 
        GameObject newHouse = UnityEngine.Object.Instantiate(house);
        ps.HoverObject(newHouse);
        yield return new WaitForSeconds(1);
        Assert.AreNotEqual(null, ps.currentlyPlacing);
    }

    [UnityTest]
    public IEnumerator DropObjectTest()
    {
        ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>();
        GameObject newHouse = UnityEngine.Object.Instantiate(house);
        ps.currentlyPlacing = newHouse;
        ps.DropObject();
        yield return new WaitForSeconds(1);
        Assert.AreEqual(null, ps.currentlyPlacing);
    }

    [UnityTest]
    public IEnumerator PlaceObjectTest()
    {
        ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>();
        GameObject newHouse = UnityEngine.Object.Instantiate(house);
        foreach (GameObject tile in ps.currentlyPlacing.GetComponent<PlaceableObject>().currentlyColliding)
        {
            tile.GetComponent<MapTile>().isOccupied = false;
            tile.GetComponent<MapTile>().placedObject = null;
        }
        ps.HoverObject(newHouse);
        //Assert.AreEqual(true, newHouse.GetComponent<PlaceableObject>().canBePlaced);
        ps.PlaceObject();
        yield return new WaitForSeconds(1);
        Assert.AreEqual(null, ps.currentlyPlacing);
    }

    [UnityTest]
    public IEnumerator CurrentlyPlacingStateWhenSelectObjectTest()
    {
        ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>();
        GameObject newHouse = UnityEngine.Object.Instantiate(house);
        ps.currentlySelecting = newHouse;
        ps.SelectObject();
        yield return new WaitForSeconds(1);
        Assert.AreEqual(ps.currentlyPlacing, ps.currentlySelecting);
    }
}
