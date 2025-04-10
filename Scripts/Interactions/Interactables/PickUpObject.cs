using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PickUpObject : Interactable {

    [SerializeField] private float throwForce;
    [SerializeField] private GameObject selectedOverlay;

    private Rigidbody rb;
    private Collider objectCollider;
    private Player player;
    private GameInput gameInput;
    private Transform holdPosition;  // Pozycja przed graczem, gdzie trzymany jest obiekt
    private Collider holdPositionCollider;

    public static event EventHandler OnAnyCollision;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
        player = Player.Instance;
        gameInput = GameInput.Instance;
        holdPosition = player.holdPosition;
        holdPositionCollider = holdPosition.GetComponent<Collider>();
    }

    protected override void Interact() {
        if (!player.holdingObject) {
            PickUpObjects();
        }
        else {
            Debug.Log("Gracz ju� trzyma obiekt.");
        }
    }

    internal protected void PickUpObjects() {

        player.HoldObject(this);
        rb.isKinematic = true;  // Wy��cz fizyk�, aby kontrolowa� obiekt r�cznie
        objectCollider.enabled = false; //Wy��cz collider, aby zapobiec kolizjom
        holdPositionCollider.enabled = true;

        // Przenie� obiekt na pozycj� przed graczem
        transform.position = holdPosition.position;
        transform.parent = holdPosition;

        selectedOverlay.SetActive(false); // Wy��czenie pod�wietlania

        // Debug.Log("Podniesiono: " + gameObject.name);

    }

    public void DropObject() {
        OnObjectRelease();
        //Debug.Log("Upuszczono: " + gameObject.name);
    }

    public void ThrowObject(float throwForce) {
        OnObjectRelease();
        rb.AddForce(holdPosition.forward * throwForce, ForceMode.Impulse);
        //Debug.Log("Rzucono: " + gameObject.name);
    }

    private void Update() {
        if (player.holdingObject && player.GetHeldObject() == this) {
            if (gameInput.GetDropHeldObject()) {
                DropObject();
            }

            if (gameInput.GetThrowHeldObject()) {
                ThrowObject(throwForce);
            }
        }
    }

    private void OnObjectRelease() {
        // Uwolnij obiekt i przywr�� jego fizyczne w�a�ciwo�ci
        rb.isKinematic = false;
        objectCollider.enabled = true;
        holdPositionCollider.enabled = false;
        transform.parent = null;
        selectedOverlay.SetActive(true); // W��czenie pod�wietlania
        player.ReleaseObject();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag != "Player")
            OnAnyCollision?.Invoke(this, EventArgs.Empty);
    }
}
