using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private BaseUI audioMenu;

    [SerializeField] private BaseUI levelMenuUI;// Obiekt LevelMenuUI na Canvasie

    private void Awake() {
        newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnNewGameButtonClicked() {
        if (IsAnySaveSlotEmpty()) {
            int emptySlot = GetFirstEmptySlot();
            SaveProgress.ActiveSaveSlot = emptySlot;  // Ustaw aktywny slot
            Loader.Load(Loader.Scene.Level1);
        }
        else {
            Hide();
            levelMenuUI.Show();
        }
    }

    private void OnContinueButtonClicked() {
        Hide();
        levelMenuUI.Show();
    }

    private void OnSettingsButtonClicked() {
        Hide();
        audioMenu.Show();
    }

    private void OnQuitButtonClicked() {
        Application.Quit();
    }

    private bool IsAnySaveSlotEmpty() {
        // �cie�ki do plik�w zapisu
        string[] paths = {
            Application.persistentDataPath + "/level1.json",
            Application.persistentDataPath + "/level2.json",
            Application.persistentDataPath + "/level3.json"
        };

        // Sprawdzenie, czy kt�rykolwiek plik jest pusty lub nie istnieje
        foreach (string path in paths) {
            if (!File.Exists(path) || new FileInfo(path).Length == 0) {
                return true;
            }
        }

        return false; // Je�li wszystkie pliki s� zape�nione
    }
    private int GetFirstEmptySlot() {
        string[] paths = {
        Application.persistentDataPath + "/level1.json",
        Application.persistentDataPath + "/level2.json",
        Application.persistentDataPath + "/level3.json"
    };

        for (int i = 0; i < paths.Length; i++) {
            if (!File.Exists(paths[i]) || new FileInfo(paths[i]).Length == 0) {
                return i + 1; // Zwraca numer pierwszego pustego slota (1, 2 lub 3)
            }
        }

        return -1; // Je�li wszystkie sloty s� pe�ne, zwraca -1
    }
}
