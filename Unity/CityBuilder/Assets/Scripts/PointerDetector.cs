using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The PointerDetector class is responsible for creating the grid effect. <br/>
/// It moves an invisible gameobject that checks the objects in a range around it, and returns the closest gameobject. <br/>
/// This allows objects to be placed at the center of objects, creating a grid effect.
/// </summary>
public class PointerDetector : MonoBehaviour
{
    public GameObject currentlyColliding;
    public GameObject indicator;
    public GameObject currentlyPlacing;

    /// <summary>
    /// Uses the invisible collider attached to the indicator object to find the list of nearby objects, and sets currentlyColliding to the closest object.
    /// </summary>
    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f, LayerMask.GetMask("Ground"));
        GameObject closest = null;
        float closestDistance = 100f; //arbitrary large number
        foreach (Collider col in cols)
        {
            float distance = (col.gameObject.transform.position - transform.position).magnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = col.gameObject;
            }
        }
        if (closest != null)
        {
            currentlyColliding = closest;
            Vector3 pos = currentlyColliding.transform.position;
            indicator.transform.position = new Vector3(pos.x, 0, pos.z);
        }
    }

    /// <summary>
    /// Aligns currentlyPlacing so that the cursor is always at the center of a tile of the object being placed. <br/>
    /// For even dimensioned objects, for example 2x2, the object should not be centered at the cursor since it would be placed at an offset.
    /// </summary>
    public void AlignObject()
    {
        if (currentlyPlacing.GetComponent<PlaceableObject>().evenDimensions)
        {
            currentlyPlacing.transform.localPosition = new Vector3(-0.5f, 0, -0.5f);
        }
    }
}
