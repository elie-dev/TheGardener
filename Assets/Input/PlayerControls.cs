// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

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
            ""name"": ""gamepade"",
            ""id"": ""0adfac3b-c2de-4e7e-ad9b-dcdf71e7a286"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""ca930790-ca62-4145-ab26-3e37d6a634e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""88f15f2a-8000-4512-b389-6c43d8161160"",
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
        // gamepade
        m_gamepade = asset.FindActionMap("gamepade", throwIfNotFound: true);
        m_gamepade_Move = m_gamepade.FindAction("Move", throwIfNotFound: true);
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

    // gamepade
    private readonly InputActionMap m_gamepade;
    private IGamepadeActions m_GamepadeActionsCallbackInterface;
    private readonly InputAction m_gamepade_Move;
    public struct GamepadeActions
    {
        private @PlayerControls m_Wrapper;
        public GamepadeActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_gamepade_Move;
        public InputActionMap Get() { return m_Wrapper.m_gamepade; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamepadeActions set) { return set.Get(); }
        public void SetCallbacks(IGamepadeActions instance)
        {
            if (m_Wrapper.m_GamepadeActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GamepadeActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GamepadeActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GamepadeActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_GamepadeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public GamepadeActions @gamepade => new GamepadeActions(this);
    public interface IGamepadeActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
