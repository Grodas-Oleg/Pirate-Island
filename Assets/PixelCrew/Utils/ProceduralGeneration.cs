using UnityEngine;

namespace PixelCrew.Utils
{
    public class ProceduralGeneration : MonoBehaviour
    {
        [SerializeField] private int _width, _height;
        [SerializeField] private GameObject _tile;

        private void Start()
        {
            Generation();
        }

        private void Generation()
        {
            for (int x = 0; x < _width; x++)
            {
                int minHeight = _height - 1;
                int maxHeight = _height + 2;

                _height = Random.Range(minHeight, maxHeight);

                for (int y = 0; y < _height; y++)
                {
                    SpawnObject(_tile, x, y);
                }

                SpawnObject(_tile, x, _height);
            }
        }

        private void SpawnObject(GameObject go, int width, int height)
        {
            go = Instantiate(go, new Vector2(width, height), Quaternion.identity);
            go.transform.parent = transform;
        }
    }
}