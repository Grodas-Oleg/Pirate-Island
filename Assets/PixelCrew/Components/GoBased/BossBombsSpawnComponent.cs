using System.Collections;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class BossBombsSpawnComponent : MonoBehaviour
    {
        [SerializeField] private int _bursCount;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _position;
        [SerializeField] private float _delay;

        public void DropBombs()
        {
            StartCoroutine(SpawnBombs());
        }

        private IEnumerator SpawnBombs()
        {
            for (int i = 0; i < _bursCount; i++)
            {
                var randomX = Random.Range(6, 22);
                var position = new Vector2(randomX, _position.transform.position.y);
                SpawnUtils.Spawn(_prefab, position);
                // Мб стоит вернуть spawn component, сделать через него + Pool
                // _spanwer.transform.position = new Vector2(_randomX, _spanwer.transform.position.y);
                // _spanwer.Spawn();

                yield return new WaitForSeconds(_delay);
            }
        }
    }
}