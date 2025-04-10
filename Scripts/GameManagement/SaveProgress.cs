using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveProgress : MonoBehaviour
{
    public static SaveProgress Instance { get; private set; }
    public static int ActiveSaveSlot { get; set; }
    private int levelNumber; 

    //[SerializeField] private GameObject SavingPlace;
    private const string SAVE_SEPARATOR = "#SAVE-VALUE";

    // Inicjalizacja Singletona
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform.parent.gameObject.CompareTag("Player")) {
            levelNumber = GetLevelNumber();
            SaveLevelProgress(ActiveSaveSlot, levelNumber); // Zapisz postęp dla wybranego slotu
        }
    }

    private int GetLevelNumber() {
        string sceneName = SceneManager.GetActiveScene().name;
        if (int.TryParse(sceneName.Substring(5), out int parsedLevel)) {
            return parsedLevel+1;
        }
        return 0; // W przypadku braku poprawnego odczytu numeru poziomu
    }

    public void SaveLevelProgress(int slot, int levelNumber) {
        if (slot < 1 || slot > 3) {
            Debug.LogWarning("Nieprawidlowy slot zapisu");
            return;
        }

        // Tworzymy dane do zapisania
        LevelData levelData = new LevelData { levelNumber = levelNumber };
        string json = JsonUtility.ToJson(levelData);

        // Ścieżka do zapisu
        string path = Application.persistentDataPath + $"/level{slot}.json";

        // Zapisujemy do pliku
        File.WriteAllText(path, json);
        Debug.Log($"Zapisano post�p na poziomie {levelNumber} w pliku: {path}");
    }

    // Klasa pomocnicza do przechowywania danych
    [System.Serializable]
    private class LevelData {
        public int levelNumber;
    }
}
