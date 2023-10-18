using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.IO;
using System.Collections;

public class SaveFileTests
{
    
    public string saveName = "SaveData_";
    [UnityEngine.Range(0, 10)]
    public int saveDataIndex = 1;
    public string dataToSave = "TestObject";

    [UnityTest]
    public void SaveDataTest()
    {
        //SaveFile.SaveData(dataToSave);

    }
}
