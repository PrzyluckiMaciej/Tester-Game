using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CapsuleDispenser : MonoBehaviour, IActivationSignal {
    [Header("Tasks")]
    [SerializeField] private int currentPhase = 1;
    [SerializeField] private List<SignalTask> tasksPhase1;
    [SerializeField] private List<SignalTask> tasksPhase2;

    public bool DispenserActive { get; set; }

    private void Awake() {
        DispenserActive = false;
    }

    public bool isActive() {
        return DispenserActive;
    }

    private void OnCollisionEnter(Collision collision) {
        // Sprawdü, czy obiekt ma tag Holdable
        if (collision.gameObject.CompareTag("Holdable")) {
            Debug.Log("Holdable object hit the CapsuleDispenser!");
            if (currentPhase == 1) {
                foreach (var task in tasksPhase1) {
                    task.Execute();
                }
                collision.gameObject.SetActive(false);
                currentPhase++;
            }
            else if (currentPhase == 2) {
                DispenserActive = true;
                foreach (var task in tasksPhase2) {
                    task.Execute();
                }
                collision.gameObject.SetActive(false);
                currentPhase++;
            }
        }
    }
}
