//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Eunji/Input/TowerInputActions.inputactions
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

public partial class @TowerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TowerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TowerInputActions"",
    ""maps"": [
        {
            ""name"": ""Tower"",
            ""id"": ""6e88b66e-b43e-4619-886e-03d89446319b"",
            ""actions"": [
                {
                    ""name"": ""Remove"",
                    ""type"": ""Button"",
                    ""id"": ""c9d54948-b3df-48c0-8c40-babbd6656db2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2840c405-a4c4-47d1-b036-a0cb1294b90f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Remove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Test"",
            ""id"": ""cb76f6f3-b5ba-4f15-a7a4-40d1383defd8"",
            ""actions"": [
                {
                    ""name"": ""Test1"",
                    ""type"": ""Button"",
                    ""id"": ""d57445f8-b645-4ba7-b9f2-160c5d133292"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Test2"",
                    ""type"": ""Button"",
                    ""id"": ""3dd65780-1a85-4820-997b-5e8f99d21f79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Test3"",
                    ""type"": ""Button"",
                    ""id"": ""e762aae9-b4cc-433f-9d94-404405dab6a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Test4"",
                    ""type"": ""Button"",
                    ""id"": ""ac5e6cec-0136-43f3-844e-55a38a7b0c30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c560dcf-8528-4997-ba59-10ce426eb86f"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Test1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0a0f0af-5b54-46e3-97e3-13a545355fac"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard&Mouse"",
                    ""action"": ""Test1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d1ba531-69f8-4594-9054-9b2038e899e7"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Test2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53ba1fa3-ae66-4f46-b7d9-828d0d1ca729"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Test2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f31ceadb-5e08-488f-9b62-cfe169822049"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard&Mouse"",
                    ""action"": ""Test2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""537053a4-a923-45a1-83a8-d8858a520eb8"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Test3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4038159b-6ca8-421e-a181-60c6963acff5"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard&Mouse"",
                    ""action"": ""Test3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da12fe2d-27a0-47eb-abdc-cf9dc8aef6ee"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Test4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a76e5d9-ac5d-499f-878b-f8f5da58c496"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard&Mouse"",
                    ""action"": ""Test4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
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
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // Tower
        m_Tower = asset.FindActionMap("Tower", throwIfNotFound: true);
        m_Tower_Remove = m_Tower.FindAction("Remove", throwIfNotFound: true);
        // Test
        m_Test = asset.FindActionMap("Test", throwIfNotFound: true);
        m_Test_Test1 = m_Test.FindAction("Test1", throwIfNotFound: true);
        m_Test_Test2 = m_Test.FindAction("Test2", throwIfNotFound: true);
        m_Test_Test3 = m_Test.FindAction("Test3", throwIfNotFound: true);
        m_Test_Test4 = m_Test.FindAction("Test4", throwIfNotFound: true);
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

    // Tower
    private readonly InputActionMap m_Tower;
    private ITowerActions m_TowerActionsCallbackInterface;
    private readonly InputAction m_Tower_Remove;
    public struct TowerActions
    {
        private @TowerInputActions m_Wrapper;
        public TowerActions(@TowerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Remove => m_Wrapper.m_Tower_Remove;
        public InputActionMap Get() { return m_Wrapper.m_Tower; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TowerActions set) { return set.Get(); }
        public void SetCallbacks(ITowerActions instance)
        {
            if (m_Wrapper.m_TowerActionsCallbackInterface != null)
            {
                @Remove.started -= m_Wrapper.m_TowerActionsCallbackInterface.OnRemove;
                @Remove.performed -= m_Wrapper.m_TowerActionsCallbackInterface.OnRemove;
                @Remove.canceled -= m_Wrapper.m_TowerActionsCallbackInterface.OnRemove;
            }
            m_Wrapper.m_TowerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Remove.started += instance.OnRemove;
                @Remove.performed += instance.OnRemove;
                @Remove.canceled += instance.OnRemove;
            }
        }
    }
    public TowerActions @Tower => new TowerActions(this);

    // Test
    private readonly InputActionMap m_Test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_Test_Test1;
    private readonly InputAction m_Test_Test2;
    private readonly InputAction m_Test_Test3;
    private readonly InputAction m_Test_Test4;
    public struct TestActions
    {
        private @TowerInputActions m_Wrapper;
        public TestActions(@TowerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Test1 => m_Wrapper.m_Test_Test1;
        public InputAction @Test2 => m_Wrapper.m_Test_Test2;
        public InputAction @Test3 => m_Wrapper.m_Test_Test3;
        public InputAction @Test4 => m_Wrapper.m_Test_Test4;
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @Test1.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest1;
                @Test1.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest1;
                @Test1.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest1;
                @Test2.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest2;
                @Test2.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest2;
                @Test2.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest2;
                @Test3.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest3;
                @Test3.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest3;
                @Test3.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest3;
                @Test4.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest4;
                @Test4.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest4;
                @Test4.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest4;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Test1.started += instance.OnTest1;
                @Test1.performed += instance.OnTest1;
                @Test1.canceled += instance.OnTest1;
                @Test2.started += instance.OnTest2;
                @Test2.performed += instance.OnTest2;
                @Test2.canceled += instance.OnTest2;
                @Test3.started += instance.OnTest3;
                @Test3.performed += instance.OnTest3;
                @Test3.canceled += instance.OnTest3;
                @Test4.started += instance.OnTest4;
                @Test4.performed += instance.OnTest4;
                @Test4.canceled += instance.OnTest4;
            }
        }
    }
    public TestActions @Test => new TestActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface ITowerActions
    {
        void OnRemove(InputAction.CallbackContext context);
    }
    public interface ITestActions
    {
        void OnTest1(InputAction.CallbackContext context);
        void OnTest2(InputAction.CallbackContext context);
        void OnTest3(InputAction.CallbackContext context);
        void OnTest4(InputAction.CallbackContext context);
    }
}
