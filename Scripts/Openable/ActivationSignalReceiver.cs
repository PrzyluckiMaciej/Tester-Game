using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("PlayModeTests")]
public class ActivationSignalReceiver : MonoBehaviour
{
    [SerializeField] private List<Transform> activationSignals = new List<Transform>();

    protected bool isStateDefault = true;

    internal protected bool CheckActivationSignals() {
        foreach (var signal in activationSignals) {
            IActivationSignal activationSignal = signal.GetComponent<IActivationSignal>();
            if (activationSignal != null) {
                Debug.Log("Found signal: " + activationSignal + " which is " + activationSignal.isActive());
                if (!activationSignal.isActive()) {
                    Debug.Log("Not every signal is active");
                    return false;
                }
            }
        }
        Debug.Log("Every signal out of " + activationSignals.Count + " is active");
        return true;
    }

    public void AddSignal(Transform activationSignal) {
        activationSignals.Add(activationSignal);
    }

    public virtual void TryActivate() { }
    public virtual void TryDeactivate() { }
}
