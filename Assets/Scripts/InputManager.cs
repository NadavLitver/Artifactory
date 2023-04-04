using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class InputManager : MonoBehaviour
{
    public PlayerInput inputs;

    public UnityEvent onAttackDown;

    public UnityEvent onMobilityDown;

    public UnityEvent onUltimateDown;

    public UnityEvent onJumpDown;


    public UnityEvent<bool> OnPrimaryTouchDown;
    public UnityEvent<bool> OnPrimaryTouchUp;

    private bool primaryDown;
    private bool secondaryDown;

    public bool bothFingersDown => primaryDown && secondaryDown;

    public UnityEvent<bool> OnSecondaryTouchDown;
    public UnityEvent<bool> OnSecondaryTouchUp;

    public UnityEvent OnInteract;

    private void Awake()
    {
        inputs = new PlayerInput();
        inputs.Enable();
        inputs.BaseMovement.Attack.started += InvokeOnAttackDown;
        inputs.BaseMovement.Mobility.started += InvokeOnMobilityDown;
        inputs.BaseMovement.Ultimate.started += InvokeOnUltimateDown;
        inputs.BaseMovement.jump.started += InvokeOnJumpDown;
        inputs.BaseMovement.PrimaryTouch.started += PrimaryTouch_started;
        inputs.BaseMovement.PrimaryTouch.canceled += PrimaryTouch_canceled;
        inputs.BaseMovement.SecondaryTouch.started += SecondaryTouch_started;
        inputs.BaseMovement.SecondaryTouch.canceled += SecondaryTouch_canceled;
        inputs.BaseMovement.Interact.started += Interact_started;



    }

    private void SecondaryTouch_canceled(InputAction.CallbackContext obj)
    {
        OnSecondaryTouchUp?.Invoke(false);
        secondaryDown = false;

    }

    private void SecondaryTouch_started(InputAction.CallbackContext obj)
    {
        OnSecondaryTouchDown?.Invoke(false);
        secondaryDown = true;
    }

    private void Interact_started(InputAction.CallbackContext obj)
    {
        OnInteract.Invoke();
    }

    private void PrimaryTouch_canceled(InputAction.CallbackContext obj)
    {
        OnPrimaryTouchUp.Invoke(true);
        primaryDown = false;

    }

    private void PrimaryTouch_started(InputAction.CallbackContext obj)
    {
        OnPrimaryTouchDown.Invoke(true);
        primaryDown = true;
    }
    public Vector2 Touch_ScreenPos()
    {
        return inputs.BaseMovement.PrimaryTouchPos.ReadValue<Vector2>();
    }
    public Vector2 SecondaryTouch_ScreenPos()
    {
        return inputs.BaseMovement.SecondaryTouchPos.ReadValue<Vector2>();
    }
    private void InvokeOnJumpDown(InputAction.CallbackContext obj)
    {
        onJumpDown.Invoke();
    }

    private void InvokeOnUltimateDown(InputAction.CallbackContext obj)
    {
        onUltimateDown.Invoke();
    }

    private void InvokeOnMobilityDown(InputAction.CallbackContext obj)
    {
        onMobilityDown.Invoke();
    }

    private void InvokeOnAttackDown(InputAction.CallbackContext obj)
    {
        onAttackDown.Invoke();
    }
   
    public Vector2 GetMoveVector()
    {
        return inputs.BaseMovement.move.ReadValue<Vector2>();
    }

    public bool JumpDown()
    {
        return inputs.BaseMovement.jump.triggered;
    }
    public bool AttackDown()
    {
        return inputs.BaseMovement.Attack.triggered;
    }
}
