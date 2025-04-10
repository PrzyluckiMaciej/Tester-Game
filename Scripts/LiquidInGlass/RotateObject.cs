using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100f; // Pr�dko�� obrotu

    void Update()
    {
        // Sprawdzamy, czy prawy przycisk myszy jest wci�ni�ty
        if (Input.GetMouseButton(1))
        {
            // Pobieramy ruch myszy w poziomie (o� X) i pionie (o� Y)
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Obracamy obiekt wok� osi Y i X
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
