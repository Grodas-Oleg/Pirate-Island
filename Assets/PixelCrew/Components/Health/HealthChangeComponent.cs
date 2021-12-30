﻿using UnityEngine;

namespace PixelCrew.Components.Health
{
    public class HealthChangeComponent : MonoBehaviour
    {
        [SerializeField] public int _value;
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