using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private bool isInMainMenu = false;

    private float volume = 0.3f;

    public event EventHandler OnVolumeChange;

    private void Awake() {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 0.3f);
    }

    private void Start() {
        if (!isInMainMenu) {
            GroundCheck.Instance.OnLanding += GroundCheck_OnLanding;
            Player.Instance.OnGrabbing += Player_OnGrabbing;
            PickUpObject.OnAnyCollision += PickUpObject_OnAnyCollision;
            DoorControls.OnDoorMove += DoorControls_OnDoorMove;
            TriggerDoorController.OnDoorMove += TriggerDoorController_OnDoorMove;
            BoxContainerController.OnContainerOpen += BoxContainerController_OnContainerOpen;
            PressurePlate.OnPlatePress += PressurePlate_OnPlatePress;
            LaserSwitch.OnSwitchActivation += LaserSwitch_OnSwitchActivation;
        }        
    }

    private void LaserSwitch_OnSwitchActivation(object sender, System.EventArgs e) {
        LaserSwitch laserSwitch = sender as LaserSwitch;
        PlaySound(audioClipRefsSO.switchActivation, laserSwitch.transform.position);
    }

    private void PressurePlate_OnPlatePress(object sender, System.EventArgs e) {
        PressurePlate pressurePlate = sender as PressurePlate;
        PlaySound(audioClipRefsSO.platePress, pressurePlate.transform.position);
    }

    private void BoxContainerController_OnContainerOpen(object sender, System.EventArgs e) {
        BoxContainerController boxContainerController = sender as BoxContainerController;
        PlaySound(audioClipRefsSO.objectMove, boxContainerController.transform.position);
    }

    private void TriggerDoorController_OnDoorMove(object sender, System.EventArgs e) {
        TriggerDoorController triggerDoorController = sender as TriggerDoorController;
        PlaySound(audioClipRefsSO.objectMove, triggerDoorController.transform.position);
    }

    private void DoorControls_OnDoorMove(object sender, System.EventArgs e) {
        DoorControls doorControls = sender as DoorControls;
        PlaySound(audioClipRefsSO.objectMove, doorControls.transform.position);
    }

    private void PickUpObject_OnAnyCollision(object sender, System.EventArgs e) {
        PickUpObject pickUpObject = sender as PickUpObject;
        PlaySound(audioClipRefsSO.drop, pickUpObject.transform.position);
    }

    private void Player_OnGrabbing(object sender, System.EventArgs e) {
        Player player = sender as Player;
        PlaySound(audioClipRefsSO.grab, player.transform.position);
    }

    private void GroundCheck_OnLanding(object sender, System.EventArgs e) {
        GroundCheck groundCheck = sender as GroundCheck;
        PlaySound(audioClipRefsSO.playerLand, groundCheck.transform.position);
    }

    public void ChangeVolume(float volume) {
        this.volume = volume;
        Debug.Log("Sound volume in " + this.name + ": " + this.volume);

        // Zapis ustawieñ
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
        OnVolumeChange?.Invoke(this, EventArgs.Empty);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public float GetVolume() {
        return volume;
    }
}
