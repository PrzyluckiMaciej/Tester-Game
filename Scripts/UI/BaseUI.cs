using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public void Show() {
        // Poka¿ interfejs
        gameObject.SetActive(true);
    }

    public void Hide() {
        // Ukryj interfejs
        gameObject.SetActive(false);
    }
}
