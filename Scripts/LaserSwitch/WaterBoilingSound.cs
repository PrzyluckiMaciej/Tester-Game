using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBoilingSound : ContinuousSoundPlayer
{
    private ShotInLiquid shotInLiquid;

    private void Awake() {
        shotInLiquid = GetComponent<ShotInLiquid>();
    }

    private void Update() {
        if (shotInLiquid.IsBeingHeated) {
            audioSource.enabled = true;
        }
        else {
            audioSource.enabled = false;
        }
    }
}
