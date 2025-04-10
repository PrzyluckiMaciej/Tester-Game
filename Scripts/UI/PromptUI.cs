using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PromptUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TextMeshProUGUI continueText;

    public static PromptUI Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        continueText.text = "Wciœnij " + GameInput.Instance.GetBindingText(GameInput.Binding.Interactions) + ", aby kontynuowaæ.";
        Hide();
    }

    private void Update() {
        if (GameInput.Instance.GetInteractInput() || GameInput.Instance.GetPauseInput()) {
            Hide();
        }
    }

    public void SetPromptText(string text) {
        promptText.text = text;
    }
}
