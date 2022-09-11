using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public PlayerInput inputs;

    public UnityEvent onAttackDown;

    private void Awake()
    {
        inputs = new PlayerInput();
        inputs.Enable();
        inputs.BaseMovement.Attack.started += InvokeOnAttackDown;
    }

    private void InvokeOnAttackDown(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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
