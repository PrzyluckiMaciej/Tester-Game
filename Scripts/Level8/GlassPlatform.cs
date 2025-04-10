using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPlatform : MonoBehaviour {
    [SerializeField] private Transform glassPlatform;
    private float angleMultiplier = 163.4f;
    void Update() {
        transform.rotation = Quaternion.Euler(glassPlatform.rotation.x * angleMultiplier, 90, 0);
    }
}
