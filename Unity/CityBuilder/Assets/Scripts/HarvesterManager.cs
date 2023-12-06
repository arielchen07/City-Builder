using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvesterManager : MonoBehaviour
{
    public static HarvesterManager harvesterManager;
    public int numTreeHarvester = 0;
    public int numOccupiedTreeHarvesters = 0;
    public int numRockHarvester = 0;
    public int numOccupiedRockHarvesters = 0;
    public float cooldown;
    public GameObject harvestButton;
    private void Awake(){
        if (harvesterManager != null && harvesterManager != this){
            Destroy(this);
        } else {
            harvesterManager = this;
        }
    }

    public void AddTreeHarvester(){
        numTreeHarvester += 1;
    }

    public void AddRockHarvester(){
        numRockHarvester += 1;
    }
    public void RemoveTreeHarvester(){
        numTreeHarvester -= 1;
    }

    public void RemoveRockHarvester(){
        numRockHarvester -= 1;
    }

    public bool AddActiveTreeHarvester(){
        if(numOccupiedTreeHarvesters >= numTreeHarvester) {
            return false;
        }
        numOccupiedTreeHarvesters += 1;
        Invoke("ResetTreeHarvester", cooldown);
        DisableButton();
        return true;
    }
    public bool AddActiveRockHarvester(){
        if(numOccupiedRockHarvesters >= numRockHarvester) {
            return false;
        }
        numOccupiedRockHarvesters += 1;
        Invoke("ResetRockHarvester", cooldown);
        DisableButton();
        return true;
    }

    public void ResetTreeHarvester(){
        numOccupiedTreeHarvesters--;
        EnableButton();
    }

    public void ResetRockHarvester(){
        numOccupiedRockHarvesters--;
        EnableButton();
    }

    public void EnableButton(){
        if(numOccupiedRockHarvesters < numRockHarvester || numOccupiedTreeHarvesters < numTreeHarvester){
            harvestButton.GetComponent<Image>().color = new Color(1,1,1,1);
            harvestButton.GetComponent<Button>().interactable = true;
        }
    }
    
    public void DisableButton(){
        if(numOccupiedRockHarvesters == numRockHarvester && numOccupiedTreeHarvesters == numTreeHarvester){
            harvestButton.GetComponent<Image>().color = new Color(1,1,1,0.7f);
            harvestButton.GetComponent<Button>().interactable = false;
        }
    }
}
