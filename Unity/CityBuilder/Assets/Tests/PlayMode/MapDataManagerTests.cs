using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameMapTests
{
    [UnityTest]
    public IEnumerator TestClearGameMap()
    {
        // Create test game objects
        GameObject testPlaceableObject = new GameObject("TestPlaceableObject");
        GameObject testRoad = new GameObject("TestRoad");
        GameObject testTile = new GameObject("TestTile");

        testPlaceableObject.AddComponent<PlaceableObject>();
        testRoad.AddComponent<Road>();
        MapTile tile = testTile.AddComponent<MapTile>();
        tile.isOccupied = true;
        tile.placedObject = testPlaceableObject;

        // Instantiate the MapDataManager
        MapDataManager mapDataManager = new GameObject().AddComponent<MapDataManager>();

        // Call the ClearGameMap method
        mapDataManager.ClearGameMap();

        // Wait for a frame so that Unity can process the Destroy calls
        yield return null;

        Assert.IsNull(GameObject.FindObjectOfType<PlaceableObject>(), "PlaceableObject was not destroyed.");
        Assert.IsNull(GameObject.FindObjectOfType<Road>(), "Road was not destroyed.");

        MapTile[] tiles = GameObject.FindObjectsOfType<MapTile>();
        foreach (var remainingTile in tiles)
        {
            Assert.IsFalse(remainingTile.isOccupied, "Tile is still occupied.");
            Assert.IsNull(remainingTile.placedObject, "Tile still has a reference to placedObject.");
        }

        // Cleanup
        Object.DestroyImmediate(mapDataManager.gameObject);
        Object.DestroyImmediate(testTile);
    }

    [UnityTest]
    public IEnumerator TestSerializeAllGameObjects()
    {
        MapDataManager mapDataManager = new GameObject().AddComponent<MapDataManager>();
        PlacementSystem mockPlacementSystem = new GameObject().AddComponent<PlacementSystem>();
        mapDataManager.placementSystem = mockPlacementSystem;

        GameObject obj1 = new GameObject("PlaceableObject1");
        PlaceableObject placeable1 = obj1.AddComponent<PlaceableObject>();
        GameObject obj2 = new GameObject("PlaceableObject2");
        PlaceableObject placeable2 = obj2.AddComponent<PlaceableObject>();
        GameObject road1 = new GameObject("Road1");
        road1.AddComponent<Road>();

        string serializedData = mapDataManager.SerializeAllGameObjects();

        Assert.IsTrue(serializedData.Contains("PlaceableObject1"), "Serialized data does not contain PlaceableObject1.");
        Assert.IsTrue(serializedData.Contains("PlaceableObject2"), "Serialized data does not contain PlaceableObject2.");
        Assert.IsTrue(serializedData.Contains("Road1"), "Serialized data does not contain Road1.");

        Object.DestroyImmediate(obj1);
        Object.DestroyImmediate(obj2);
        Object.DestroyImmediate(road1);
        Object.DestroyImmediate(mapDataManager.gameObject);
        Object.DestroyImmediate(mockPlacementSystem.gameObject);

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestRemoveAllTiles()
    {
        GameObject testGameObject = new GameObject("MapDataManager");
        MapDataManager mapDataManager = testGameObject.AddComponent<MapDataManager>();
        GameObject tile1 = new GameObject("Tile1");
        tile1.AddComponent<MapTile>();
        GameObject tile2 = new GameObject("Tile2");
        tile2.AddComponent<MapTile>();

        yield return null; 

        Assert.IsNotNull(GameObject.Find("Tile1"), "Tile1 does not exist before RemoveAllTiles.");
        Assert.IsNotNull(GameObject.Find("Tile2"), "Tile2 does not exist before RemoveAllTiles.");

        mapDataManager.RemoveAllTiles();

        yield return null;

        Assert.IsNull(GameObject.Find("Tile1"), "Tile1 was not removed.");
        Assert.IsNull(GameObject.Find("Tile2"), "Tile2 was not removed.");

        Object.DestroyImmediate(mapDataManager.gameObject);
    }
}
