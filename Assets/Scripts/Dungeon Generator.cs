using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] GameObject pointVisualization;
    [SerializeField] GameObject emptySpace;
    [SerializeField] GameObject caveWall;
    [SerializeField] Vector2Int size;
    [SerializeField] int minSteps;
    [SerializeField] int maxSteps;
    [ContextMenu("Regenerate Cave")]
    public void Regenerate()
    {
#if UNITY_EDITOR
        // Clear old tiles
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Regenerate and place
        GenerateSetup();
#endif
    }
    void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateSetup();
    }

    void GenerateSetup()
    {
        bool[,] grid = new bool[size.x, size.y];
        Vector2Int[] startingPoints = GetStartingPoints(size);
        foreach (var point in startingPoints)
        {
            StartGenerate(grid, Random.Range(minSteps, maxSteps) * 100, point);
        }
        PlaceTileset(grid);
        PlacePoints(startingPoints);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector2Int[] GetStartingPoints(Vector2Int gridSize, int margin = 5)
    {
        Vector2Int[] points = new Vector2Int[5];

        Vector2Int[] centers = new Vector2Int[]
        {
        new Vector2Int(gridSize.x / 4, gridSize.y * 3 / 4),     // top left
        new Vector2Int(gridSize.x * 3 / 4, gridSize.y * 3 / 4), // top right
        new Vector2Int(gridSize.x / 2, gridSize.y / 2),         // center
        new Vector2Int(gridSize.x / 4, gridSize.y / 4),         // bottom left
        new Vector2Int(gridSize.x * 3 / 4, gridSize.y / 4),     // bottom right
        };

        for (int i = 0; i < centers.Length; i++)
        {
            int x = Random.Range(Mathf.Max(0, centers[i].x - margin), Mathf.Min(gridSize.x, centers[i].x + margin));
            int y = Random.Range(Mathf.Max(0, centers[i].y - margin), Mathf.Min(gridSize.y, centers[i].y + margin));
            points[i] = new Vector2Int(x, y);
        }

        return points;
    }


    void StartGenerate(bool[,] grid, int steps, Vector2Int startingPoint)
    {
        //Setup
        List<Vector2Int> movements = new List<Vector2Int>() { Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.up };

        Vector2Int currentPosition = startingPoint;
        grid[currentPosition.x, currentPosition.y] = true;

        //Start random process
        for (int i = 0; i < steps; i++)
        {
            List<Vector2Int> validMoves = new List<Vector2Int>();

            foreach (var move in movements)
            {
                Vector2Int next = currentPosition + move;
                if (IsInMargin(next, size))
                {
                    validMoves.Add(move);
                }
            }

            if (validMoves.Count == 0) break;

            Vector2Int chosenMove = validMoves[Random.Range(0, validMoves.Count)];
            currentPosition += chosenMove;
            grid[currentPosition.x, currentPosition.y] = true;
        }
    }

    bool IsInMargin(Vector2Int pos, Vector2Int size, int margin = 5)
    {
        float noiseX = Mathf.PerlinNoise(pos.x * 0.1f, 0f);
        float noiseY = Mathf.PerlinNoise(pos.y * 0.1f, 0f);

        int marginX = margin + Mathf.FloorToInt(noiseX * 5f);
        int marginY = margin + Mathf.FloorToInt(noiseY * 5f);

        return pos.x >= marginX && pos.x < size.x - marginX && pos.y >= marginY && pos.y < size.y - marginY;
    }

    void PlaceTileset(bool[,] grid)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                GameObject prefab = grid[x, y] ? emptySpace : caveWall;
                Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity, this.transform);
            }
        }
    }

    void PlacePoints(Vector2Int[] points)
    {
        foreach (Vector2Int point in points)
        {
            Instantiate(pointVisualization, new Vector3(point.x, point.y, 0), Quaternion.identity, transform);
        }
    }

}
