using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    public static PlayerInteract Instance { get; private set; }

    private Transform cameraTransform;

    [SerializeField] private float interactionDistance = 4f;

    //[SerializeField] private LayerMask layerMask;

    private GameInput gameInput;

    public event EventHandler<OnSelectedInteractableChangedEventArgs> OnSelectedInteractableChanged;

    public class OnSelectedInteractableChangedEventArgs : EventArgs {
        public Interactable interactable;
    }

    private Interactable selectedInteractable;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        gameInput = GameInput.Instance;
        cameraTransform = MainPlayerCamera.Instance.transform;
    }

    private void Update() {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hitInfo;

        // Najechanie na obiekt w warstwie Interactable
        if (Physics.Raycast(ray, out hitInfo, interactionDistance)) {

            //Debug.Log(hitInfo.collider.name);

            // Sprawdzenie czy obiekt posiada komponent Interactable
            if (hitInfo.collider.GetComponent<Interactable>() != null) {
                Interactable interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();

                if (interactable != selectedInteractable) {
                    SetSelectedInteractable(interactable);
                }

                // Wciœniêcie przycisku interakcji
                if (gameInput.GetInteractInput()) {
                    selectedInteractable.BaseInteract();
                }
            }
            else {
                SetSelectedInteractable(null);
            }
        }
        else {
            SetSelectedInteractable(null);
        }
    }

    private void SetSelectedInteractable(Interactable interactable) {

        selectedInteractable = interactable;
        OnSelectedInteractableChanged?.Invoke(this, new OnSelectedInteractableChangedEventArgs { interactable = selectedInteractable });
    }
}
