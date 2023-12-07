using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The InputManager class is responsible for tracking what object the cursor is currently on. <br/>
/// It does so via raycast.
/// </summary>
public class InputManager : MonoBehaviour
{
    public Camera cam;
    private Vector3 mousePos;
    public LayerMask placementLayermask;
    public GameObject hitObject = null;

    /// <summary>
    /// Get the position of the cursor projected onto the map.
    /// </summary>
    /// <returns>The position of the cursor on the map</returns>
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0.3f;
        Ray ray = cam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, placementLayermask))
        {
            this.mousePos = hit.point;
            hitObject = hit.transform.gameObject;
        }
        return this.mousePos;
    }
}
