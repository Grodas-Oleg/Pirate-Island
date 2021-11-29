using UnityEngine;

namespace PixelCrew.Components
{
    public class HealthChangeComponent : MonoBehaviour
    {
        [SerializeField] private int _value;
        public void ApplyDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_value);
            }
        }
    }
}