using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject road1;
    public GameObject road2;
    public GameObject road3;
    public GameObject road4;
    public List<GameObject> adjacentRoads;

    void Start(){
        ChooseActiveRoadObject(1); 
        adjacentRoads = new List<GameObject>();
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = Vector3.Scale(collider.size, collider.transform.lossyScale) * 0.5f;
        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation);
        foreach (Collider col in cols) {
            if (col.gameObject.CompareTag("Tile")) {
                col.gameObject.GetComponent<MapTile>().isOccupied = true;
                col.gameObject.GetComponent<MapTile>().placedObject = gameObject;
            }
        }
        transform.parent = GameObject.Find("RoadsContainer").transform;
    }

    void Update(){
        CheckAdjacentRoads();
    }
    public void CheckAdjacentRoads()
    {
        adjacentRoads.Clear();
        //check for roads in each adjacent tile
        GetAdjacent(new Vector3(1,0,0));
        GetAdjacent(new Vector3(-1,0,0));
        GetAdjacent(new Vector3(0,0,1));
        GetAdjacent(new Vector3(0,0,-1));

        if (adjacentRoads.Count == 4) {
            ChooseActiveRoadObject(4);
        } else if (adjacentRoads.Count == 3) {
            ChooseActiveRoadObject(3);
            Vector3 totalOffset = Vector3.zero;
            foreach (GameObject road in adjacentRoads) {
                Vector3 positionDiff = road.transform.position - transform.position;
                totalOffset += positionDiff;
            }
            if (totalOffset == new Vector3(0,0,1)) {
                transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            } else if (totalOffset == new Vector3(1,0,0)) {
                transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
            } else if (totalOffset == new Vector3(0,0,-1)) {
                transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
            } else if (totalOffset == new Vector3(-1,0,0)) {
                transform.rotation = Quaternion.Euler(new Vector3(0,270,0));
            }
        } else if (adjacentRoads.Count == 2) {
            ChooseActiveRoadObject(2);
            Vector3 totalOffset = Vector3.zero;
            foreach (GameObject road in adjacentRoads) {
                Vector3 positionDiff = road.transform.position - transform.position;
                totalOffset += positionDiff;
            }
            if (totalOffset == new Vector3(1,0,1)){
                transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            } else if (totalOffset == new Vector3(1,0,-1)){
                transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
            } else if (totalOffset == new Vector3(-1,0,-1)){
                transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
            } else if (totalOffset == new Vector3(-1,0,1)){
                transform.rotation = Quaternion.Euler(new Vector3(0,270,0));
            } else if (totalOffset == new Vector3(0,0,0)){
                ChooseActiveRoadObject(1);
                Vector3 positionDiff = adjacentRoads[0].transform.position - adjacentRoads[1].transform.position;
                if (positionDiff.x == 0){
                    transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
                }
                else {
                    transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
                }
            }
        } else if (adjacentRoads.Count == 1) {
            Vector3 positionDiff = adjacentRoads[0].transform.position - transform.position;
            if (positionDiff.x == 0){
                    transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
                }
                else {
                    transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
                }
            ChooseActiveRoadObject(1);
        }
    }

    void GetAdjacent(Vector3 offset) {
        Collider[] cols = Physics.OverlapSphere(transform.position + offset, 0.1f, LayerMask.GetMask("Foreground"));
        foreach (Collider col in cols) {
            if (col.gameObject.TryGetComponent<Road>(out var road)){
                adjacentRoads.Add(road.gameObject);
            }
        }
    }

    void ChooseActiveRoadObject(int num){
        road1.SetActive(num == 1);
        road2.SetActive(num == 2);
        road3.SetActive(num == 3);
        road4.SetActive(num == 4);
    }
}
