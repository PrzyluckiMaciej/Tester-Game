using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotInLiquid : LaserSwitch {
    public float fillStep = 0.004f; // Krok zmiany wartoœci fill
    public Color endColor { get; set; }

    [SerializeField] private Transform liquidTransform;
    private Renderer liquidRenderer;

    [Header("Threshold")]
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    public float CurrentFillValue { get; set; }

    public bool IsBeingHeated { get; set; } = false;

    public static event EventHandler OnFillDecrease;

    private void Awake() {
        liquidRenderer = liquidTransform.GetComponent<Renderer>();
        CurrentFillValue = liquidRenderer.material.GetFloat("_Fill");
    }

    public void UpdateFillData() {
        CurrentFillValue = liquidRenderer.material.GetFloat("_Fill");
        if (CurrentFillValue > minValue && CurrentFillValue < maxValue) {
            SwitchActive = true;
            if (tasks.Count > 0) {
                foreach (var task in tasks) {
                    task.Execute();
                }
            }
        }
        else {
            SwitchActive = false;
            if (tasks.Count > 0) {
                foreach (var task in tasks) {
                    task.Shutdown();
                }
            }
        }
        //Debug.Log(SwitchActive);
    }

    // Metoda aktywuj¹ca przejœcie
    public override void ActivateSwitch() {
        if (CurrentFillValue > -1.3f) {
            IsBeingHeated = true;
            UpdateFillData();
            //Debug.Log("Laser hit a water container");

            // Uruchamiamy metodê zmieniaj¹c¹ wartoœæ Fill, kiedy strzelamy
            UpdateFill();

            // Uruchamiamy korutynê do zmiany koloru
            StartCoroutine(TransitionColor(Color.red));
        }
    }

    public override void DeactivateSwitch() {
        IsBeingHeated = false;
        Color colorFromHex;
        ColorUtility.TryParseHtmlString("#1385E6", out colorFromHex);
        StartCoroutine(TransitionColor(colorFromHex));
    }

    // Korutyna zmieniaj¹ca kolor 
    private IEnumerator TransitionColor(Color endColor) {
        //Debug.Log("Changing the color");
        Color startColor = liquidRenderer.material.GetColor("_LiquidColor");

        float elapsedTime = 0f;
        float transitionDuration = 3.0f; // Przyk³adowy czas na zmianê koloru

        while (elapsedTime < transitionDuration) {
            float t = Mathf.Pow(elapsedTime / transitionDuration, 0.7f); // Wyk³adnicze przyspieszenie
            Color currentColor = Color.Lerp(startColor, endColor, t);

            liquidRenderer.material.SetColor("_LiquidColor", currentColor);
            liquidRenderer.material.SetColor("_SurfaceColor", currentColor);
            liquidRenderer.material.SetColor("_FresnelColor", currentColor);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        liquidRenderer.material.SetColor("_LiquidColor", endColor);
        liquidRenderer.material.SetColor("_SurfaceColor", endColor);
        liquidRenderer.material.SetColor("_FresnelColor", endColor);
    }

    // Aktualizowanie wartoœci Fill na podstawie strza³ów
    private void UpdateFill() {
        //Debug.Log("Updating fill");
        float currentFill = liquidRenderer.material.GetFloat("_Fill");
        float endFill = -1.3f; // Docelowa wartoœæ fill (pusty)

        // Zmniejszamy wartoœæ fill o fillStep
        currentFill -= fillStep;

        // Upewniamy siê, ¿e nie przekroczymy wartoœci koñcowej
        currentFill = Mathf.Max(currentFill, endFill);

        // Ustawiamy now¹ wartoœæ fill
        liquidRenderer.material.SetFloat("_Fill", currentFill);
        if(currentFill > -1.3f)
            OnFillDecrease?.Invoke(this, EventArgs.Empty);
    }

    public void FillWithValue(float endValue) {
        liquidRenderer.material.SetFloat("_Fill", endValue);
    }
}
