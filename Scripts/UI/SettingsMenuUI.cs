using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenuUI : BaseUI
{
    public static SettingsMenuUI Instance {  get; private set; }

    [SerializeField] private Button QuitButton;
    [SerializeField] private Button audioButton;

    [SerializeField] private Button ForwardButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Button JumpButton;
    [SerializeField] private Button InteractionsButton;
    [SerializeField] private Button FireButton;
    [SerializeField] private Button DropButton;
    [SerializeField] private Button ThrowButton;
    [SerializeField] private Button HangWeaponButton;
    [SerializeField] private Button DirectHangedWeaponButton;

    [SerializeField] private TextMeshProUGUI ForwardText;
    [SerializeField] private TextMeshProUGUI BackText;
    [SerializeField] private TextMeshProUGUI LeftText;
    [SerializeField] private TextMeshProUGUI RightText;
    [SerializeField] private TextMeshProUGUI JumpText;
    [SerializeField] private TextMeshProUGUI InteractionsText;
    [SerializeField] private TextMeshProUGUI FireText;
    [SerializeField] private TextMeshProUGUI DropText;
    [SerializeField] private TextMeshProUGUI ThrowText;
    [SerializeField] private TextMeshProUGUI HangWeaponText;
    [SerializeField] private TextMeshProUGUI DirectHangedWeaponText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    [SerializeField] private BaseUI previousMenu;
    [SerializeField] private BaseUI audioMenu;

    private void Awake()
    {
        Instance = this;

        QuitButton.onClick.AddListener(() =>
        {
            Hide();
            previousMenu.Show();
        });

        audioButton.onClick.AddListener(() => {
            Hide();
            audioMenu.Show();
        });

        ForwardButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Forward); });
        BackButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Back); });
        LeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Left); });
        RightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Right); });
        JumpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Jump); });
        InteractionsButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interactions); });
        FireButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Fire); });
        DropButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Drop); });
        ThrowButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Throw); });
        HangWeaponButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.HangWeapon); });
        DirectHangedWeaponButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.DirectHangedWeapon); });
    }

    private void Start()
    {
        UpdateVisual();

        HidePressToRebindKey(); 
    }
    private void UpdateVisual()
    {
        ForwardText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Forward);
        BackText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Back);
        LeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Left);
        RightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Right);
        JumpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Jump);
        InteractionsText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interactions);
        FireText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Fire);
        DropText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Drop);
        ThrowText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Throw);
        HangWeaponText.text = GameInput.Instance.GetBindingText(GameInput.Binding.HangWeapon);
        DirectHangedWeaponText.text = GameInput.Instance.GetBindingText(GameInput.Binding.DirectHangedWeapon);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
