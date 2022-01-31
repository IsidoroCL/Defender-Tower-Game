using UnityEngine;

public class UIButtonConstruct : MonoBehaviour
{

	#region Fields

    #endregion

    #region Unity methods

    #endregion

    #region Private methods
    #endregion

    #region Public / Protected methods
    public static void ClickedCannon()
    {
        Game.tileTypeSelected = TileType.CannonTurret;
    }

    public static void ClickedLaser()
    {
        Game.tileTypeSelected = TileType.LaserTurret;
    }

    public static void ClickedQuarry()
    {
        Game.tileTypeSelected = TileType.Quarry;
    }

    public static void ClickedWall()
    {
        Game.tileTypeSelected = TileType.Wall;
    }
    #endregion
}