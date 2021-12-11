using System;
using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;
        
        private GameSession _session;
        public void ApplyDamage(int changeValue)
        {
            if (_health <= 0) return;
            _health += changeValue;
            _onChange?.Invoke(_health);

            if (changeValue > 0)
            {
                _onHeal?.Invoke();
            }
            else if (changeValue < 0)
            {
                _onDamage?.Invoke();
            }

            if (_health <= 0)
            {
                _onDie.Invoke();
            }
        }
#if UNITY_EDITOR
        [ContextMenu("Update Health")]
        private void UpdateHealth()
        {
            _onChange.Invoke(_health);
        }
#endif
        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
        }

        public void SetHealth(int health)
        {
            _health = health;
        }
    }
}