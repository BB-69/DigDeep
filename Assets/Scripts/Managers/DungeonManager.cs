using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class DungeonManager : MonoBehaviour
{
    /// <summary>
    /// grid -> use to determine the occupied position
    /// blocks dict -> use to check if its diggable & return the type of that block
    /// </summary>
    public static DungeonManager instance { get; private set; }
    public Dictionary<Vector3Int, BlockData> blocks { get; private set; } = new Dictionary<Vector3Int, BlockData>();
    TileLib tileLib;
    CheckpointGenerator checkpointGenerator;
    BlockType[,] grid;
    [SerializeField] GameObject pointVisualization;
    [SerializeField] GameObject playerGO;
    [SerializeField] GameObject[] itemToDrops;
    [SerializeField] TileClickDig tileClickDig;
    GameObject currentPlayer;
    [Header("TILE")]
    public Tilemap tilemap;
    [Header("Dungeon Customization")]
    [SerializeField] CameraSetUp cameraSetUp;
    [SerializeField] Vector2Int size;
    [SerializeField] int minSteps;
    [SerializeField] int maxSteps;
    [SerializeField] float copperChance, ironChance, goldChance, emeraldChance;
    int copperCount, ironCount, goldCount, emeraldCount =0;
    bool isFirstTimeGenerate = true;
    [ContextMenu("Regenerate Cave")]
    public void Regenerate()
    {
        blocks.Clear();
        copperCount = 0;
        ironCount = 0;
        goldCount = 0;
        emeraldCount = 0;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Regenerate and place
        GenerateSetup();
    }
    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this.gameObject); }
        tileLib = GetComponent<TileLib>();
        checkpointGenerator = GetComponent<CheckpointGenerator>();
        
    }

    #region DUNGEON SETUP
    public void SetValue(float copperChance=5f, float ironChance=2f, float goldChance=0f, float emeraldChance=0f)
    {
        this.copperChance = copperChance;
        this.ironChance = ironChance;
        this.goldChance = goldChance;
        this.emeraldChance = emeraldChance;
    }

    void GenerateSetup()
    {
        BlockType[,] tempGrid = new BlockType[size.x, size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                tempGrid[x, y] = BlockType.Normal;
            }
        }

        //get 5 random position equally separated
        List<Vector2Int> startingPoints = GetStartingPoints(size);
        foreach (var point in startingPoints)
        {
            StartGenerate(tempGrid, Random.Range(minSteps, maxSteps) * 100, point);
        }

        //random player position from starting points
        int playerPosIndex = Random.Range(0, 5);
        Vector3 playerPos = new Vector3(startingPoints[playerPosIndex].x, startingPoints[playerPosIndex].y, 0);
        startingPoints.RemoveAt(playerPosIndex);
        //place tile
        PlaceTileset(tempGrid);
        Debug.Log(copperCount);
        Debug.Log(ironCount);
        Debug.Log(goldCount);
        Debug.Log(emeraldCount);
        Debug.Log(playerPos);
        checkpointGenerator.SpawnCheckpointObject(startingPoints, new int[] { copperCount, ironCount, goldCount, emeraldCount });
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerGO, playerPos, Quaternion.identity);
        else currentPlayer.transform.position = playerPos;

        cameraSetUp.SetBounds();
        cameraSetUp.SetPlayer(currentPlayer.transform);
        tileClickDig.SetPlayer(currentPlayer);
    }

    List<Vector2Int> GetStartingPoints(Vector2Int gridSize, int margin = 5)
    {
        List<Vector2Int> points = new List<Vector2Int>();

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
            points.Add(new Vector2Int(x, y));
        }

        return points;
    }


    void StartGenerate(BlockType[,] grid, int steps, Vector2Int startingPoint)
    {
        //Setup
        List<Vector2Int> movements = new List<Vector2Int>() { Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.up };

        Vector2Int currentPosition = startingPoint;
        grid[currentPosition.x, currentPosition.y] = BlockType.Empty;

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
            grid[currentPosition.x, currentPosition.y] = BlockType.Empty;
        }

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y] == BlockType.Normal)
                {
                    grid[x, y] = TryGenerateOre();
                }
            }
        }
    }

    bool IsInMargin(Vector2Int pos, Vector2Int size, int baseMargin = 5)
    {
        //make the edge more natural with perlin noise
        float noise = Mathf.PerlinNoise(pos.x * 0.03f, pos.y * 0.03f);
        int margin = baseMargin + Mathf.FloorToInt(noise * 2f); // 5–7

        return pos.x >= margin && pos.x < size.x - margin &&
               pos.y >= margin && pos.y < size.y - margin;
    }

    void PlaceTileset(BlockType[,] grid)
    {
        Dictionary<BlockType, TileBase> tileCache = new()
    {
        [BlockType.Empty] = tileLib.GetTile("Empty"),
        [BlockType.Normal] = tileLib.GetTile("Wall"),
        [BlockType.Copper] = tileLib.GetTile("CopperOre"),
        [BlockType.Iron] = tileLib.GetTile("IronOre"),
        [BlockType.Gold] = tileLib.GetTile("GoldOre"),
        [BlockType.Emerald] = tileLib.GetTile("EmeraldOre")
    };

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                TileBase tile = tileCache[grid[x, y]];
                switch (grid[x, y])
                {
                    case BlockType.Empty:
                        blocks[new Vector3Int(x, y, 0)] = new BlockData(BlockType.Empty, canDig: false);
                        break;
                    case BlockType.Normal:
                        blocks[new Vector3Int(x, y, 0)] = new BlockData(BlockType.Normal, canDig: true, 10);
                        break;
                    case BlockType.Copper:
                        blocks[new Vector3Int(x, y, 0)] = new BlockData(BlockType.Copper, canDig: true, 25);
                        break;
                    case BlockType.Iron:
                        blocks[new Vector3Int(x, y, 0)] = new BlockData(BlockType.Iron, canDig: true, 40);
                        break;
                    case BlockType.Gold:
                        blocks[new Vector3Int(x, y, 0)] = new BlockData(BlockType.Gold, canDig: true, 60);
                        break;
                    case BlockType.Emerald:
                        blocks[new Vector3Int(x, y, 0)] = new BlockData(BlockType.Emerald, canDig: true, 100);
                        break;
                }

                if (isFirstTimeGenerate)
                {
                    Debug.Log(tileCache[grid[x, y]]);
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
                else
                {
                    if (this.grid[x, y] == grid[x, y]) continue;
                    else { tilemap.SetTile(new Vector3Int(x, y, 0), tile); }
                }

            }

        }
        this.grid = grid;
        isFirstTimeGenerate = false;
    }

    public void TakeDamageBlock(Vector3Int pos, int dmg)
    {
        blocks[pos].hp -= dmg;
        if (blocks[pos].hp <= 0)
        {
            DigTile(pos);
        }
    }

    public BlockType TryGenerateOre()
    {
        // Randomly generate ores around the block
        float oreChance = Random.Range(0f, 100f); // 0 to 100
        float emeraldThreshold = emeraldChance;
        float goldThreshold = emeraldThreshold + goldChance;
        float ironThreshold = goldThreshold + ironChance;
        float copperThreshold = ironThreshold + copperChance;

        if (oreChance < emeraldThreshold)
        {
            emeraldCount++;
            return BlockType.Emerald;
        }
        else if (oreChance < goldThreshold)
        {
            goldCount++;
            return BlockType.Gold;
        }
        else if (oreChance < ironThreshold)
        {
            ironCount++;
            return BlockType.Iron;
        }
        else if (oreChance < copperThreshold)
        {
            copperCount++;
            return BlockType.Copper;
        }
        else
        {
            return BlockType.Normal; // No ore generated
        }


    }

    #endregion

    public void DigTile(Vector3Int position)
    {
        // normal tile has 85% chance to drop items
        if (blocks.TryGetValue(position, out var tile))
        {
            this.grid[position.x, position.y] = BlockType.Empty;
            blocks[position].canDig = false;
            if (tile.blockType == BlockType.Normal)
            {
                CalculateItemDrop(new Vector2(position.x, position.y));
            }
            else
            {
                DropItem(tile.blockType, new Vector2(position.x, position.y));
            }

            tilemap.SetTile(position, tileLib.GetTile("Empty"));
            Debug.Log(tile + "Has been destroy" +  position);
        }
        else
        {
            Debug.LogWarning("Position not found");
        }
    }

    void CalculateItemDrop(Vector2 position)
    {
        int dropChance = Random.Range(0, 100);
        if (dropChance >= 90)
        {
            int weight = Random.Range(0, 100);
            if (weight <= 80)
            {
                DropItem(BlockType.Copper, position);
            }
            else if (weight <= 90)
            {
                DropItem(BlockType.Iron, position);
            }
            else if (weight <= 95)
            {
                DropItem(BlockType.Gold, position);
            }
            else
            {
                PlayerManager.instance.bombThrower.AddBomb(1); // 5% chance to drop bomb
            }
        }
    }

    void DropItem(BlockType blockType, Vector2 position)
    {
        //TODO: Drop ore
        if (blockType == BlockType.Normal) return;
        switch (blockType)
        {
            case BlockType.Copper:
                Instantiate(itemToDrops[0], position, Quaternion.identity);
                break;
            case BlockType.Iron:
                Instantiate(itemToDrops[1], position, Quaternion.identity);
                break;
            case BlockType.Gold:
                Instantiate(itemToDrops[2], position, Quaternion.identity);
                break;
            case BlockType.Emerald:
                Instantiate(itemToDrops[3], position, Quaternion.identity);
                break;
        }
        Debug.Log(blockType.ToString());
    }

    public void SetTile(Vector3Int position, string tileName)
    {
        TileBase tile = tileLib.GetTile(tileName);
        if (tile == null)
        {
            Debug.LogWarning("Tile not found.");
            return;
        }

        tilemap.SetTile(position, tile);
    }
}
