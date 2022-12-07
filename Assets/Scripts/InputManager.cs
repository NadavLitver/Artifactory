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


    public UnityEvent OnTouchDown;
    public UnityEvent OnTouchUp;

    public UnityEvent OnInteract;

    private void Awake()
    {
        inputs = new PlayerInput();
        inputs.Enable();
        inputs.BaseMovement.Attack.started += InvokeOnAttackDown;
        inputs.BaseMovement.Mobility.started += InvokeOnMobilityDown;
        inputs.BaseMovement.Ultimate.started += InvokeOnUltimateDown;
        inputs.BaseMovement.jump.started += InvokeOnJumpDown;
        inputs.BaseMovement.Touch.started += Touch_started;
        inputs.BaseMovement.Touch.canceled += Touch_canceled;
        inputs.BaseMovement.Interact.started += Interact_started;



    }

    private void Interact_started(InputAction.CallbackContext obj)
    {
        OnInteract.Invoke();
    }

    private void Touch_canceled(InputAction.CallbackContext obj)
    {
        OnTouchUp.Invoke();
    }

    private void Touch_started(InputAction.CallbackContext obj)
    {
        OnTouchDown.Invoke();
    }
    public Vector2 Touch_ScreenPos()
    {
        return inputs.BaseMovement.TouchPos.ReadValue<Vector2>();
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
