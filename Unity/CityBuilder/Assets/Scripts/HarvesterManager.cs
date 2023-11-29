using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterManager : MonoBehaviour
{
    public static HarvesterManager harvesterManager;
    public int numTreeHarvester = 0;
    public int numOccupiedTreeHarvesters = 0;
    public int numRockHarvester = 0;
    public int numOccupiedRockHarvesters = 0;
    private void Awake(){
        if (harvesterManager != null && harvesterManager != this){
            Destroy(this);
        } else {
            harvesterManager = this;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
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
        return true;
    }
    public bool AddActiveRockHarvester(){
        if(numOccupiedRockHarvesters >= numRockHarvester) {
            return false;
        }
        numOccupiedRockHarvesters += 1;
        return true;
    }
}
