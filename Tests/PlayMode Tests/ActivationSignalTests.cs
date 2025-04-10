using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ActivationSignalTests {

    GameObject activationSignalReceiverObject;
    GameObject pressurePlate1Object;
    GameObject pressurePlate2Object;
    GameObject pressurePlate3Object;

    ActivationSignalReceiver activationSignalReceiver;
    PressurePlate pressurePlate1;
    PressurePlate pressurePlate2;
    PressurePlate pressurePlate3;

    [SetUp]
    public void SetUp() {
        activationSignalReceiverObject = new GameObject();
        pressurePlate1Object = new GameObject();
        pressurePlate2Object = new GameObject();
        pressurePlate3Object = new GameObject();

        activationSignalReceiver = activationSignalReceiverObject.AddComponent<ActivationSignalReceiver>();
        pressurePlate1 = pressurePlate1Object.AddComponent<PressurePlate>();
        pressurePlate2 = pressurePlate2Object.AddComponent<PressurePlate>();
        pressurePlate3 = pressurePlate3Object.AddComponent<PressurePlate>();

        // Dodanie płyt naciskowych do listy sygnałów
        activationSignalReceiver.AddSignal(pressurePlate1.transform);
        activationSignalReceiver.AddSignal(pressurePlate2.transform);
        activationSignalReceiver.AddSignal(pressurePlate3.transform);

        // Przypisanie odbiornika wszystkim płytom naciskowym
        pressurePlate1.SetActivationSignalReceiver(activationSignalReceiver);
        pressurePlate2.SetActivationSignalReceiver(activationSignalReceiver);
        pressurePlate3.SetActivationSignalReceiver(activationSignalReceiver);
    }

    [UnityTest]
    public IEnumerator CheckSignalsWithAllPlatesPressed() {
        // Włączenie płyt naciskowych
        pressurePlate1.ButtonPushed();
        pressurePlate2.ButtonPushed();
        pressurePlate3.ButtonPushed();

        yield return null;

        Assert.IsTrue(activationSignalReceiver.CheckActivationSignals());
    }

    [UnityTest]
    public IEnumerator CheckSignalsWithSomePlatesPressed() {
        // Włączenie płyt naciskowych
        pressurePlate1.ButtonPushed();
        pressurePlate2.ButtonPushed();

        yield return null;

        Assert.IsFalse(activationSignalReceiver.CheckActivationSignals());
    }
}
