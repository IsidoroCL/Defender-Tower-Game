using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileFactory", menuName = "Factory/TileFactory")]
public class TileFactory : GameObjectFactory
{

	#region Fields
	[SerializeField]
	TileContentType plain;

	[SerializeField]
	TileContentType mountain;

	[SerializeField]
	TileContentType forest;

	[SerializeField]
	TileContentType cannonTurret;

	[SerializeField]
	TileContentType laserTurret;

	[SerializeField]
	TileContentType crystal;

	[SerializeField]
	TileContentType spawn;
	#endregion

	#region Unity methods

	#endregion

	#region Private methods
	private TileContentType Get(TileContentType tile)
    {
		TileContentType tileTemp = CreateGameObjectInstance<TileContentType>(tile);
		tileTemp.factory = this;
		return tileTemp;
	}
	#endregion

	#region Public / Protected methods
	public TileContentType GetTile(TileType type)
    {
		switch(type)
        {
			case TileType.Plain:
				return Get(plain);
			case TileType.Mountain:
				return Get(mountain);
			case TileType.Forest:
				return Get(forest);
			case TileType.CannonTurret:
				return Get(cannonTurret);
			case TileType.LaserTurret:
				return Get(laserTurret);
			case TileType.Crystal:
				return Get(crystal);
			case TileType.SpawnPoint:
				return Get(spawn);
		}
		return null;
    }

	public void Recycle(TileContentType tile)
    {
		Destroy(tile.gameObject);
    }
	#endregion
}