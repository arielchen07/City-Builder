using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ServerInventoryData
{
    public List<ItemServer> items = new List<ItemServer>();
}

[Serializable]
public class ItemServer
{
    public string _id;
    public string userID;
    public int quantity;
    public string category;
    public string name;
    public string createdAt;
    public string updateAt;
    public int __v;

    public ItemServer(string _id, string userID, int quantity, string category, string name, string createdAt, string updateAt, int __v)
    {
        this._id = _id;
        this.userID = userID;
        this.quantity = quantity;
        this.category = category;
        this.name = name;
        this.createdAt = createdAt;
        this.updateAt = updateAt;
        this.__v = __v;
    }
}

[Serializable]
public class InitItemServer
{
    public string category;
    public string name;
    public int quantity;

    public InitItemServer(string category, string name, int quantity)
    {
        this.category = category;
        this.name = name;
        this.quantity = quantity;
    }
}