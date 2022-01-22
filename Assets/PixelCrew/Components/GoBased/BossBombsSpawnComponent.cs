using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class BossBombsSpawnComponent : MonoBehaviour
    {
        [SerializeField] private int _bursCount;
        [SerializeField] private SpawnComponent _spanwer;
        [SerializeField] private float _delay;

        public void DropBombs()
        {
            StartCoroutine(SpawnBombs());
        }

        private IEnumerator SpawnBombs()
        {
            for (int i = 0; i < _bursCount; i++)
            {
                var _randomX = Random.Range(6, 22);
                _spanwer.transform.position = new Vector2(_randomX, _spanwer.transform.position.y);
                _spanwer.Spawn();

                yield return new WaitForSeconds(_delay);
            }
        }
    }
}