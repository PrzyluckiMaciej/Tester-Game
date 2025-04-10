using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAlien : MonoBehaviour {
    [SerializeField] private GameObject alien;
    [SerializeField] private GameObject blackScreen;

    private bool isActive = false;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent.gameObject.CompareTag("Player")) {
            alien.SetActive(true);
            isActive = true;
            GameInput.Instance.DisableMove();
            GameInput.Instance.DisablePause();
        }
    }

    private void Update() {
        if (isActive && GameInput.Instance.GetInteractInput()) {
            Debug.Log("Black");
            blackScreen.SetActive(true);
            StartCoroutine(switchScene());
        }
    }

    private IEnumerator switchScene() {
        yield return new WaitForSeconds(5);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameInput.Instance.EnableMove();
        GameInput.Instance.EnablePause();
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
