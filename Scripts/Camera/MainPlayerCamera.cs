using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerCamera : MonoBehaviour
{
    public static MainPlayerCamera Instance { get; private set; }

    [SerializeField] private float sensitivity = 10f;

    private Vector2 look;
    private float lookRotation;
    private float xRotation, yRotation;

    public bool CameraEnabled { get; set; }

    // Odczyt ruchu mysz¹ przez event
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        Instance = this;
        // Zablokowanie i ukrycie kursora
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CameraEnabled = true;
    }

    private void Update()
    {
        if (CameraEnabled)
            Look();
    }

    private void Look()
    {
        float xMouseInput = look.x * Time.deltaTime * sensitivity;
        float yMouseInput = look.y * Time.deltaTime * sensitivity;

        yRotation += xMouseInput;
        xRotation -= yMouseInput;

        // ograniczenie spojrzenia góra - dó³
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // obrócenie kamery
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
