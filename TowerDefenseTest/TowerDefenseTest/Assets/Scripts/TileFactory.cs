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

    [SerializeField]
    TileContentType quarry;

    [SerializeField]
    TileContentType wall;

    [SerializeField]
    TileContentType woodenwall;
    #endregion

    #region Unity methods

    #endregion

    #region Private methods
    private TileContentType Get(TileContentType tile)
    {
        Game.money -= tile.cost;
        if (Game.money < 0)
        {
            Game.money += tile.cost;
            TileContentType tileNull = Get(plain);
            tileNull.IsBuildable = true;
            return tileNull;
        }
        TileContentType tileTemp = CreateGameObjectInstance(tile);
        tileTemp.factory = this;
        return tileTemp;
    }
    #endregion

    #region Public / Protected methods
    public TileContentType GetTile(TileType type)
    {
        switch (type)
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
            case TileType.Quarry:
                return Get(quarry);
            case TileType.Wall:
                return Get(wall);
            case TileType.WoodenWall:
                return Get(woodenwall);
        }
        return null;
    }

    public void Recycle(TileContentType tile)
    {
        Destroy(tile.gameObject);
    }
    #endregion
}