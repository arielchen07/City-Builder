using UnityEngine;
using NUnit.Framework;

public class SerializeHelperTests
{
    [Test]
    public void TestAddObj()
    {
        var structureObjsSerialization = new StructureObjsSerialization();

        structureObjsSerialization.AddObj("TestObject", new Vector3(1, 2, 3), new Vector3(0, 90, 0));

        Assert.AreEqual(1, structureObjsSerialization.structureObjData.Count, "Expected one object in the list.");
        var addedObj = structureObjsSerialization.structureObjData[0];
        Assert.AreEqual("TestObject", addedObj.name, "Object name doesn't match expected value.");
        float tolerance = 0.0001f;
        Assert.IsTrue(Vector3.Distance(new Vector3(1, 2, 3), addedObj.position.GetValue()) < tolerance, "Object position doesn't match expected value.");
        Assert.IsTrue(Vector3.Distance(new Vector3(0, 90, 0), addedObj.rotation.GetValue()) < tolerance, "Object rotation doesn't match expected value.");
    }

    [Test]
    public void TestStructureObjSerializationConstructor()
    {
        var structureObj = new StructureObjSerialization("TestStructure", new Vector3(1, 2, 3), new Vector3(0, 90, 0));

        Assert.AreEqual("TestStructure", structureObj.name);
        Assert.AreEqual(new Vector3(1, 2, 3), structureObj.position.GetValue());
        Assert.AreEqual(new Vector3(0, 90, 0), structureObj.rotation.GetValue());
    }

    [Test]
    public void TestVector3SerializationConstructorAndValueGetter()
    {
        var vectorSerialization = new Vector3Serialization(new Vector3(1, 2, 3));

        Assert.AreEqual(1, vectorSerialization.x);
        Assert.AreEqual(2, vectorSerialization.y);
        Assert.AreEqual(3, vectorSerialization.z);
        Assert.AreEqual(new Vector3(1, 2, 3), vectorSerialization.GetValue());
    }

    [Test]
    public void TestTileObjsSerializationAddTile()
    {
        var tileObjsSerialization = new TileObjsSerialization();
        tileObjsSerialization.AddTile("TestTile", new Vector3(1, 2, 3), new Vector3(0, 90, 0), true);

        Assert.AreEqual(1, tileObjsSerialization.tileData.Count);
        var addedTile = tileObjsSerialization.tileData[0];
        Assert.AreEqual("TestTile", addedTile.name);
        Assert.AreEqual(new Vector3(1, 2, 3), addedTile.position.GetValue());
        Assert.AreEqual(new Vector3(0, 90, 0), addedTile.rotation.GetValue());
        Assert.IsTrue(addedTile.isOccupied);
    }

    [Test]
    public void TestTileSerializationConstructor()
    {
        var tileSerialization = new TileSerialization("TestTile", new Vector3(1, 2, 3), new Vector3(0, 90, 0), true);

        Assert.AreEqual("TestTile", tileSerialization.name);
        Assert.AreEqual(new Vector3(1, 2, 3), tileSerialization.position.GetValue());
        Assert.AreEqual(new Vector3(0, 90, 0), tileSerialization.rotation.GetValue());
        Assert.IsTrue(tileSerialization.isOccupied);
    }
}
