using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to all map tiles. <br/>
/// It contains information used by the placement system. It tracks its occupancy and the objects placed on top of it.
/// </summary>
public class MapTile : MonoBehaviour
{
    public bool isOccupied;
    public bool hasDecorations;
    public GameObject placedObject;
    public int numDecorations;
}
