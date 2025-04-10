using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : Interactable
{
    protected override void Interact() {
        Debug.Log("Interacted with " + transform.name);
    }
}
