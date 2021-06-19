// GENERATED AUTOMATICALLY FROM 'Assets/Input/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""8531ebf2-a98d-48c0-94d4-2d5a616ca154"",
            ""actions"": [
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""9215d100-0810-433f-ba11-ec486e141cd4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""34da0da3-ccda-4b79-8ea8-ec5c661686d7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""82d2e067-8211-40ab-841e-8899f047bb38"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1f85f918-5771-41d4-a970-8b1de2c4c6c8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""761fdf1b-894f-4d76-913e-fdc1a7a39456"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e3559a1-ee2b-48e8-be83-629f5c450269"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Drag = m_Game.FindAction("Drag", throwIfNotFound: true);
        m_Game_MousePosition = m_Game.FindAction("MousePosition", throwIfNotFound: true);
        m_Game_Restart = m_Game.FindAction("Restart", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Drag;
    private readonly InputAction m_Game_MousePosition;
    private readonly InputAction m_Game_Restart;
    public struct GameActions
    {
        private @Inputs m_Wrapper;
        public GameActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Drag => m_Wrapper.m_Game_Drag;
        public InputAction @MousePosition => m_Wrapper.m_Game_MousePosition;
        public InputAction @Restart => m_Wrapper.m_Game_Restart;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Drag.started -= m_Wrapper.m_GameActionsCallbackInterface.OnDrag;
                @Drag.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnDrag;
                @Drag.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnDrag;
                @MousePosition.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMousePosition;
                @Restart.started -= m_Wrapper.m_GameActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnRestart;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Drag.started += instance.OnDrag;
                @Drag.performed += instance.OnDrag;
                @Drag.canceled += instance.OnDrag;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    public interface IGameActions
    {
        void OnDrag(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
    }
}
