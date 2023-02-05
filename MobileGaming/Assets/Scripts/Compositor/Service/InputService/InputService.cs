using System.Collections;
using System.Collections.Generic;
using Attributes;
using Service;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : IInputService
{
    public PlayerControls controls { get; private set; }
    private InputAction positonAction;
    private InputAction deltaAction;
    public Vector2 cursorPosition => positonAction.ReadValue<Vector2>();
    public Vector2 deltaPosition => deltaAction.ReadValue<Vector2>();
    
    [ServiceInit]
    private void InitControls()
    {
        controls = new PlayerControls();

        positonAction = controls.TouchActions.Positon;
        deltaAction = controls.TouchActions.DeltaPosition;
        
        controls.TouchActions.Press.performed += Press;
        controls.TouchActions.Press.canceled += Release;
        
        controls.Enable();
    }
    
    private void Press(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Pressed at : {cursorPosition}");
    }

    private void Release(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Released at {cursorPosition}");
    }
}
