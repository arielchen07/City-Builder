using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StructureObjsSerialization
{
    public List<StructureObjSerialization> structureObjData = new List<StructureObjSerialization>();

    public void AddObj(string name, Vector3 position, Vector3 rotation)
    {
        structureObjData.Add(new StructureObjSerialization(name, position, rotation));
    }

}

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

[Serializable]
public class TileObjsSerialization
{
    public List<TileSerialization> tileData = new List<TileSerialization>();

    public void AddTile(string name, Vector3 position, Vector3 rotation, bool isOccupied)
    {
        tileData.Add(new TileSerialization(name, position, rotation, isOccupied));
    }
}

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
