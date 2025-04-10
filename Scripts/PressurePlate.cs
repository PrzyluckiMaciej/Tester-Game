using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("PlayModeTests")]
public class PressurePlate : MonoBehaviour, IActivationSignal {
    private Collider boxCollider;
    private bool plateActive = false;
    [SerializeField] private ActivationSignalReceiver activationSignalReceiver;
    [SerializeField] private Animator animator;

    public static event EventHandler OnPlatePress;

    private void OnTriggerEnter(Collider other) {
        // Wykrycie kolizji - przycisk zostaje wciœniêty
        if (CheckCollider(other)){
            boxCollider = other.GetComponent<Collider>();
            Debug.Log("Pressure plate is pressed");
            ButtonPushed();
        }
    }

    private void Update() {
        // Obs³uga zwolnienia przycisku przy podniesieniu z niego skrzynki
        if (boxCollider!=null)
        {
            if (!boxCollider.enabled) {
                Debug.Log("Pressure plate is no longer pressed (no collider)");
                ButtonReleased();
                boxCollider = null;
            }
        }        
    }

    private void OnTriggerExit(Collider other) {
        // Wykrycie kolizji - przycisk zostaje zwolniony
        if (CheckCollider(other)) {
            Debug.Log("Pressure plate is no longer pressed");
            ButtonReleased();
        }
    }

    private bool CheckCollider(Collider other) {
        return other.gameObject.tag == "Player" || other.gameObject.tag == "Holdable";
    }

    internal protected void ButtonPushed() {
        plateActive = true;
        OnPlatePress?.Invoke(this, EventArgs.Empty);
        activationSignalReceiver.TryActivate();
        PlayAnimation("PlatePress");
    }
    internal protected void ButtonReleased() {
        plateActive = false;
        OnPlatePress?.Invoke(this, EventArgs.Empty);
        activationSignalReceiver.TryDeactivate();
        PlayAnimation("PlateRelease");
    }

    private void PlayAnimation(string option) {
        if(animator != null) animator.Play(option);
    }

    public bool isActive() {
        return plateActive;
    }

    public void SetActivationSignalReceiver(ActivationSignalReceiver activationSignalReceiver) {
        this.activationSignalReceiver = activationSignalReceiver;
    }
}
