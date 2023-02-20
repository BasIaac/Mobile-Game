using System;
using Attributes;
using Service;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : IInputService
{
    public PlayerControls controls { get; private set; }
    private InputAction positonAction;
    private InputAction deltaAction;
    private InputAction moveAction;

    public static Vector2 movement;
    public static Vector2 cursorPosition;
    public static Vector2 deltaPosition;
    
    [ServiceInit]
    private void InitControls()
    {
        controls = new PlayerControls();

        positonAction = controls.TouchActions.Positon;
        deltaAction = controls.TouchActions.DeltaPosition;
        moveAction = controls.Movement.Move;
        
        controls.TouchActions.Press.performed += Press;
        controls.TouchActions.Release.performed += Release;
        
        controls.Enable();
    }
    
    private void Press(InputAction.CallbackContext ctx)
    {
        cursorPosition = positonAction.ReadValue<Vector2>();
        OnPress?.Invoke(cursorPosition);
    }

    private void Release(InputAction.CallbackContext ctx)
    {
        cursorPosition = positonAction.ReadValue<Vector2>();
        OnRelease?.Invoke(cursorPosition);
    }

    [OnUpdate]
    private void UpdateInputs()
    {
        movement = moveAction.ReadValue<Vector2>();
        cursorPosition = positonAction.ReadValue<Vector2>();
        deltaPosition = deltaAction.ReadValue<Vector2>();
    }

    public static event Action<Vector2> OnPress;
    public static event Action<Vector2> OnRelease;
}
