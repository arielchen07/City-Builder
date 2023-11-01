using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.IO;
using System.Collections;

public class SaveFileTests
{
    private GameObject testGameObject;
    private SaveFile saveFile;

    [SetUp]
    public void SetUp()
    {
        testGameObject = new GameObject("TestGameObject");
        saveFile = testGameObject.AddComponent<SaveFile>();
    }

    [TearDown]
    public void TearDown()
    {
        string path = Path.Combine(Application.persistentDataPath, "unitTest1");
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        Object.DestroyImmediate(testGameObject);
    }

    [UnityTest]
    public IEnumerator TestWriteToFile()
    {
        bool success = saveFile.WriteToFile("unitTest1", "Testing WriteToFile");
        string path = Path.Combine(Application.persistentDataPath, "unitTest1");

        Assert.IsTrue(success, "Expected WriteToFile to return true");
        Assert.IsTrue(File.Exists(path), "Expected file to exist after WriteToFile.");
        Assert.AreEqual(File.ReadAllText(path), "Testing WriteToFile", "Written content doesn't match the expected content.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestSaveDataLocal()
    {
        // this test assumes that WriteToFile works as intended
        saveFile.saveName = "unitTest";
        saveFile.SaveDataLocal("Testing SaveDataLocal");
        string path = Path.Combine(Application.persistentDataPath, "unitTest1");

        Assert.IsTrue(File.Exists(path), "Expected file to exist after WriteToFile.");
        Assert.AreEqual(File.ReadAllText(path), "Testing SaveDataLocal", "Written content doesn't match the expected content.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestLoadDataLocal()
    {
        // this test assumes that WriteToFile works as intended
        saveFile.WriteToFile("unitTest1", "Testing LoadDataLocal");
        saveFile.saveName = "unitTest";
        string load = saveFile.LoadDataLocal();

        Assert.AreEqual(load, "Testing LoadDataLocal", "Loaded content doesn't match the expected content.");

        yield return null;
    }

}

