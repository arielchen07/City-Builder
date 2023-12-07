using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The IHarvester interface is implemented by all harvest type buildings
/// It provides two functions that add and remove harvesters from the HarvesterManager class
/// </summary>
public interface IHarvester
{
    /// <summary>
    /// Adds a harvester to HarvesterManager
    /// </summary>
    public void AddHarvester();
    /// <summary>
    /// Removes a harvester to HarvesterManager
    /// </summary>
    public void RemoveHarvester();
}
