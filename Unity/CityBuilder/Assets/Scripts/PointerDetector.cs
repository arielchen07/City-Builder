using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PointerDetector : MonoBehaviour
{
    public GameObject currentlyColliding;
    public GameObject indicator;
    public GameObject currentlyPlacing;

    void Update(){
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f, LayerMask.GetMask("Ground"));
        GameObject closest = null;
        float closestDistance = 100f; //arbitrary large number
        foreach(Collider col in cols){
            float distance = (col.gameObject.transform.position - transform.position).magnitude;
            if (distance < closestDistance) {
                closestDistance = distance;
                closest = col.gameObject;
            }
        }
        if(closest != null){
            currentlyColliding = closest;
            Vector3 pos = currentlyColliding.transform.position;
            indicator.transform.position = new Vector3(pos.x, 0, pos.z);
        }
    }

    public void AlignObject(){
        if(currentlyPlacing.GetComponent<PlaceableObject>().evenDimensions){
            currentlyPlacing.transform.localPosition = new Vector3(-0.5f, 0, -0.5f);
        }
    }
}
