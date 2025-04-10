using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserGun : MonoBehaviour {
    private MainPlayerCamera playerCamera;
    [SerializeField] private Transform laserOutputTransform;
    [SerializeField] private float laserRange;
    [SerializeField] private int maxReflections;
    [SerializeField] private ParticleSystem laserBeamParticles;
    [SerializeField] private ParticleSystem laserBeamHitParticles;

    private GameInput gameInput;
    private LineRenderer lineRenderer;
    private Ray targetRay;
    private LaserSwitch lastHitSwitch;
    private bool isFiring = false;

    public static LaserGun Instance { get; private set; }

    private void Awake() {
        Instance = this;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start() {
        playerCamera = MainPlayerCamera.Instance;
        gameInput = GameInput.Instance;
        DisableLaser();
    }

    private void LateUpdate() {

        if (gameInput.GetFireSinglePress()) {
            EnableLaser();
        }

        if (gameInput.GetFireDown()) {
            UpdateLaser(playerCamera.transform);
        }

        if (gameInput.GetFireUp()) {
            DisableLaser();
        }

        //Debug.Log("Laser firing: " + isFiring);
    }

    public void EnableLaser() {
        lineRenderer.enabled = true;
        isFiring = true;
        laserBeamParticles.gameObject.SetActive(true);
    }

    public bool IsFiring() {
        return isFiring;
    }

    public void UpdateLaser(Transform targetTransform) {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, laserOutputTransform.position);

        targetRay = new Ray(targetTransform.position, targetTransform.forward);

        RaycastHit mainHitInfo;

        for (int i = 0; i < maxReflections; i++) {
            if (Physics.Raycast(targetRay, out mainHitInfo)) {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mainHitInfo.point);
                targetRay = new Ray(mainHitInfo.point, 
                    Vector3.Reflect(targetRay.direction, mainHitInfo.normal));

                // Laser trafi³ na obiekt nie bêd¹cy lustrem
                if (mainHitInfo.collider.tag != "Mirror") {
                    if (!laserBeamHitParticles.gameObject.activeSelf) {
                        laserBeamHitParticles.gameObject.SetActive(true);
                    }

                    laserBeamHitParticles.transform.position = mainHitInfo.point;

                    //Laser trafi³ na prze³¹cznik
                    if (mainHitInfo.collider.tag == "Switch" 
                        && mainHitInfo.collider.GetComponent<LaserSwitch>() != null) {
                        LaserSwitch laserSwitch = mainHitInfo.collider.GetComponent<LaserSwitch>();
                        laserSwitch.ActivateSwitch();
                        lastHitSwitch = laserSwitch;
                    }
                    else {
                        DeactivateLastHitSwitch();
                    }
                    break;
                }
            }
            else {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, 
                    targetRay.origin + (targetRay.direction * laserRange));
            }
        }
    }

    public void DisableLaser() {
        lineRenderer.enabled = false;
        isFiring = false;
        laserBeamParticles.gameObject.SetActive(false);
        laserBeamHitParticles.gameObject.SetActive(false);
        DeactivateLastHitSwitch();
    }

    private void DeactivateLastHitSwitch() {
        if (lastHitSwitch != null)
            lastHitSwitch.DeactivateSwitch();
    }
}
