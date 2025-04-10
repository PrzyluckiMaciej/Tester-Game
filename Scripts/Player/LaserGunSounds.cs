using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LaserGunSounds : ContinuousSoundPlayer
{
    private void Update() {
        if (LaserGun.Instance.IsFiring()) {
            audioSource.enabled = true;
        }
        else {
            audioSource.enabled = false;
        }
    }
}
