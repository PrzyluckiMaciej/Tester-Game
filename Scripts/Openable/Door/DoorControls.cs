using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : ActivationSignalReceiver
{
    [Header("Animation")]
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private string doorOpen = "DoorOpen";
    [SerializeField] private string doorClose = "DoorClose";
    [SerializeField] private bool isCloseable = true;

    public static event EventHandler OnDoorMove;

    public override void TryActivate() {
        if (CheckActivationSignals() && isStateDefault) {
            myDoor.Play(doorOpen, 0, 0.0f);
            isStateDefault = false;
            OnDoorMove?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void TryDeactivate() {
        if (!isStateDefault && isCloseable) {
            myDoor.Play(doorClose, 0, 0.0f);
            isStateDefault = true;
            OnDoorMove?.Invoke(this, EventArgs.Empty);
        }
    }
}
