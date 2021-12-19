using System;
using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] public int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] public UnityEvent _onDie;
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

        private void OnDestroy()
        {
            _onDie.RemoveAllListeners();
        }

        public void SetHealth(int health)
        {
            _health = health;
        }
    }
}