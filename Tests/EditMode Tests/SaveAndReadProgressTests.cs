using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SaveAndReadProgressTests
{
    [Test]
    public void SaveAndLoadLevelProgress()
    {
        ProgressManager.SaveLevelProgress(1, 2);
        Assert.AreEqual("Poziom 2", ProgressManager.LoadLevelFromFile(Application.persistentDataPath + "/level1.json"));
    }

    [Test]
    public void LoadLevelProgressWhenFileDoesNotExist() {
        Assert.AreEqual("Brak zapisu", ProgressManager.LoadLevelFromFile(Application.persistentDataPath + "/level4.json"));
    }
}
