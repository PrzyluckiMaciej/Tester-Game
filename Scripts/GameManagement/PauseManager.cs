using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private bool isGamePaused = false;

    [SerializeField] private BaseUI pauseUI;
    [SerializeField] private BaseUI audioUI;
    [SerializeField] private BaseUI controlsUI;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (GameInput.Instance.GetPauseInput()) { 
            TogglePauseGame();
        }
    }

    public void TogglePauseGame() { 
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameInput.Instance.DisableFire();
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameInput.Instance.EnableFire();
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public void HideAllUI() {
        pauseUI.Hide();
        audioUI.Hide();
        controlsUI.Hide();
    }
}
