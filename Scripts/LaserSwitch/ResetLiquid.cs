using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLiquid : LaserSwitch {
    public float fillStep = 0.001f;
    [SerializeField] private Transform glassTransform;
    private Renderer liquidRenderer;
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private Transform liquidTransform;

    public static event EventHandler OnFillIncrease;

    public void EnableReset() {
        isEnabled = true;
    }

    public override void ActivateSwitch() {
        if (isEnabled) {
            SwitchActive = true;
            liquidRenderer = liquidTransform.GetComponent<Renderer>();
            UpdateFill();
        }        
    }

    public override void DeactivateSwitch() {
        if(isEnabled) SwitchActive = false;
    }

    public void UpdateFill() {
        float currentFill = liquidRenderer.material.GetFloat("_Fill");
        float endFill = 1.3f; // Docelowa wartoœæ fill 

        // Zmniejszamy wartoœæ fill o fillStep
        currentFill += fillStep;

        // Upewniamy siê, ¿e nie przekroczymy wartoœci koñcowej
        currentFill = Mathf.Min(currentFill, endFill);

        // Ustawiamy now¹ wartoœæ fill
        liquidRenderer.material.SetFloat("_Fill", currentFill);
        glassTransform.GetComponent<ShotInLiquid>().UpdateFillData();

        if(currentFill < 1.3f)
            OnFillIncrease?.Invoke(this, EventArgs.Empty);
    }
}
