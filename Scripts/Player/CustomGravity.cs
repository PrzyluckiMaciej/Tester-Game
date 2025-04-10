using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    [SerializeField] private float gravityMultiplier = 1f;

    [SerializeField] private static float globalGravity = -9.81f;

    private Rigidbody rb;

    private void OnEnable() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void FixedUpdate() {
        Vector3 gravity = globalGravity * gravityMultiplier * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
