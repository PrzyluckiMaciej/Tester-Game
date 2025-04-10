using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPromptActivator : MonoBehaviour
{
    [SerializeField] BasePromptObject promptObject;
    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent.gameObject.CompareTag("Player") && !PromptUI.Instance.gameObject.activeSelf) {
            PromptUI.Instance.SetPromptText(promptObject.GetPromptText());
            PromptUI.Instance.Show();
            gameObject.SetActive(false);
        }
    }
}
