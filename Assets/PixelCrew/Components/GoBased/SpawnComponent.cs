using PixelCrew.Utils;
using PixelCrew.Utils.ObjectPool;
using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _usePool;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var position = _target.position;
            var instance = _usePool
                ? Pool.Instance.Get(_prefab, position)
                : SpawnUtils.Spawn(_prefab, position);

            var scale = _target.lossyScale;
            instance.transform.localScale = scale;
            instance.SetActive(true);
        }

        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }
    }
}