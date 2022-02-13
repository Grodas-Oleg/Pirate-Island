using PixelCrew.Components.Health;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.Creatures.Boss.Patric
{
    public class HealthAnimationGlue : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private Animator _animator;

        private static readonly int Health = Animator.StringToHash("Health");

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Awake()
        {
            _trash.Retain(_health._onChange.Subscribe(OnHealthChanged));
            OnHealthChanged(_health.Health);
        }

        private void OnHealthChanged(int health)
        {
            _animator.SetInteger(Health, health);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}