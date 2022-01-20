using UnityEngine;

public class TileContentType : MonoBehaviour
{

    #region Fields
    public TileType type;
    public TileFactory factory;
    public Tile tileContainer;
    public int cost;
    public bool isWalkable;
    #endregion

    #region Unity methods

    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
    public void Recycle()
    {
        factory.Recycle(this);
    }
    #endregion
}