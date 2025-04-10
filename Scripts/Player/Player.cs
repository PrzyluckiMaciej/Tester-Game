using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    private GameInput gameInput;
    private Transform cameraTransform;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airDrag = 1f;

    private bool isGrounded = true;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool isReadyTojump;

    [Header("HoldingObject")]
    public bool holdingObject = false;  // Czy gracz trzyma obiekt
    private PickUpObject heldObject;  // Referencja do podniesionego obiektu

    // Referencja do pozycji, w której trzymany bêdzie obiekt
    [SerializeField] public Transform holdPosition;

    public bool isHoldingWeapon = true;

    private bool isWalking;

    private Rigidbody rb;
    private Vector2 inputVector;

    public event EventHandler OnGrabbing;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        isReadyTojump = true;
    }

    private void Start() {
        gameInput = GameInput.Instance;
        cameraTransform = MainPlayerCamera.Instance.transform;
    }

    private void FixedUpdate() {
        Move();
        Jump();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void Update() {
        SpeedControl();

        isGrounded = GroundCheck.Instance.CheckGround();
        AssignGroundDrag();
        //Debug.Log(isGrounded);
    }

    // Sprawdza, czy gracz trzyma obiekt
    public bool IsHoldingObject() {
        return holdingObject;
    }

    // Podnosi obiekt i ustawia flagê
    public void HoldObject(PickUpObject obj) {
        gameInput.playerInputActions.Player.Fire.Disable();
        gameInput.playerInputActions.Player.Interact.Disable();
        holdingObject = true;
        heldObject = obj;
        OnGrabbing?.Invoke(this, EventArgs.Empty);
    }

    // Uwalnia obiekt i resetuje flagê
    public void ReleaseObject() {
        //Debug.Log("Realising object.");
        if (isHoldingWeapon) 
            gameInput.playerInputActions.Player.Fire.Enable();
        gameInput.playerInputActions.Player.Interact.Enable();
        holdingObject = false;
        heldObject = null;
    }

    // Zwraca aktualnie trzymany obiekt
    public PickUpObject GetHeldObject() {
        return heldObject;
    }

    private void SpeedControl() {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed) {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed * Time.deltaTime;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void AssignGroundDrag() {
        if (isGrounded) {
            rb.drag = groundDrag;
        }
        else {
            rb.drag = airDrag;
        }
    }

    private void Move() {
        inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = cameraTransform.forward * inputVector.y + cameraTransform.right * inputVector.x;
        moveDir.y = 0f;

        if (isGrounded) {
            rb.AddForce(moveDir * moveSpeed * 10f * Time.deltaTime, ForceMode.Force);
        }
        else {
            rb.AddForce(moveDir * moveSpeed * 10f * Time.deltaTime * airMultiplier, ForceMode.Force);
        }

        isWalking = moveDir.x != 0f && moveDir.z != 0f && isGrounded;
    }

    private void Jump() {
        bool isJumpKeyDown = gameInput.GetJumpInput();
        if (isJumpKeyDown && isReadyTojump && isGrounded) {
            isGrounded = false;
            isReadyTojump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump() {
        isReadyTojump = true;
    }
}
