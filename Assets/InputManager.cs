using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInput inputs;

    private void Awake()
    {
        instance = this;
        inputs = new PlayerInput();
        inputs.Enable();
    }
    public Vector2 GetMoveVector()
    {
        return inputs.BaseMovement.move.ReadValue<Vector2>();
    }

    public bool JumpDown()
    {
        return inputs.BaseMovement.jump.triggered;
    }
}
