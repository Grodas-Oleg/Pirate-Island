using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
    
        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instanse = Instantiate(_prefab, _target.position, Quaternion.identity);
            
            var scale = _target.lossyScale;
            instanse.transform.localScale = scale;
            instanse.SetActive(true);
        }
    }
}