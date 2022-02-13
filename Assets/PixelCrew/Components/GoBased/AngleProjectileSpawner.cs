using System;
using System.Collections;
using PixelCrew.Creatures.Weapon;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class AngleProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private float degree = 360f;
        [SerializeField] private int pDirection = 1;
        [SerializeField] private AngleProjectileSequence[] _settings;

        private float _defaultDegree;
        private int _defaultDirection;
        public int Stage { get; set; }

        [ContextMenu("Spawn!")]
        private void Awake()
        {
            _defaultDegree = degree;
            _defaultDirection = pDirection;
        }

        public void LaunchProjectiles()
        {
            StartCoroutine(SpawnProjectiles());
        }

        private IEnumerator SpawnProjectiles()
        {
            if (transform.localScale.x >= 1)
            {
                pDirection = _defaultDirection;
                degree = _defaultDegree;
            }
            else
            {
                pDirection = -pDirection;
                degree = -degree;
            }

            var sequence = _settings[Stage];
            foreach (var settings in sequence.Sequence)
            {
                var sectorStep = (degree / 360) * 2 * Mathf.PI / settings.BursCount;
                for (int i = 0, burstCount = 1; i < settings.BursCount; i++)
                {
                    var angle = sectorStep * i;
                    var direction = new Vector2(Mathf.Cos(angle) * pDirection, Mathf.Sin(angle) * pDirection);

                    var instance = SpawnUtils.Spawn(settings.Prefab.gameObject, transform.position);
                    var projectile = instance.GetComponent<DirectionalProjectile>();
                    projectile.transform.localScale = transform.localScale * 0.7f;
                    projectile.Launch(direction);

                    if (burstCount < settings.ItemPerBurst) continue;

                    burstCount = 0;
                    yield return new WaitForSeconds(settings.Delay);
                }
            }
        }
    }

    [Serializable]
    public struct AngleProjectileSequence
    {
        [SerializeField] private AngleProjectileSettings[] _sequence;

        public AngleProjectileSettings[] Sequence => _sequence;
    }

    [Serializable]
    public struct AngleProjectileSettings
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