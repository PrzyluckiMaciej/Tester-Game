using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI {
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private BaseUI optionsUI;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            PauseManager.Instance.TogglePauseGame();
        });

        optionsButton.onClick.AddListener(() => {
            Hide();
            optionsUI.Show();
        } );

        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1.0f;
            GameInput.Instance.EnableFire();
            Loader.Load(Loader.Scene.MainMenuScene);
        } );
    }

    private void Start() {
        PauseManager.Instance.OnGamePaused += PauseManager_OnGamePaused;
        PauseManager.Instance.OnGameUnpaused += PauseManager_OnGameUnpaused;
        Hide();
    }

    private void PauseManager_OnGameUnpaused(object sender, System.EventArgs e) {
        PauseManager.Instance.HideAllUI();
    }

    private void PauseManager_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }
}
