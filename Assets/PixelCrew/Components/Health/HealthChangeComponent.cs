using PixelCrew.Components.PopupDamageText;
using UnityEngine;

namespace PixelCrew.Components.Health
{
    public class HealthChangeComponent : MonoBehaviour
    {
        [SerializeField] public int _value;
        private bool _critical;

        public void SetDelta(int delta, bool critical)
        {
            _value = Random.Range(delta - 2, delta + 2);
            _critical = critical;
        }

        public void ApplyDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();

            if (healthComponent == null) return;
            healthComponent.ApplyDamage(_value);

            if (healthComponent.Health <= 0) return;
            DamagePopup.Create(transform.position, _value, _critical);
        }
    }
}