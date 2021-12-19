using System.Collections.Generic;
using UnityEngine;

public class GridScenario : MonoBehaviour
{

    #region Fields
    public Vector2Int scenarioSize;
    public Tile[,] scenarioTiles;
    public List<Tile> spawnPoints;

    [SerializeField]
    GameObject tilePrefab;
    [SerializeField]
    TileFactory tileFactory;
    #endregion

    #region Unity methods


    #endregion

    #region Private methods
    private List<Tile> GenerateRandomTiles(GameConfig configuration)
    {
        List<Tile> spawnTiles = new List<Tile>();
        int numberOfTilesToChange = configuration.numberOfMountains +
            configuration.numberOfForests +
            configuration.numberOfCrystals +
            configuration.numberOfSpawns;
        TileType type = TileType.Plain;
        for (int i = 0; i < numberOfTilesToChange; i++)
        {
            int seedX = Random.Range(0, scenarioSize.x);
            int seedY = Random.Range(0, scenarioSize.y);
            if (i < configuration.numberOfMountains)
            {
                type = TileType.Mountain;
            }
            else if (i >= configuration.numberOfMountains &&
                i < (configuration.numberOfMountains + configuration.numberOfForests))
            {
                type = TileType.Forest;
            }
            else if (i >= (configuration.numberOfMountains + configuration.numberOfForests) &&
                i < (configuration.numberOfMountains + configuration.numberOfForests + configuration.numberOfCrystals))
            {
                type = TileType.Crystal;
                seedX = Random.Range(scenarioSize.x / 2, scenarioSize.x);
            }
            else if (i >= (configuration.numberOfMountains + configuration.numberOfForests + configuration.numberOfCrystals))
            {
                type = TileType.SpawnPoint;
                seedX = 0;
            }
            int numberOfTimesTry = 0;
            while (scenarioTiles[seedX, seedY].Content.type != TileType.Plain &&
                numberOfTimesTry < scenarioSize.y - 1)
            {
                seedY = Random.Range(0, scenarioSize.y);
                numberOfTimesTry++;
            }
            scenarioTiles[seedX, seedY].Content = tileFactory.GetTile(type);
            if (type == TileType.SpawnPoint)
            {
                spawnTiles.Add(scenarioTiles[seedX, seedY]);
            }
        }
        return spawnTiles;
    }

    private bool IsCrystalOrSpawnNear(Tile tile)
    {
        foreach (Tile neighbor in Game.GetNeighbor(tile))
        {
            if (neighbor.Content.type == TileType.Crystal ||
                neighbor.Content.type == TileType.SpawnPoint)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Public / Protected methods
    public void Initialize(GameConfig configuration)
    {
        this.scenarioSize = configuration.sizeGrid;
        scenarioTiles = new Tile[scenarioSize.x, scenarioSize.y];
        for (int y = 0; y < scenarioSize.y; y++)
        {
            for (int x = 0; x < scenarioSize.x; x++)
            {
                GameObject tile = (GameObject)Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tile.GetComponent<Tile>().Initialize(x, y, tileFactory.GetTile(TileType.Plain));
                tile.transform.SetParent(this.transform);
                scenarioTiles[x, y] = tile.GetComponent<Tile>();
            }
        }
        spawnPoints = GenerateRandomTiles(configuration);
    }

    public void ToggleContent(Tile tile, TileType type)
    {
        if (tile.Content.type == TileType.Plain &&
            !IsCrystalOrSpawnNear(tile))
        {
            tile.Content = tileFactory.GetTile(type);
        }
        else if (tile.Content.type == type)
        {
            tile.Content = tileFactory.GetTile(TileType.Plain);
        }
    }

    public List<Tile> GetNeighbor(Tile tile)
    {
        int x = tile.x;
        int y = tile.y;
        List<Tile> neighbors = new List<Tile>();
        if (x - 1 >= 0) neighbors.Add(scenarioTiles[x - 1, y]);
        if (x + 1 < scenarioSize.x) neighbors.Add(scenarioTiles[x + 1, y]);
        if (y - 1 >= 0) neighbors.Add(scenarioTiles[x, y - 1]);
        if (y + 1 < scenarioSize.y) neighbors.Add(scenarioTiles[x, y + 1]);

        return neighbors;
    }

    public void ClearTilesForNextPathFinding()
    {
        for (int y = 0; y < scenarioSize.y; y++)
        {
            for (int x = 0; x < scenarioSize.x; x++)
            {
                scenarioTiles[x, y].parentNode = null;
            }
        }
    }
    #endregion
}