using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace PixelCrew.Utils
{
    public class ProceduralGeneration : MonoBehaviour
    {
        [SerializeField] private int width, height;
        [SerializeField] private float smoothness;
        [SerializeField] private Tilemap mainTilemap, backgroundTilemap;
        [SerializeField] private TileBase mainTile, backgroundTile;
        [SerializeField] [Range(0, 1)] private float modifier;

        private int[,] _map;
        private float _seed;


        public void Generate()
        {
            _seed = Random.Range(-10000, 10000);
            mainTilemap.ClearAllTiles();
            backgroundTilemap.ClearAllTiles();
            ClearTiles();
            _map = GeneratedArray(width, height, true);
            _map = TerrainGeneration(_map);
            RenderMap(_map, mainTilemap, backgroundTilemap, mainTile, backgroundTile);
        }

        private int[,] GeneratedArray(int width, int height, bool empty)
        {
            int[,] map = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = (empty) ? 0 : 1;
                }
            }

            return map;
        }

        private int[,] TerrainGeneration(int[,] map)
        {
            for (int x = 0; x < width; x++)
            {
                var perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, _seed) * height / 2);
                perlinHeight += height / 2;
                for (int y = 0; y < perlinHeight; y++)
                {
                    var caveValue =
                        Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + _seed, (y * modifier) + _seed));
                    map[x, y] = (caveValue == 1) ? 2 : 1;
                }
            }

            return map;
        }

        private void RenderMap(int[,] map, Tilemap mainTMap, Tilemap bgTilemap, TileBase mainT, TileBase bgTile)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == 1)
                    {
                        mainTMap.SetTile(new Vector3Int(x, y, 0), mainT);
                    }
                    else if (map[x, y] == 2)
                    {
                        bgTilemap.SetTile(new Vector3Int(x, y, 0), bgTile);
                    }
                }
            }
        }

        private void ClearTiles()
        {
            mainTilemap.ClearAllTiles();
            backgroundTilemap.ClearAllTiles();
        }
    }
}