using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingAction;

    private PlayerInputAction playerInputAction;

    public enum Binding {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlt,
        Pause
    }

    private void Awake() {
        Instance = this;
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.AlternateInteract.performed += AlternateInteract_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        playerInputAction.Player.Interact.performed -= Interact_performed;
        playerInputAction.Player.AlternateInteract.performed -= AlternateInteract_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;

        playerInputAction.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void AlternateInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        //Vector2 inputVector = playerInputAction.Player.Movement.ReadValue<Vector2>();
        Vector2 inputVector = UDPInputHandler.Instance.GetMovementVector();

        inputVector = inputVector.normalized;

        return inputVector;
    } 

    public string GetBindingText(Binding binding) {
        switch (binding) {
            default:
            case Binding.MoveUp:
                return playerInputAction.Player.Movement.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return playerInputAction.Player.Movement.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerInputAction.Player.Movement.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerInputAction.Player.Movement.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlt:
                return playerInputAction.Player.AlternateInteract.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void Rebind(Binding binding, Action onActionRebound) {
        playerInputAction.Player.Disable();

        InputAction inputAction;
        int index;

        switch(binding) {
            default:
            case Binding.MoveUp:
                inputAction = playerInputAction.Player.Movement;
                index = 1; 
                break;
            case Binding.MoveDown:
                inputAction = playerInputAction.Player.Movement;
                index = 2;
                break;
            case Binding.MoveLeft:
                inputAction = playerInputAction.Player.Movement;
                index = 3;
                break;
            case Binding.MoveRight:
                inputAction = playerInputAction.Player.Movement;
                index = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputAction.Player.Interact;
                index = 0;
                break;
            case Binding.InteractAlt:
                inputAction = playerInputAction.Player.AlternateInteract;
                index = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputAction.Player.Pause;
                index = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(index)
            .OnComplete(callback => {
                callback.Dispose();
                playerInputAction.Player.Enable();
                onActionRebound();

                OnBindingAction?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }

}