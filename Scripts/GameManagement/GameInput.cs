using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBinding";

    public static GameInput Instance { get; private set; }

    public enum Binding
    {
        Forward,
        Back,
        Left,
        Right,
        Jump,
        Interactions,
        Fire,
        Drop,
        Throw,
        HangWeapon,
        DirectHangedWeapon,
    }

    public PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        // Za³adowuje przypisania klawiszy jeœli istniej¹
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputActions.Player.Enable();
    }

    //Pobranie wektora ruchu gracza z kontrolera
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    // Funkcje wykrywaj¹ce wciœniêcie, przytrzymanie lub puszczenie klawisza
    public bool GetJumpInput()
    {
        return playerInputActions.Player.Jump.IsPressed();
    }

    public bool GetInteractInput()
    {
        return playerInputActions.Player.Interact.WasPerformedThisFrame();
    }

    public bool GetFireSinglePress()
    {
        return playerInputActions.Player.Fire.WasPerformedThisFrame();
    }

    public bool GetFireDown()
    {
        return playerInputActions.Player.Fire.IsPressed();
    }

    public bool GetFireUp()
    {
        return playerInputActions.Player.Fire.WasReleasedThisFrame();
    }

    public void DisableFire()
    {
        playerInputActions.Player.Fire.Disable();
    }
    public void EnableFire()
    {
        playerInputActions.Player.Fire.Enable();
    }

    public void DisableMove() {
        playerInputActions.Player.Move.Disable();
    }

    public void EnableMove() {
        playerInputActions.Player.Move.Enable();
    }

    public void DisablePause() {
        playerInputActions.Player.Pause.Disable();
    }

    public void EnablePause() {
        playerInputActions.Player.Pause.Enable();
    }

    public bool GetDropHeldObject()
    {
        return playerInputActions.Player.Drop.WasPerformedThisFrame();
    }

    public bool GetThrowHeldObject()
    {
        return playerInputActions.Player.Throw.WasPerformedThisFrame();
    }

    public bool GetHangWeapon()
    {
        return playerInputActions.Player.HangWeapon.WasPerformedThisFrame();
    }

    public bool GetDirectHangedWeaponSinglePress()
    {
        return playerInputActions.Player.DirectHangedWeapon.WasPerformedThisFrame();
    }

    public bool GetDirectHangedWeaponDown()
    {
        return playerInputActions.Player.DirectHangedWeapon.IsPressed();
    }

    public bool GetDirectHangedWeaponUp()
    {
        return playerInputActions.Player.DirectHangedWeapon.WasReleasedThisFrame();
    }

    public bool GetPauseInput() {
        return playerInputActions.Player.Pause.WasPerformedThisFrame();
    }

    
    // Funkcja zwracaj¹ca przypisania klawiszy jako tekst
    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Forward:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Back:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Jump:
                return playerInputActions.Player.Jump.bindings[0].ToDisplayString();
            case Binding.Interactions:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.Fire:
                return playerInputActions.Player.Fire.bindings[0].ToDisplayString();
            case Binding.Drop:
                return playerInputActions.Player.Drop.bindings[0].ToDisplayString();
            case Binding.Throw:
                return playerInputActions.Player.Throw.bindings[0].ToDisplayString();
            case Binding.HangWeapon:
                return playerInputActions.Player.HangWeapon.bindings[0].ToDisplayString();
            case Binding.DirectHangedWeapon:
                return playerInputActions.Player.DirectHangedWeapon.bindings[0].ToDisplayString();
        }
    }

    // Funkcja do zmiany przypisania klawisza
    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Forward:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1; 
                break;
            case Binding.Back:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Jump:
                inputAction = playerInputActions.Player.Jump;
                bindingIndex = 0;
                break;
            case Binding.Interactions:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Fire:
                inputAction = playerInputActions.Player.Fire;
                bindingIndex = 0;
                break;
            case Binding.Drop:
                inputAction = playerInputActions.Player.Drop;
                bindingIndex = 0;
                break;
            case Binding.Throw:
                inputAction = playerInputActions.Player.Throw;
                bindingIndex = 0;
                break;
            case Binding.HangWeapon:
                inputAction = playerInputActions.Player.HangWeapon;
                bindingIndex = 0;
                break;
            case Binding.DirectHangedWeapon:
                inputAction = playerInputActions.Player.DirectHangedWeapon;
                bindingIndex = 0;
                break;

        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                callback.Dispose();
                playerInputActions.Player.Enable();
                onActionRebound();


                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }
}
