using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    public InventoryList inventory;
    private List<MapTile> availableTiles;
    public int maxDecorationsPerTile;

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
            if (!tileScript.isOccupied && tileScript.numDecorations < maxDecorationsPerTile)
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
            Vector2 randomOffset = new Vector2(UnityEngine.Random.Range(-0.5f,0.5f), UnityEngine.Random.Range(-0.5f,0.5f));
            float randomRotation = UnityEngine.Random.Range(0, 360);
            GameObject decorationInstance = Instantiate(randomDecoration, selectedTile.transform.position + new Vector3(randomOffset.x, 0 , randomOffset.y), Quaternion.identity);
            decorationInstance.transform.Rotate(new Vector3(0,randomRotation, 0));
            selectedTile.hasDecorations = true;
            selectedTile.numDecorations += 1;
            if(selectedTile.numDecorations == maxDecorationsPerTile){
                availableTiles.RemoveAt(randomIndex);
            }
        }
    }

}
