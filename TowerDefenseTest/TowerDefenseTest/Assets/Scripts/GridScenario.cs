using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScenario : MonoBehaviour
{

	#region Fields
	[SerializeField]
	Vector2Int size;
    [SerializeField]
    GameObject tilePrefab;

    public Tile[,] map;
	public List<Tile> spawnPoints; 

	[SerializeField]
	TileFactory tileFactory;
    #endregion

    #region Unity methods


    #endregion

    #region Private methods
    private List<Tile> RandomScenario(int number, TileType type)
    {
		List<Tile> tileChanges = new List<Tile>(); 
		int seedX = 0;
		int seedY = 0;
		for(int i = 0; i < number; i++)
        {
			if (type == TileType.Crystal) seedX = Random.Range(size.x / 2, size.x);
			else if (type != TileType.SpawnPoint) seedX = Random.Range(0, size.x);
			seedY = Random.Range(0, size.y);
			if (map[seedX, seedY].Content.type == TileType.Plain)
            {
				ToggleContent(map[seedX, seedY], type);
				tileChanges.Add(map[seedX, seedY]);
			}
			else
            {
				RandomScenario(1, type);
            }
		}
		return tileChanges;
	}

	
    #endregion

    #region Public / Protected methods
    public void Init(Vector2Int size, int mountains, int crystal, int spawns)
    {
		this.size = size;
		map = new Tile[size.x, size.y];
		for (int y = 0; y < size.y; y++)
		{
			for (int x = 0; x < size.x; x++)
			{
				GameObject tile = (GameObject)Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
				tile.name = "Tile_" + x + "_" + y;
				tile.GetComponent<Tile>().x = x;
				tile.GetComponent<Tile>().y = y;
				tile.GetComponent<Tile>().Content = tileFactory.GetTile(TileType.Plain);
				tile.transform.SetParent(this.transform);
				tile.isStatic = true;
				map[x, y] = tile.GetComponent<Tile>();
			}
		}
		RandomScenario(mountains, TileType.Mountain);
		RandomScenario(crystal, TileType.Crystal);
		spawnPoints = RandomScenario(spawns, TileType.SpawnPoint);
	}

    public void ToggleContent(Tile tile, TileType type)
    {
		if (tile.Content.type == TileType.Plain)
        {
			tile.Content = tileFactory.GetTile(type);
        }
		else if (tile.Content.type == type)
        {
			tile.Content = tileFactory.GetTile(TileType.Plain);
		}
    }

	public void ClearAllsearchFrom()
	{
		for (int y = 0; y < size.y; y++)
		{
			for (int x = 0; x < size.x; x++)
			{
				map[x, y].searchFrom = null;
			}
		}
	}
	#endregion
}