// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""42f58bf6-7187-4a98-a7f5-4ebc04e72b94"",
            ""actions"": [
                {
                    ""name"": ""TouchInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3c738b88-ca22-4773-80c3-57cadc6882ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""Value"",
                    ""id"": ""f293c5bb-ad1f-4d1f-8b7e-4ee2ab4955d6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d876ede1-6226-4718-91f3-e1ac3db09fb6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2805d24-6f71-45c0-824f-cc135895f736"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b93e485-1d9e-4492-94d7-02ceb79a0adc"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f33d05d9-414f-4be8-be75-5c0effcb84b3"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_TouchInput = m_Mouse.FindAction("TouchInput", throwIfNotFound: true);
        m_Mouse_TouchPosition = m_Mouse.FindAction("TouchPosition", throwIfNotFound: true);
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

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_TouchInput;
    private readonly InputAction m_Mouse_TouchPosition;
    public struct MouseActions
    {
        private @PlayerControls m_Wrapper;
        public MouseActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchInput => m_Wrapper.m_Mouse_TouchInput;
        public InputAction @TouchPosition => m_Wrapper.m_Mouse_TouchPosition;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @TouchInput.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnTouchInput;
                @TouchInput.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnTouchInput;
                @TouchInput.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnTouchInput;
                @TouchPosition.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnTouchPosition;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchInput.started += instance.OnTouchInput;
                @TouchInput.performed += instance.OnTouchInput;
                @TouchInput.canceled += instance.OnTouchInput;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface IMouseActions
    {
        void OnTouchInput(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
    }
}
