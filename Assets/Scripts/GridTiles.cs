using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTiles : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] float number;
    [SerializeField] float spacing = 1.0f; // Adjust the spacing as needed
    [SerializeField] Vector3 tileSize = new Vector3(1.0f, 1.0f, 1.0f); // Adjust the tile size as needed

    private List<GameObject> tiles = new List<GameObject>(); // Store references to created tiles

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < number; i++)
        {
            for (int y = 0; y < number; y++)
            {
                float xOffset = i * (tileSize.x + spacing);
                float yOffset = y * (tileSize.y + spacing);

                Vector3 tilePosition = new Vector3(xOffset, yOffset, 0f);

                GameObject grid = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                grid.transform.localScale = tileSize; // Set the size of the tile
                tiles.Add(grid); // Add the reference to the list
            }
        }
    }
}
