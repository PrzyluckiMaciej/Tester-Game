using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairUI : BaseUI
{
    private void Start() {
        PauseManager.Instance.OnGamePaused += PauseManager_OnGamePaused;
        PauseManager.Instance.OnGameUnpaused += PauseManager_OnGameUnpaused;
    }

    private void PauseManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Show();
    }

    private void PauseManager_OnGamePaused(object sender, System.EventArgs e) {
        Hide();
    }
}
