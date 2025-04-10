using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAndPrepareWaterContainerTask : SignalTask
{
    [SerializeField] private ShotInLiquid shotInLiquid;
    [SerializeField] private ResetLiquid resetLiquid;

    private void Awake() {
        resetLiquid.enabled = false;
    }
    public override void Execute() {
        shotInLiquid.CurrentFillValue = 1.3f; // Wartoœæ dla pe³nego kontenera
        shotInLiquid.FillWithValue(1.3f);
        resetLiquid.EnableReset();
    }
}
