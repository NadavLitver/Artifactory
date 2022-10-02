using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInput inputs;

    public UnityEvent onAttackDown;

    public UnityEvent onMobilityDown;

    public UnityEvent onUltimateDown;

    public UnityEvent onAttackUp;

    private void Awake()
    {
        inputs = new PlayerInput();
        inputs.Enable();
        inputs.BaseMovement.Attack.started += InvokeOnAttackDown;
        inputs.BaseMovement.Mobility.started += InvokeOnMobilityDown;
        inputs.BaseMovement.Ultimate.started += InvokeOnUltimateDown;
        inputs.BaseMovement.Attack.canceled += InvokeOnAttackUp;
    }

    private void InvokeOnAttackUp(InputAction.CallbackContext obj)
    {
        onAttackUp?.Invoke();
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


    //public void InvokeonUltimateDown()
    //{
    //    onUltimateDown.Invoke();
    //}
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
