using CommandChoice.Model;
using UnityEngine;

namespace CommandChoice.Component
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridModel model;
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private GameObject player;

        void Awake()
        {

            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Start()
        {
            GenerateGrid();
        }

        void GenerateGrid()
        {
            Vector3 posPlayer = player.GetComponent<Transform>().position;
            for (int x = 0; x < model.Width; x++)
            {
                for (int y = 0; y < model.Height; y++)
                {
                    var spawnedTile = Instantiate(_tilePrefab, new Vector3(posPlayer.x + x, posPlayer.y + y, 20), Quaternion.identity);
                    var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                    spawnedTile.GetComponent<SpriteRenderer>().color = isOffset ? model.FirstColor : model.SecondColor;
                    spawnedTile.transform.SetParent(transform);
                    spawnedTile.name = $"Tile {x} {y}";
                }
            }
        }
    }
}