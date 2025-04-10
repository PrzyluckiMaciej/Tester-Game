using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangWeapon : MonoBehaviour {
    private GameInput gameInput;
    private Collider objectCollider;
    [SerializeField] private Transform laserGunModel;

    private bool isColliding = false;


    private void Start() {
        gameInput = GameInput.Instance;
        objectCollider = GetComponent<Collider>();
        objectCollider.isTrigger = true;
    }

    private void Update() {
        if (gameInput.GetHangWeapon()) {
            Hang();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (Player.Instance.isHoldingWeapon) {
            isColliding = true;
            //Debug.Log(isColliding);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (Player.Instance.isHoldingWeapon) {
            isColliding = false;
            //Debug.Log(isColliding);
        }
    }

    private void Hang() {

        if (Player.Instance.isHoldingWeapon && !isColliding) {
            transform.parent = null; // zawieszenie broni w obecnej pozycji
            gameInput.DisableFire(); // wy³¹czenie mo¿liwoœci strzelania

            // prze³¹czenie warstwy broni na "Default"
            SetLayerAllChildren(transform, 0);

            objectCollider.isTrigger = false;
            Player.Instance.isHoldingWeapon = false;
            LaserGun.Instance.EnableLaser();
        }
    }

    void SetLayerAllChildren(Transform root, int layer) {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children) {
            //Debug.Log(child.name);
            child.gameObject.layer = layer;
        }
    }
}
