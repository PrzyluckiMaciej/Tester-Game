using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : MonoBehaviour, IActivationSignal
{
    [SerializeField] protected List<SignalTask> tasks;

    public bool SwitchActive {  get; set; }

    public static event EventHandler OnSwitchActivation;
    private bool isSoundPlayable = true;

    private void Awake() {
        SwitchActive = false;
    }

    public virtual void ActivateSwitch() {
        //Debug.Log("Laser has hit a switch.");
        SwitchActive = true;
        if (isSoundPlayable) { 
            OnSwitchActivation?.Invoke(this, EventArgs.Empty);
            isSoundPlayable = false;
        }
        if (tasks.Count > 0) {
            foreach (var task in tasks) { 
                task.Execute();
            }
        }        
    }

    public virtual void DeactivateSwitch()
    {
        //Debug.Log("Laser is not hitting a switch.");
        SwitchActive = false;
        if (!isSoundPlayable) isSoundPlayable = true;
        if (tasks.Count > 0)
        {
            foreach (var task in tasks)
            {
                task.Shutdown();
            }
        }
    }

    public bool isActive() {
        return SwitchActive;
    }
}
