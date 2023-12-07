using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable wrapper data structure for map data to be send to server
/// </summary>
[Serializable]
public class MapRequestBody
{
    public string mapData;

    public MapRequestBody(string data)
    {
        mapData = data;
    }
}

/// <summary>
/// Serializable data structure for map data
/// </summary>
[Serializable]
public class MapSerialization
{
    public List<StructureObjSerialization> structureObjData = new List<StructureObjSerialization>();
    public List<TileSerialization> tileData = new List<TileSerialization>();
    public List<DecorSerialization> decorData = new List<DecorSerialization>();

    public void AddStructure(string name, Vector3 position, Vector3 rotation)
    {
        structureObjData.Add(new StructureObjSerialization(name, position, rotation));
    }

    public void AddTile(string name, Vector3 position, Vector3 rotation, bool isOccupied)
    {
        tileData.Add(new TileSerialization(name, position, rotation, isOccupied));
    }

    public void AddDecor(string name, Vector3 position, Vector3 rotation)
    {
        decorData.Add(new DecorSerialization(name, position, rotation));
    }

}

/// <summary>
/// Serializable data structure for building objects
/// </summary>
[Serializable]
public class StructureObjSerialization
{
    public string name;
    public Vector3Serialization position;
    public Vector3Serialization rotation;

    public StructureObjSerialization(string name, Vector3 position, Vector3 rotation)
    {
        this.name = name;
        this.position = new Vector3Serialization(position);
        this.rotation = new Vector3Serialization(rotation);
    }
}

/// <summary>
/// Serializable data structure for tile objects
/// </summary>
[Serializable]
public class TileSerialization
{
    public string name;
    public Vector3Serialization position;
    public Vector3Serialization rotation;
    public bool isOccupied;

    public TileSerialization(string name, Vector3 position, Vector3 rotation, bool isOccupied)
    {
        this.name = name;
        this.position = new Vector3Serialization(position);
        this.rotation = new Vector3Serialization(rotation);
        this.isOccupied = isOccupied;
    }
}

/// <summary>
/// Serializable data structure for decor objects such as trees
/// </summary>
[Serializable]
public class DecorSerialization
{
    public string name;
    public Vector3Serialization position;
    public Vector3Serialization rotation;

    public DecorSerialization(string name, Vector3 position, Vector3 rotation)
    {
        this.name = name;
        this.position = new Vector3Serialization(position);
        this.rotation = new Vector3Serialization(rotation);
    }
}

/// <summary>
/// Serializable data structure vector of 3 elements
/// </summary>
[Serializable]
public class Vector3Serialization
{
    public float x, y, z;

    public Vector3Serialization(Vector3 position)
    {
        this.x = position.x;
        this.y = position.y;
        this.z = position.z;
    }

    public Vector3 GetValue()
    {
        return new Vector3(x, y, z);
    }
}
