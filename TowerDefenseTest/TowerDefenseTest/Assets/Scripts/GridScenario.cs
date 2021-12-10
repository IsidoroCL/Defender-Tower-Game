using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScenario : MonoBehaviour
{

	#region Fields
	[SerializeField]
	Vector2Int size;
	[SerializeField]
	int numberOfMountainChains;
	[SerializeField]
	int numberOfCrystals;
    [SerializeField]
    GameObject tilePrefab;

    public Tile[,] map;

	[SerializeField]
	TileFactory tileFactory;
    #endregion

    #region Unity methods
    private void Awake()
    {
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
		RandomScenario(numberOfMountainChains, TileType.Mountain);
		RandomScenario(numberOfCrystals, TileType.Crystal);
	}

    #endregion

    #region Private methods
    private void RandomScenario(int number, TileType type)
    {
		for(int i = 0; i < number; i++)
        {
			int seedX = Random.Range(0, size.x);
			int seedY = Random.Range(0, size.y);
			if (map[seedX, seedY].Content.type == TileType.Plain)
            {
				ToggleContent(map[seedX, seedY], type);
			}
			else
            {
				RandomScenario(1, type);
            }
		}
	}
    #endregion

    #region Public / Protected methods
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
	#endregion
}