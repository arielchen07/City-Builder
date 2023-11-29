using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationManager : MonoBehaviour
{
    public InventoryList inventory;
    private List<MapTile> availableTiles;

    void Start()
    {
        availableTiles = new List<MapTile>();
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tileGameObject in allTiles)
        {
            MapTile tileScript = tileGameObject.GetComponent<MapTile>();
            if (!tileScript.isOccupied)
            {
                availableTiles.Add(tileScript);
            }
        }
    }

    public void SpawnDecoration()
    {
        Debug.Log("HI");
        List<GameObject> decorations = inventory.inventoryLst.FindAll(item => item.tag == "Decoration");

        if (availableTiles.Count > 0)
        {
            // Randomly select an object from the inventory list
            GameObject randomDecoration = decorations[UnityEngine.Random.Range(0, decorations.Count)];

            // Randomly select a tile from the available tiles list
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
            
                // Instantiate the decoration at the tile's position
               GameObject decorationInstance = Instantiate(randomDecoration, selectedTile.transform.position, Quaternion.identity);

                // Set the tile's properties
               selectedTile.isOccupied = true;
               selectedTile.placedObject = decorationInstance; // Assuming placedObject is a GameObject reference in MapTile

                // Optionally, remove the tile from the available tiles list if it's no longer considered available
               availableTiles.RemoveAt(randomIndex);
        }
    }

}
