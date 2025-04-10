using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    public event EventHandler OnLanding;

    public static GroundCheck Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public bool CheckGround() {
        // sprawdzenie czy gracz jest na ziemi
        return Physics.CheckSphere(transform.position, groundDistance, groundLayer);
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) {
            OnLanding?.Invoke(this, EventArgs.Empty);
            Debug.Log("Player has landed");
        }
    }
}
