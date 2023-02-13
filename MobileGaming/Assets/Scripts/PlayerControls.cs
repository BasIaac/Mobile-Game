//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""TouchActions"",
            ""id"": ""8ca1e24b-abe1-469b-b719-e82a02c78acf"",
            ""actions"": [
                {
                    ""name"": ""Positon"",
                    ""type"": ""Value"",
                    ""id"": ""146e1445-d476-4478-946b-d2a14aec088f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Press"",
                    ""type"": ""Button"",
                    ""id"": ""301dd547-98c5-4c2a-a8da-2cf664b6275e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.5)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Release"",
                    ""type"": ""Button"",
                    ""id"": ""3cc30194-f36e-4bab-a2d0-717b68850330"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=1.401298E-45,behavior=1)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DeltaPosition"",
                    ""type"": ""Value"",
                    ""id"": ""dbb31545-2560-44aa-907e-bc86d98459eb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3721c0f3-3f1c-4778-8b06-1110f5c7efaa"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Positon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c75003e-28c7-451d-b0d2-a3bfd7a14623"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ded77ec3-2c44-4b86-a4ac-b0e3740d7815"",
                    ""path"": ""<Touchscreen>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DeltaPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30b004ce-863b-41b1-86ea-208db4e48a96"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Movement"",
            ""id"": ""2722c09e-88ad-4428-a3dc-240b6875b51c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0b01ae01-6117-45f5-8d4a-f34d4feaf18a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a0ffbcd2-eef0-4d67-841b-38a7c1b80057"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TouchActions
        m_TouchActions = asset.FindActionMap("TouchActions", throwIfNotFound: true);
        m_TouchActions_Positon = m_TouchActions.FindAction("Positon", throwIfNotFound: true);
        m_TouchActions_Press = m_TouchActions.FindAction("Press", throwIfNotFound: true);
        m_TouchActions_Release = m_TouchActions.FindAction("Release", throwIfNotFound: true);
        m_TouchActions_DeltaPosition = m_TouchActions.FindAction("DeltaPosition", throwIfNotFound: true);
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // TouchActions
    private readonly InputActionMap m_TouchActions;
    private ITouchActionsActions m_TouchActionsActionsCallbackInterface;
    private readonly InputAction m_TouchActions_Positon;
    private readonly InputAction m_TouchActions_Press;
    private readonly InputAction m_TouchActions_Release;
    private readonly InputAction m_TouchActions_DeltaPosition;
    public struct TouchActionsActions
    {
        private @PlayerControls m_Wrapper;
        public TouchActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Positon => m_Wrapper.m_TouchActions_Positon;
        public InputAction @Press => m_Wrapper.m_TouchActions_Press;
        public InputAction @Release => m_Wrapper.m_TouchActions_Release;
        public InputAction @DeltaPosition => m_Wrapper.m_TouchActions_DeltaPosition;
        public InputActionMap Get() { return m_Wrapper.m_TouchActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActionsActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActionsActions instance)
        {
            if (m_Wrapper.m_TouchActionsActionsCallbackInterface != null)
            {
                @Positon.started -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnPositon;
                @Positon.performed -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnPositon;
                @Positon.canceled -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnPositon;
                @Press.started -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnPress;
                @Press.performed -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnPress;
                @Press.canceled -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnPress;
                @Release.started -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnRelease;
                @Release.performed -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnRelease;
                @Release.canceled -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnRelease;
                @DeltaPosition.started -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnDeltaPosition;
                @DeltaPosition.performed -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnDeltaPosition;
                @DeltaPosition.canceled -= m_Wrapper.m_TouchActionsActionsCallbackInterface.OnDeltaPosition;
            }
            m_Wrapper.m_TouchActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Positon.started += instance.OnPositon;
                @Positon.performed += instance.OnPositon;
                @Positon.canceled += instance.OnPositon;
                @Press.started += instance.OnPress;
                @Press.performed += instance.OnPress;
                @Press.canceled += instance.OnPress;
                @Release.started += instance.OnRelease;
                @Release.performed += instance.OnRelease;
                @Release.canceled += instance.OnRelease;
                @DeltaPosition.started += instance.OnDeltaPosition;
                @DeltaPosition.performed += instance.OnDeltaPosition;
                @DeltaPosition.canceled += instance.OnDeltaPosition;
            }
        }
    }
    public TouchActionsActions @TouchActions => new TouchActionsActions(this);

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Move;
    public struct MovementActions
    {
        private @PlayerControls m_Wrapper;
        public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Movement_Move;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    public interface ITouchActionsActions
    {
        void OnPositon(InputAction.CallbackContext context);
        void OnPress(InputAction.CallbackContext context);
        void OnRelease(InputAction.CallbackContext context);
        void OnDeltaPosition(InputAction.CallbackContext context);
    }
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
