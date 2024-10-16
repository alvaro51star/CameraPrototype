//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/PlayerInput.inputactions
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

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""CameraPlayer"",
            ""id"": ""e3682ddf-876a-41e5-ac06-5f9f7c595080"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""4c5fd5f2-a348-4063-9cc6-2d3f9fa9686a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""f4436446-0a72-4f94-ac2e-37288116d347"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TakePhoto"",
                    ""type"": ""Button"",
                    ""id"": ""c15d0c0a-09ef-48c3-b77c-413ef9403ab4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c7335fd6-c28d-4e85-8fff-d5f596a584f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""3115673f-45b0-4dc4-9edf-7273263946d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotatePlayer"",
                    ""type"": ""Value"",
                    ""id"": ""6a264b34-ac9a-4d59-bb42-19f6edc929dc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""38225da0-0ea7-49ec-b87f-6ebb363d6a2c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FocusCamera"",
                    ""type"": ""Button"",
                    ""id"": ""b6f88360-9dfb-4d42-9f77-649ffe320869"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkipText"",
                    ""type"": ""Button"",
                    ""id"": ""56a453a3-5643-4dab-b13a-99e7ba5bdd8a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CameraRollCheat"",
                    ""type"": ""Button"",
                    ""id"": ""5b5555ed-f4ff-49d3-a235-db9bf3d591b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeLense"",
                    ""type"": ""PassThrough"",
                    ""id"": ""650d52a9-3728-4776-b56b-fc223289e58b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""16c92659-549d-4fa1-90f2-67379365a0da"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8e648b3f-7390-4dc2-8637-1677f1c39a82"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3408ec2d-80b5-493e-ba3a-5a88eea1ea84"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3a3596cc-f813-43f7-9162-d0d162413e14"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3c0c0504-4484-4bdf-b842-6ca7c3fb3d71"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c8567538-e924-490f-8277-df99eecff768"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9564b24b-8db3-42bd-a4d9-3f90980781dc"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82aba0f2-4783-4851-adcb-f344a1709cd2"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34656689-a3ba-465f-8516-782790c8d74e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TakePhoto"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef293b14-5e1b-4a5f-adbc-cb0f94582196"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""TakePhoto"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46039bbd-a374-49d9-9aad-5b7c9e985c63"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccb78e83-d661-4ca2-9665-7a6d62a7c4ba"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0f0d55c-5755-4c85-b62d-8afb768867f2"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89e5c870-7260-4d84-a09c-789766e9472d"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c52000f-7df7-4771-97d1-f414d1b58af0"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2338b88-a440-4cdb-9c2d-2e4f42887fe8"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RotatePlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""136ef4a3-1993-49ad-aba5-c6bf55d854e0"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""RotatePlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9305da4-a9c0-4e88-8143-92a114b1ff18"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d98ed9f-2f79-43f4-afab-80d4fdaf3de9"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""PauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2898c6ac-9774-4708-8405-a8837106763b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""FocusCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b427f887-2eb0-45c7-ba7c-b10f3019f67b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""FocusCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80e321c1-d845-4e72-bfd7-b1f85f79ac6b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SkipText"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abdb8aab-b7ae-4435-8d14-9c6a6fd941fe"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""SkipText"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24b1122e-0928-4bcb-8298-8c1dc38c3a78"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CameraRollCheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7a26106-2415-41b8-8733-f2c4a3458471"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeLense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CameraPlayer
        m_CameraPlayer = asset.FindActionMap("CameraPlayer", throwIfNotFound: true);
        m_CameraPlayer_Movement = m_CameraPlayer.FindAction("Movement", throwIfNotFound: true);
        m_CameraPlayer_Run = m_CameraPlayer.FindAction("Run", throwIfNotFound: true);
        m_CameraPlayer_TakePhoto = m_CameraPlayer.FindAction("TakePhoto", throwIfNotFound: true);
        m_CameraPlayer_Interact = m_CameraPlayer.FindAction("Interact", throwIfNotFound: true);
        m_CameraPlayer_Inventory = m_CameraPlayer.FindAction("Inventory", throwIfNotFound: true);
        m_CameraPlayer_RotatePlayer = m_CameraPlayer.FindAction("RotatePlayer", throwIfNotFound: true);
        m_CameraPlayer_PauseMenu = m_CameraPlayer.FindAction("PauseMenu", throwIfNotFound: true);
        m_CameraPlayer_FocusCamera = m_CameraPlayer.FindAction("FocusCamera", throwIfNotFound: true);
        m_CameraPlayer_SkipText = m_CameraPlayer.FindAction("SkipText", throwIfNotFound: true);
        m_CameraPlayer_CameraRollCheat = m_CameraPlayer.FindAction("CameraRollCheat", throwIfNotFound: true);
        m_CameraPlayer_ChangeLense = m_CameraPlayer.FindAction("ChangeLense", throwIfNotFound: true);
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

    // CameraPlayer
    private readonly InputActionMap m_CameraPlayer;
    private List<ICameraPlayerActions> m_CameraPlayerActionsCallbackInterfaces = new List<ICameraPlayerActions>();
    private readonly InputAction m_CameraPlayer_Movement;
    private readonly InputAction m_CameraPlayer_Run;
    private readonly InputAction m_CameraPlayer_TakePhoto;
    private readonly InputAction m_CameraPlayer_Interact;
    private readonly InputAction m_CameraPlayer_Inventory;
    private readonly InputAction m_CameraPlayer_RotatePlayer;
    private readonly InputAction m_CameraPlayer_PauseMenu;
    private readonly InputAction m_CameraPlayer_FocusCamera;
    private readonly InputAction m_CameraPlayer_SkipText;
    private readonly InputAction m_CameraPlayer_CameraRollCheat;
    private readonly InputAction m_CameraPlayer_ChangeLense;
    public struct CameraPlayerActions
    {
        private @PlayerInput m_Wrapper;
        public CameraPlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CameraPlayer_Movement;
        public InputAction @Run => m_Wrapper.m_CameraPlayer_Run;
        public InputAction @TakePhoto => m_Wrapper.m_CameraPlayer_TakePhoto;
        public InputAction @Interact => m_Wrapper.m_CameraPlayer_Interact;
        public InputAction @Inventory => m_Wrapper.m_CameraPlayer_Inventory;
        public InputAction @RotatePlayer => m_Wrapper.m_CameraPlayer_RotatePlayer;
        public InputAction @PauseMenu => m_Wrapper.m_CameraPlayer_PauseMenu;
        public InputAction @FocusCamera => m_Wrapper.m_CameraPlayer_FocusCamera;
        public InputAction @SkipText => m_Wrapper.m_CameraPlayer_SkipText;
        public InputAction @CameraRollCheat => m_Wrapper.m_CameraPlayer_CameraRollCheat;
        public InputAction @ChangeLense => m_Wrapper.m_CameraPlayer_ChangeLense;
        public InputActionMap Get() { return m_Wrapper.m_CameraPlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraPlayerActions set) { return set.Get(); }
        public void AddCallbacks(ICameraPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraPlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraPlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @TakePhoto.started += instance.OnTakePhoto;
            @TakePhoto.performed += instance.OnTakePhoto;
            @TakePhoto.canceled += instance.OnTakePhoto;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Inventory.started += instance.OnInventory;
            @Inventory.performed += instance.OnInventory;
            @Inventory.canceled += instance.OnInventory;
            @RotatePlayer.started += instance.OnRotatePlayer;
            @RotatePlayer.performed += instance.OnRotatePlayer;
            @RotatePlayer.canceled += instance.OnRotatePlayer;
            @PauseMenu.started += instance.OnPauseMenu;
            @PauseMenu.performed += instance.OnPauseMenu;
            @PauseMenu.canceled += instance.OnPauseMenu;
            @FocusCamera.started += instance.OnFocusCamera;
            @FocusCamera.performed += instance.OnFocusCamera;
            @FocusCamera.canceled += instance.OnFocusCamera;
            @SkipText.started += instance.OnSkipText;
            @SkipText.performed += instance.OnSkipText;
            @SkipText.canceled += instance.OnSkipText;
            @CameraRollCheat.started += instance.OnCameraRollCheat;
            @CameraRollCheat.performed += instance.OnCameraRollCheat;
            @CameraRollCheat.canceled += instance.OnCameraRollCheat;
            @ChangeLense.started += instance.OnChangeLense;
            @ChangeLense.performed += instance.OnChangeLense;
            @ChangeLense.canceled += instance.OnChangeLense;
        }

        private void UnregisterCallbacks(ICameraPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @TakePhoto.started -= instance.OnTakePhoto;
            @TakePhoto.performed -= instance.OnTakePhoto;
            @TakePhoto.canceled -= instance.OnTakePhoto;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Inventory.started -= instance.OnInventory;
            @Inventory.performed -= instance.OnInventory;
            @Inventory.canceled -= instance.OnInventory;
            @RotatePlayer.started -= instance.OnRotatePlayer;
            @RotatePlayer.performed -= instance.OnRotatePlayer;
            @RotatePlayer.canceled -= instance.OnRotatePlayer;
            @PauseMenu.started -= instance.OnPauseMenu;
            @PauseMenu.performed -= instance.OnPauseMenu;
            @PauseMenu.canceled -= instance.OnPauseMenu;
            @FocusCamera.started -= instance.OnFocusCamera;
            @FocusCamera.performed -= instance.OnFocusCamera;
            @FocusCamera.canceled -= instance.OnFocusCamera;
            @SkipText.started -= instance.OnSkipText;
            @SkipText.performed -= instance.OnSkipText;
            @SkipText.canceled -= instance.OnSkipText;
            @CameraRollCheat.started -= instance.OnCameraRollCheat;
            @CameraRollCheat.performed -= instance.OnCameraRollCheat;
            @CameraRollCheat.canceled -= instance.OnCameraRollCheat;
            @ChangeLense.started -= instance.OnChangeLense;
            @ChangeLense.performed -= instance.OnChangeLense;
            @ChangeLense.canceled -= instance.OnChangeLense;
        }

        public void RemoveCallbacks(ICameraPlayerActions instance)
        {
            if (m_Wrapper.m_CameraPlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraPlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraPlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraPlayerActions @CameraPlayer => new CameraPlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    public interface ICameraPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnTakePhoto(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnRotatePlayer(InputAction.CallbackContext context);
        void OnPauseMenu(InputAction.CallbackContext context);
        void OnFocusCamera(InputAction.CallbackContext context);
        void OnSkipText(InputAction.CallbackContext context);
        void OnCameraRollCheat(InputAction.CallbackContext context);
        void OnChangeLense(InputAction.CallbackContext context);
    }
}
