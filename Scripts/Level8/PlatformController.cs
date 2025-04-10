using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    [SerializeField] private float step = 0.01f;
    [SerializeField] private float stepMulitplier = 1.0f;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private int stepDirection = 1;

    private void Start() {
        ShotInLiquid.OnFillDecrease += ShotInLiquid_OnFillDecrease;
        ResetLiquid.OnFillIncrease += ResetLiquid_OnFillIncrease;
    }

    private void ShotInLiquid_OnFillDecrease(object sender, EventArgs e) {
        if (transform.localPosition.y <= minHeight || transform.localPosition.y >= maxHeight) stepDirection *= -1;
        Vector3 newPosition = transform.localPosition;
        newPosition.y -= step * stepMulitplier * stepDirection;
        transform.localPosition = newPosition;
        Debug.Log(transform.name + ": " + transform.localPosition.y);
    }

    private void ResetLiquid_OnFillIncrease(object sender, EventArgs e) {
        if (transform.localPosition.y <= minHeight || transform.localPosition.y >= maxHeight) stepDirection *= -1;
        Vector3 newPosition = transform.localPosition;
        newPosition.y += step * stepMulitplier * stepDirection;
        transform.localPosition = newPosition;
        Debug.Log(transform.name + ": " + transform.localPosition.y);

    }
}
