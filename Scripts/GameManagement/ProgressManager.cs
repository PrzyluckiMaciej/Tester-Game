using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class ProgressManager
{
    public static void SaveLevelProgress(int slot, int levelNumber) {
        if (slot < 1 || slot > 3) {
            Debug.LogWarning("Nieprawidlowy slot zapisu");
            return;
        }

        // Tworzymy dane do zapisania
        LevelData levelData = new LevelData { levelNumber = levelNumber };
        string json = JsonUtility.ToJson(levelData);

        // Œcie¿ka do zapisu
        string path = Application.persistentDataPath + $"/level{slot}.json";

        // Zapisujemy do pliku
        File.WriteAllText(path, json);
        Debug.Log($"Zapisano postêp na poziomie {levelNumber} w pliku: {path}");
    }

    public static string LoadLevelFromFile(string path) {
        // Sprawdzenie, czy plik istnieje i nie jest pusty
        if (File.Exists(path) && new FileInfo(path).Length > 0) {
            string json = File.ReadAllText(path);
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);
            return "Poziom " + levelData.levelNumber;
        }
        else {
            return "Brak zapisu";
        }
    }

    public static void DeleteSave(int saveSlot) {
        // Œcie¿ka do pliku zapisu
        string filePath = Application.persistentDataPath + $"/level{saveSlot}.json";

        // Sprawdzenie, czy plik istnieje
        if (File.Exists(filePath)) {
            // Zapisanie pustego ci¹gu w pliku (czyszczenie zawartoœci)
            File.WriteAllText(filePath, string.Empty);
            Debug.Log($"Usunieto level{saveSlot}");
        }
        else {
            Debug.Log($"level{saveSlot}nie istnieje");
        }
    }

    // Klasa pomocnicza do przechowywania danych
    [System.Serializable]
    private class LevelData {
        public int levelNumber;
    }
}
