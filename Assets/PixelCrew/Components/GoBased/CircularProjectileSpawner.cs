using System;
using System.Collections;
using PixelCrew.Creatures.Weapon;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class CircularProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private ProjectileSequence[] _settings;
        public int Stage { get; set; }

        [ContextMenu("Spawn!")]
        public void LaunchProjectiles()
        {
            StartCoroutine(SpawnProjectiles());
        }

        private IEnumerator SpawnProjectiles()
        {
            var sequence = _settings[Stage];
            foreach (var settings in sequence.Sequence)
            {
                var sectorStep = 2 * Mathf.PI / settings.BursCount;
                for (int i = 0, burstCount = 1; i < settings.BursCount; i++)
                {
                    var angle = sectorStep * i;
                    var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                    var instance = SpawnUtils.Spawn(settings.Prefab.gameObject, transform.position);
                    var projectile = instance.GetComponent<DirectionalProjectile>();
                    projectile.Launch(direction);

                    if (burstCount < settings.ItemPerBurst) continue;

                    burstCount = 0;
                    yield return new WaitForSeconds(settings.Delay);
                }
            }
        }
    }

    [Serializable]
    public struct ProjectileSequence
    {
        [SerializeField] private CircularProjectileSettings[] _sequence;

        public CircularProjectileSettings[] Sequence => _sequence;
    }

    [Serializable]
    public struct CircularProjectileSettings
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int _bursCount;
        [SerializeField] private int _itemPerBurst;
        [SerializeField] private float _delay;

        public GameObject Prefab => prefab;
        public int BursCount => _bursCount;
        public int ItemPerBurst => _itemPerBurst;
        public float Delay => _delay;
    }
}