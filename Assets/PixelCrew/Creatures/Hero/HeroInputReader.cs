using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Creatures.Hero
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Interact();
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _hero.Dash();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }

        public void OnUse(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _hero.PerformThrowing();
            }

            if (context.canceled)
            {
                _hero.UseInventory();
            }
        }

        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.NextItem();
            }
        }
        
        public void OnPress1(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UsePerk1();
            }
        }
        
        public void OnPress2(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UsePerk2();
            }
        }
        
        public void OnPress3(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UsePerk3();
            }
        }
    }
}