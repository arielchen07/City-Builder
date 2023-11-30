using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    public InventoryList inventory;
    private List<MapTile> availableTiles;

    void Start()
    {

    }

    public void SpawnDecoration()
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
            
               GameObject decorationInstance = Instantiate(randomDecoration, selectedTile.transform.position, Quaternion.identity);

               selectedTile.isOccupied = true;

               availableTiles.RemoveAt(randomIndex);
        }
    }

}
