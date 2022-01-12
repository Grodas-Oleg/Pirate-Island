// GENERATED AUTOMATICALLY FROM 'Assets/UserInput/HeroInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @HeroInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @HeroInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HeroInputAction"",
    ""maps"": [
        {
            ""name"": ""Hero"",
            ""id"": ""84890732-fe05-4520-ab03-69bfdcd1cc73"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""53d29f1d-2ec0-4608-bb0c-65e55691a65b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""eb9d742c-c42d-4854-8cda-3b89462afd09"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""475fb8ed-4a01-4dd0-800e-ece21599e34a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""e1d83174-33d1-4f1a-8476-8a037d9d6432"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""7b0c6ac6-7144-4a7b-8c3a-a59437e263d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NextItem"",
                    ""type"": ""Button"",
                    ""id"": ""05ff5dd7-86b9-4fb9-a8c3-8d3a294ab818"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsePerk1"",
                    ""type"": ""Button"",
                    ""id"": ""784aa7a9-899d-4fac-bb7b-1aa14bab9612"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsePerk2"",
                    ""type"": ""Button"",
                    ""id"": ""26efeb27-a7fc-42af-aebb-b1d7dd6e0e46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsePerk3"",
                    ""type"": ""Button"",
                    ""id"": ""b51f85c1-60a6-4647-a9ee-617dd251b8bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""b8c51899-298f-4c4f-8cab-3ede63c76437"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8f680ac9-5de1-4547-a9e4-8f1785ced2b4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8cc55c6a-1ae4-474b-b550-c2a4cbc794ea"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aea823b1-1507-453d-88d1-547b35b4116f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""514d1d65-ef94-44db-af20-c617e788c57d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""15699690-d7a6-49f2-bb5f-a62be3db752e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7d7e6517-2015-4d3a-af2e-694bc25bf08b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""38bb3fe5-f1e5-4d07-bd50-911493607824"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""0efaf87c-6f25-46c0-ba6a-71b6ed0b0bdd"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""757e73a1-36b4-44b5-9bf2-b51128b25d89"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ad526613-d40f-43ce-9dff-2e5dfa709845"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fdd3106-b9a9-4e16-bd3d-3f012d109b28"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2937e9d5-6b00-4503-989c-90d591dabae1"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2582c8b3-3d5a-4394-ad5e-8148088551d7"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3bf5df2-1796-410f-8c78-0e68baaa606a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edbc51f7-305d-4abf-90f3-b096bf7d44af"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePerk1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c81239e3-a48d-40f6-98a0-8f3a4cb33539"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePerk2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7eb5fa4-eefa-4fe9-988a-04d2d2c93ce0"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePerk3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Hero
        m_Hero = asset.FindActionMap("Hero", throwIfNotFound: true);
        m_Hero_Movement = m_Hero.FindAction("Movement", throwIfNotFound: true);
        m_Hero_Interact = m_Hero.FindAction("Interact", throwIfNotFound: true);
        m_Hero_Dash = m_Hero.FindAction("Dash", throwIfNotFound: true);
        m_Hero_Attack = m_Hero.FindAction("Attack", throwIfNotFound: true);
        m_Hero_Throw = m_Hero.FindAction("Throw", throwIfNotFound: true);
        m_Hero_NextItem = m_Hero.FindAction("NextItem", throwIfNotFound: true);
        m_Hero_UsePerk1 = m_Hero.FindAction("UsePerk1", throwIfNotFound: true);
        m_Hero_UsePerk2 = m_Hero.FindAction("UsePerk2", throwIfNotFound: true);
        m_Hero_UsePerk3 = m_Hero.FindAction("UsePerk3", throwIfNotFound: true);
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

    // Hero
    private readonly InputActionMap m_Hero;
    private IHeroActions m_HeroActionsCallbackInterface;
    private readonly InputAction m_Hero_Movement;
    private readonly InputAction m_Hero_Interact;
    private readonly InputAction m_Hero_Dash;
    private readonly InputAction m_Hero_Attack;
    private readonly InputAction m_Hero_Throw;
    private readonly InputAction m_Hero_NextItem;
    private readonly InputAction m_Hero_UsePerk1;
    private readonly InputAction m_Hero_UsePerk2;
    private readonly InputAction m_Hero_UsePerk3;
    public struct HeroActions
    {
        private @HeroInputAction m_Wrapper;
        public HeroActions(@HeroInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Hero_Movement;
        public InputAction @Interact => m_Wrapper.m_Hero_Interact;
        public InputAction @Dash => m_Wrapper.m_Hero_Dash;
        public InputAction @Attack => m_Wrapper.m_Hero_Attack;
        public InputAction @Throw => m_Wrapper.m_Hero_Throw;
        public InputAction @NextItem => m_Wrapper.m_Hero_NextItem;
        public InputAction @UsePerk1 => m_Wrapper.m_Hero_UsePerk1;
        public InputAction @UsePerk2 => m_Wrapper.m_Hero_UsePerk2;
        public InputAction @UsePerk3 => m_Wrapper.m_Hero_UsePerk3;
        public InputActionMap Get() { return m_Wrapper.m_Hero; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HeroActions set) { return set.Get(); }
        public void SetCallbacks(IHeroActions instance)
        {
            if (m_Wrapper.m_HeroActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Dash.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnDash;
                @Attack.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttack;
                @Throw.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @NextItem.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnNextItem;
                @NextItem.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnNextItem;
                @NextItem.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnNextItem;
                @UsePerk1.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePerk1;
                @UsePerk1.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePerk1;
                @UsePerk1.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePerk1;
                @UsePerk2.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePerk2;
                @UsePerk2.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePerk2;
                @UsePerk2.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePerk2;
                @UsePerk3.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePerk3;
                @UsePerk3.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePerk3;
                @UsePerk3.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePerk3;
            }
            m_Wrapper.m_HeroActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @NextItem.started += instance.OnNextItem;
                @NextItem.performed += instance.OnNextItem;
                @NextItem.canceled += instance.OnNextItem;
                @UsePerk1.started += instance.OnUsePerk1;
                @UsePerk1.performed += instance.OnUsePerk1;
                @UsePerk1.canceled += instance.OnUsePerk1;
                @UsePerk2.started += instance.OnUsePerk2;
                @UsePerk2.performed += instance.OnUsePerk2;
                @UsePerk2.canceled += instance.OnUsePerk2;
                @UsePerk3.started += instance.OnUsePerk3;
                @UsePerk3.performed += instance.OnUsePerk3;
                @UsePerk3.canceled += instance.OnUsePerk3;
            }
        }
    }
    public HeroActions @Hero => new HeroActions(this);
    public interface IHeroActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnNextItem(InputAction.CallbackContext context);
        void OnUsePerk1(InputAction.CallbackContext context);
        void OnUsePerk2(InputAction.CallbackContext context);
        void OnUsePerk3(InputAction.CallbackContext context);
    }
}
