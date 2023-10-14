using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private Vector3 mousePos;

    [SerializeField]
    public LayerMask placementLayermask;
    public GameObject hitObject = null;

    public Vector3 GetSelectedMapPosition(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0.3f;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayermask)) {
            this.mousePos = hit.point;
            hitObject = hit.transform.gameObject;
        }
        return this.mousePos;
    }
}
