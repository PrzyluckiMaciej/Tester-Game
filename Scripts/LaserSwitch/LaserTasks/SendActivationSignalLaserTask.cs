using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendActivationSignalLaserTask : LaserTask {
    [SerializeField] private ActivationSignalReceiver activationSignalReceiver;

    public override void Execute() {
        activationSignalReceiver.TryActivate();
    }
    public override void Shutdown()
    {
        activationSignalReceiver.TryDeactivate();
    }
}
