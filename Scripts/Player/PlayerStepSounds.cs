using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepSounds : ContinuousSoundPlayer
{
    private void Update() {
        if (Player.Instance.IsWalking()) {
            audioSource.enabled = true;
        }
        else {
            audioSource.enabled = false;
        }
    }
}
