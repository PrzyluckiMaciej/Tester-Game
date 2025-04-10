using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainerController : ActivationSignalReceiver
{
    [Header("Animation")]
    [SerializeField] private Animator boxControllerAnimator = null;
    [SerializeField] private string boxOpen = "BoxContainerOpen";

    public static event EventHandler OnContainerOpen;

    public override void TryActivate() {
        if (CheckActivationSignals() && isStateDefault) {
            boxControllerAnimator.Play(boxOpen, 0, 0.0f);
            isStateDefault = false;
            OnContainerOpen?.Invoke(this, EventArgs.Empty);
        }
    }
}
