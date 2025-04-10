using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class GrabAndDirectWeapon : MonoBehaviour {
    private GameInput gameInput;
    private MainPlayerCamera playerCamera;

    [SerializeField] private float grabDistance = 3f;
    [SerializeField] private Transform gunPosition;
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform laserOutputTransform;

    private Vector2 look;
    private float lookRotation;
    private float xRotation, yRotation;
    private LaserSwitch lastHitSwitch;

    private Transform weaponTransform = null;

    private LineRenderer lineRenderer;
    private Ray updateRay;



    public void OnLook(InputAction.CallbackContext context) {
        look = context.ReadValue<Vector2>();
    }

    private void Start() {
        gameInput = GameInput.Instance;
        playerCamera = MainPlayerCamera.Instance;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update() {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, grabDistance)) {
            Debug.DrawLine(ray.origin, hitInfo.point);
            if (hitInfo.transform.tag == "Weapon") {
                weaponTransform = hitInfo.transform;
            }
        }
        if (weaponTransform != null && Vector3.Distance(transform.position, weaponTransform.position) <= grabDistance) {
            // Podniesienie broni
            if (gameInput.GetInteractInput() && !gameInput.GetDirectHangedWeaponDown()) {
                Grab(weaponTransform);
            }

            // Obracanie broni

            if (gameInput.GetDirectHangedWeaponSinglePress()) {
                playerCamera.CameraEnabled = false;
            }

            if (gameInput.GetDirectHangedWeaponDown()) {
                Direct(weaponTransform);
            }

            if (gameInput.GetDirectHangedWeaponUp()) {
                playerCamera.CameraEnabled = true;
                weaponTransform = null;
            }
        }
    }

    private void LateUpdate() {
        if (!Player.Instance.isHoldingWeapon)
            LaserGun.Instance.UpdateLaser(laserOutputTransform);
    }

    private void Grab(Transform weapon) {
        LaserGun.Instance.DisableLaser();
        weapon.gameObject.GetComponent<Collider>().isTrigger = true;
        SetLayerAllChildren(weapon, 7);
        Debug.Log("Set layers");
        weapon.position = gunPosition.transform.position;
        weapon.rotation = gunPosition.transform.rotation;
        Debug.Log("Moved weapon");
        weapon.parent = playerCamera.transform;
        Debug.Log("Assigned parent to " + playerCamera.transform.name);
        gameInput.EnableFire();
        Debug.Log("Enabled fire");
        Player.Instance.isHoldingWeapon = true;
        Debug.Log("Updated flag");
        Player.Instance.GetComponent<LineRenderer>().enabled = false;
    }

    private void SetLayerAllChildren(Transform root, int layer) {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children) {
            //Debug.Log(child.name);
            child.gameObject.layer = layer;
        }
    }

    private void Direct(Transform weapon) {
        float xMouseInput = look.x * Time.deltaTime * sensitivity;
        float yMouseInput = look.y * Time.deltaTime * sensitivity;

        yRotation += xMouseInput;
        xRotation -= yMouseInput;

        // ograniczenie spojrzenia góra - dó³
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // obrócenie kamery
        weapon.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
