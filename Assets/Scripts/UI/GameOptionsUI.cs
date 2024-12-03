using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour {

    public static GameOptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button altInteractButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI altInteractText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Transform pressToRebindTransform;

    private void Awake() {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        exitButton.onClick.AddListener(() => {
            Hide();
        });

        moveUpButton.onClick.AddListener(() => {
            Rebinding(GameInput.Binding.MoveUp);
        });

        moveDownButton.onClick.AddListener(() => {
            Rebinding(GameInput.Binding.MoveDown);
        });

        moveLeftButton.onClick.AddListener(() => {
            Rebinding(GameInput.Binding.MoveLeft);
        });

        moveRightButton.onClick.AddListener(() => {
            Rebinding(GameInput.Binding.MoveRight);
        });

        interactButton.onClick.AddListener(() => {
            Rebinding(GameInput.Binding.Interact);
        });

        altInteractButton.onClick.AddListener(() => {
            Rebinding(GameInput.Binding.InteractAlt);
        });

        pauseButton.onClick.AddListener(() => {
            Rebinding(GameInput.Binding.Pause);
        });

    }

    private void Start() {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        UpdateVisual();

        Hide();
        HideRebind();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume()*10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        altInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowRebind() {
        pressToRebindTransform.gameObject.SetActive(true);
    }
    private void HideRebind() {
        pressToRebindTransform.gameObject.SetActive(false);
    }

    private void Rebinding(GameInput.Binding binding) {
        ShowRebind();
        GameInput.Instance.Rebind(binding, () => {
            HideRebind();
            UpdateVisual();
        });
    }

}