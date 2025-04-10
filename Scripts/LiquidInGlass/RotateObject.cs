using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100f; // Prêdkoœæ obrotu

    void Update()
    {
        // Sprawdzamy, czy prawy przycisk myszy jest wciœniêty
        if (Input.GetMouseButton(1))
        {
            // Pobieramy ruch myszy w poziomie (oœ X) i pionie (oœ Y)
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Obracamy obiekt wokó³ osi Y i X
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
