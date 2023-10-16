using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEditor;

public class NewTestScript
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
        Assert.AreEqual(ps.currentlyPlacing, null);
    }

    [UnityTest]
    public IEnumerator HoverObjectTest()
    {
        yield return new WaitForSeconds(1);
        ps = GameObject.FindWithTag("PlacementSystem").GetComponent<PlacementSystem>(); 
        GameObject newHouse = UnityEngine.Object.Instantiate(house);
        ps.HoverObject(newHouse);
        Assert.AreNotEqual(ps.currentlyPlacing, null);
    }
}
