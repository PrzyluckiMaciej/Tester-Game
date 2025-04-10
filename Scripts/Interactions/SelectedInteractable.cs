using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedInteractable : MonoBehaviour {
    [SerializeField] private Interactable baseInteractable;
    [SerializeField] private GameObject gameObjectVisual;

    private void Start() {
        PlayerInteract.Instance.OnSelectedInteractableChanged += PlayerInteract_OnSelectedInteractableChanged;
    }

    private void PlayerInteract_OnSelectedInteractableChanged(object sender, PlayerInteract.OnSelectedInteractableChangedEventArgs e) {
        if (e.interactable == baseInteractable) {
            // Pod�wietlenie obiektu
            Show();
        }
        else {
            // Wy��czenie pod�wietlenia
            Hide();
        }
    }

    private void Show() {
        gameObjectVisual.SetActive(true);
    }

    private void Hide() { 
        gameObjectVisual.SetActive(false); 
    }
}
