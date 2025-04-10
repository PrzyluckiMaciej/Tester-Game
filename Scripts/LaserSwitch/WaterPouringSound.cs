using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPouringSound : ContinuousSoundPlayer
{
    private ResetLiquid resetLiquid;

    private void Awake() {
        resetLiquid = GetComponent<ResetLiquid>();
    }

    private void Update() {
        if (resetLiquid.SwitchActive) {
            audioSource.enabled = true;
        }
        else {
            audioSource.enabled = false;
        }
    }
}
