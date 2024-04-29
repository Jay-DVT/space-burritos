using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class EmployeeSpawner : MonoBehaviour
{
    public GameObject employeePrefab;
    public Tilemap workArea;
    public int numberOfEmployees = 2; // Number of employees to spawn

    void Start()
    {
        SpawnEmployees();
    }

    void SpawnEmployees()
    {
        BoundsInt bounds = workArea.cellBounds;

        // Calculate the center tile coordinates
        int centerX = (bounds.xMin + bounds.xMax) / 2;
        int centerY = (bounds.yMin + bounds.yMax) / 2;

        // Define the 2x2 center area, adjusting as needed for an exact center if bounds are even
        Vector3Int[] centerTiles = new Vector3Int[]
        {
            new Vector3Int(centerX - 1, centerY - 1, 0), // Bottom-left corner of the 2x2 area
            new Vector3Int(centerX, centerY - 1, 0),     // Bottom-right corner
            new Vector3Int(centerX - 1, centerY, 0),     // Top-left corner
            new Vector3Int(centerX, centerY, 0)          // Top-right corner
        };

        for (int i = 0; i < numberOfEmployees; i++)
        {
            // Randomly select one of the four center tiles
            Vector3Int selectedTile = centerTiles[Random.Range(0, centerTiles.Length)];

            // Convert tile position to world coordinates and adjust to center within the tile
            Vector3 worldPosition = workArea.CellToWorld(selectedTile) + new Vector3(0.25f, 0.25f, 0); // Adjust for cell size and anchor

            // Instantiate the employee prefab at the calculated world position
            Instantiate(employeePrefab, worldPosition, Quaternion.identity);
        }
    }
}
