using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int with, height; 
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform cam;

    void Start()
    {
        GenerateGrid();
        
        List<ButtonSelectLevelData> tests = DataManager.DeployGetData();
        foreach(ButtonSelectLevelData test in tests)
        {
            Debug.Log(test.indexLevel);
        }
    }

    void GenerateGrid()
    {
        for (int x = 0; x < with; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity, this.transform);
                spawnTile.name = $"Tile {x}, {y}";
                spawnTile.tag = "Grid";
            }
        }

        cam.transform.position = new Vector3((float)with/2 - 2.85f, (float)height/2 - 0.5f, -10);
    }
}
