using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The HarvesterManager class is responsible for tracking how many total harvester buildings of each type is placed, and how many are available. <br/>
/// This class is a singleton and globally accessible.
/// </summary>
public class HarvesterManager : MonoBehaviour
{
    public static HarvesterManager harvesterManager;
    public int numTreeHarvester = 0;
    public int numOccupiedTreeHarvesters = 0;
    public int numRockHarvester = 0;
    public int numOccupiedRockHarvesters = 0;
    public float cooldown;
    public GameObject harvestButton;
    private void Awake()
    {
        if (harvesterManager != null && harvesterManager != this)
        {
            Destroy(this);
        }
        else
        {
            harvesterManager = this;
        }
    }
    void Start()
    {
        //checks if the user currently has any available harvesters placed, and enables or disables the button accordingly.
        DisableButton();
        EnableButton();
    }
    /// <summary>
    /// Increments the tree harvester count and attempts the enable the harvest button.
    /// </summary>
    public void AddTreeHarvester()
    {
        numTreeHarvester += 1;
        EnableButton();
    }
    /// <summary>
    /// Increments the rock harvester count and attempts the enable the harvest button.
    /// </summary>
    public void AddRockHarvester()
    {
        numRockHarvester += 1;
        EnableButton();
    }
    /// <summary>
    /// Decrements the tree harvester count and attempts to disable the harvest button.
    /// </summary>
    public void RemoveTreeHarvester()
    {
        numTreeHarvester -= 1;
        DisableButton();
    }
    /// <summary>
    /// Decrements the rock harvester count and attempts to disable the harvest button.
    /// </summary>
    public void RemoveRockHarvester()
    {
        numRockHarvester -= 1;
        DisableButton();
    }
    /// <summary>
    /// Increments the active tree harvester count and starts a cooldown for the added tree harvester. <br/>
    /// Attempts to disable the harvest button.
    /// </summary>
    /// <returns>True if there were available tree harvesters before calling this function, and False otherwise.</returns>
    public bool AddActiveTreeHarvester()
    {
        if (numOccupiedTreeHarvesters >= numTreeHarvester)
        {
            return false;
        }
        numOccupiedTreeHarvesters += 1;
        Invoke("ResetTreeHarvester", cooldown);
        DisableButton();
        return true;
    }
    /// <summary>
    /// Increments the active rock harvester count and starts a cooldown for the added rock harvester. <br/>
    /// Attempts to disable the harvest button.
    /// </summary>
    /// <returns>True if there were available rock harvesters before calling this function, and False otherwise.</returns>
    public bool AddActiveRockHarvester()
    {
        if (numOccupiedRockHarvesters >= numRockHarvester)
        {
            return false;
        }
        numOccupiedRockHarvesters += 1;
        Invoke("ResetRockHarvester", cooldown);
        DisableButton();
        return true;
    }
    /// <summary>
    /// Resets the tree harvester after its cooldown expired. Attempts to enable the harvest button.
    /// </summary>
    public void ResetTreeHarvester()
    {
        numOccupiedTreeHarvesters--;
        EnableButton();
    }
    /// <summary>
    /// Resets the rock harvester after its cooldown expired. Attempts to enable the harvest button.
    /// </summary>
    public void ResetRockHarvester()
    {
        numOccupiedRockHarvesters--;
        EnableButton();
    }
    /// <summary>
    /// Enables the harvest button if there is any available harvester.
    /// </summary>
    public void EnableButton()
    {
        if (numOccupiedRockHarvesters < numRockHarvester || numOccupiedTreeHarvesters < numTreeHarvester)
        {
            harvestButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            harvestButton.GetComponent<Button>().interactable = true;
        }
    }
    /// <summary>
    /// Disables the harvest button if there are no available harvesters.
    /// </summary>
    public void DisableButton()
    {
        if (numOccupiedRockHarvesters == numRockHarvester && numOccupiedTreeHarvesters == numTreeHarvester)
        {
            harvestButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
            harvestButton.GetComponent<Button>().interactable = false;
        }
    }
    /// <summary>
    /// Update the number of available harvesters. <br/>
    /// This function is only called once the map has been loaded, so the manager has a reference of existing harvesters.
    /// Attempts to enable the harvest button.
    /// </summary>
    public void UpdateHarvester()
    {
        List<RockHarvester> rockHarvesters = new List<RockHarvester>(FindObjectsOfType<RockHarvester>());
        List<TreeHarvester> treeHarvesters = new List<TreeHarvester>(FindObjectsOfType<TreeHarvester>());
        numRockHarvester = rockHarvesters.Count;
        numTreeHarvester = treeHarvesters.Count;
        EnableButton();
    }
}
