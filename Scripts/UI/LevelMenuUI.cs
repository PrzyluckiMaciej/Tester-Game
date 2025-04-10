using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class LevelMenuUI : BaseUI {
    [SerializeField] private TextMeshProUGUI Poziom1Text;
    [SerializeField] private TextMeshProUGUI Poziom2Text;
    [SerializeField] private TextMeshProUGUI Poziom3Text;

    [SerializeField] private Button DeleteSave1Button;
    [SerializeField] private Button DeleteSave2Button;
    [SerializeField] private Button DeleteSave3Button;
    [SerializeField] private Button QuitButton;
    [SerializeField] private GameObject MainMenuUI;

    private void Awake() {
        DeleteSave1Button.onClick.AddListener(() => DeleteSave(1));
        DeleteSave2Button.onClick.AddListener(() => DeleteSave(2));
        DeleteSave3Button.onClick.AddListener(() => DeleteSave(3));
        QuitButton.onClick.AddListener(OnQuitButtonClicked);

        UpdateSaveSlotsUI();
    }


    private void Start() {
        LoadSavedLevels();

    }

    private void OnQuitButtonClicked() {
        Hide(); // Ukryj MainMenuUI
        MainMenuUI.GetComponent<BaseUI>().Show(); // Pokaż MainMenuUI
    }

    private void LoadSavedLevels() {
        // Ścieżki do plików zapisu
        string path1 = Application.persistentDataPath + "/level1.json";
        string path2 = Application.persistentDataPath + "/level2.json";
        string path3 = Application.persistentDataPath + "/level3.json";

        // Wczytanie poziomu z każdego pliku
        Poziom1Text.text = LoadLevelFromFile(path1);
        Poziom2Text.text = LoadLevelFromFile(path2);
        Poziom3Text.text = LoadLevelFromFile(path3);
    }

    private string LoadLevelFromFile(string path) {
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

    // Metoda do załadowania sceny na podstawie numeru poziomu
    public void LoadLevelFromSave(int saveSlot) {
        // Ustaw aktywny slot, aby gra wiedziała, gdzie zapisać postęp
        SaveProgress.ActiveSaveSlot = saveSlot;

        string path = Application.persistentDataPath + $"/level{saveSlot}.json";

        // Sprawdzenie, czy plik istnieje i nie jest pusty
        if (File.Exists(path) && new FileInfo(path).Length > 0) {
            string json = File.ReadAllText(path);
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            // Załaduj scenę na podstawie numeru poziomu
            string sceneName = "level" + levelData.levelNumber;
            SceneManager.LoadScene(sceneName);
        }
        else {
            Debug.LogWarning("Brak zapisu w slocie: " + saveSlot + ". Rozpoczynamy now� gr�.");

            // Jeśli zapis nie istnieje, rozpoczynamy od poziomu 1 i zapisujemy nowy post�p
            int startingLevel = 1;
            //SaveProgress.Instance.SaveLevelProgress(saveSlot, startingLevel);
            SceneManager.LoadScene("level" + startingLevel);
        }
    }


    private void DeleteSave(int saveSlot) {
        // Ścieżka do pliku zapisu
        string filePath = Application.persistentDataPath + $"/level{saveSlot}.json";

        // Sprawdzenie, czy plik istnieje
        if (File.Exists(filePath)) {
            // Zapisanie pustego ciągu w pliku (czyszczenie zawarto�ci)
            File.WriteAllText(filePath, string.Empty);
            Debug.Log($"Usunieto level{saveSlot}");

            UpdateSaveSlotsUI();
        }
        else {
            Debug.Log($"level{saveSlot}nie istnieje");
        }
    }

    private void UpdateSaveSlotsUI() {
        // Sprawdzenie, czy plik zapisu istnieje i jest niepusty
        UpdateSaveSlotUI(1, Poziom1Text);
        UpdateSaveSlotUI(2, Poziom2Text);
        UpdateSaveSlotUI(3, Poziom3Text);
    }

    private void UpdateSaveSlotUI(int saveSlot, TextMeshProUGUI saveText) {
        // Ścieżka do pliku zapisu
        string filePath = Application.persistentDataPath + $"/level{saveSlot}.json";

        if (File.Exists(filePath) && new FileInfo(filePath).Length > 0) {
            // Je�li plik istnieje i ma zawarto��, wy�wietl numer poziomu w odpowiednim polu tekstowym
            string json = File.ReadAllText(filePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);
            saveText.text = $"Poziom {levelData.levelNumber}";
        }
        else {
            // Jeśli plik jest pusty, pokaż informację o braku zapisu
            saveText.text = "Brak zapisu";
        }


    }

    [System.Serializable]
    private class LevelData {
        public int levelNumber;
    }
}