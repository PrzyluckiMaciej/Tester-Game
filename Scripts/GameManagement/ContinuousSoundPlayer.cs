using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasa do obs³ugi ci¹g³ych dŸwiêków, takich jak kroki gracza
public class ContinuousSoundPlayer : MonoBehaviour
{
    protected AudioSource audioSource;
    private float volume = 0.3f;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        volume = SoundManager.Instance.GetVolume();
        audioSource.volume = volume;
        SoundManager.Instance.OnVolumeChange += SoundManager_OnVolumeChange;
    }

    private void SoundManager_OnVolumeChange(object sender, System.EventArgs e) {
        volume = SoundManager.Instance.GetVolume();
        audioSource.volume = volume;
        Debug.Log("Received volume change");
    }
}
