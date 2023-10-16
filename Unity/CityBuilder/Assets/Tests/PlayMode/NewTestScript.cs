using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class NewTestScript
{
    //[SetUp]
    //public void ResetScene()
    //{
    //    //EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
    //    SceneManager.CreateNewScene()

    //}
    private GameObject placementSystemGO;
    private PlacementSystem placementSystem;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject and add the PlacementSystem component to it.
        //GameObject gameObject = new GameObject();
        //placementSystem = gameObject.AddComponent<PlacementSystem>();
        placementSystemGO = new GameObject("PlacementSystem");
        placementSystem = placementSystemGO.AddComponent<PlacementSystem>();
    }

    [UnityTest]
    public IEnumerator InitialCurrentlyPlacingStateTest()
    {
        //var gameObject = new GameObject();
        //var placementSystem = gameObject.AddComponent<PlacementSystem>();

        Assert.IsNotNull(placementSystem);

        Assert.AreEqual(null, placementSystem.currentlyPlacing);

        yield return null;
    }

    [UnityTest]
    public IEnumerator HoverObjectTest()
    {
        //var gameObject = new GameObject();
        //var placementSystem = gameObject.AddComponent<PlacementSystem>();

        placementSystem.HoverObject(new GameObject());

        yield return new WaitForSeconds(5);

        Assert.AreNotEqual(null, placementSystem.currentlyPlacing);


        yield return null;
    }
}
