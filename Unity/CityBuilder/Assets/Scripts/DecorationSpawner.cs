using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The DecorationSpawner class is attached to the MapDataManager object. <br/>
/// This class is responsible for spawning decorations (currently trees and rocks) in the map periodically.
/// </summary>
public class DecorationSpawner : MonoBehaviour
{
    public InventoryList inventory;
    private List<MapTile> availableTiles;
    public int maxDecorationsPerTile;

    /// <summary>
    /// Spawns a random decoration with a random offset and rotation on an available tile, updating pollutionManager if necessary. <br/>
    /// This function is called by MapDataManager at a set interval.
    /// </summary>
    public void SpawnDecoration()
    {
        UpdateAvailableTiles();

        List<GameObject> decorations = inventory.inventoryLst.FindAll(item => item.tag == "Decoration");

        if (availableTiles.Count > 0)
        {
            GameObject randomDecoration = decorations[UnityEngine.Random.Range(0, decorations.Count)];

            int randomIndex = UnityEngine.Random.Range(0, availableTiles.Count);
            MapTile selectedTile = availableTiles[randomIndex];
            while (true)
            {
                if (selectedTile.isOccupied)
                {
                    availableTiles.RemoveAt(randomIndex);
                    selectedTile = availableTiles[randomIndex];
                }
                else
                {
                    break;
                }
            }
            Vector2 randomOffset = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
            int randomRotation = UnityEngine.Random.Range(0, 4);
            GameObject decorationInstance = Instantiate(randomDecoration, selectedTile.transform.position + new Vector3(randomOffset.x, 0, randomOffset.y), Quaternion.identity);
            decorationInstance.transform.Rotate(new Vector3(0, randomRotation * 90, 0));
            if (decorationInstance.GetComponent<Decoration>().resourceType == "wood")
            {
                PollutionManager.pollutionManager.numTrees++;
            }
            selectedTile.hasDecorations = true;
            selectedTile.numDecorations += 1;
            if (selectedTile.numDecorations == maxDecorationsPerTile)
            {
                availableTiles.RemoveAt(randomIndex);
            }
        }
    }

    /// <summary>
    /// Updates the availableTiles list.
    /// </summary>
    private void UpdateAvailableTiles()
    {
        availableTiles = new List<MapTile>();
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tileGameObject in allTiles)
        {
            MapTile tileScript = tileGameObject.GetComponent<MapTile>();
            if (!tileScript.isOccupied && tileScript.numDecorations < maxDecorationsPerTile)
            {
                availableTiles.Add(tileScript);
            }
        }
    }

}
