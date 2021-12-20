using UnityEngine;

public class Tile : MonoBehaviour
{

    #region Fields
    public int x;
    public int y;
    public Tile parentNode;
    public GameObject pathSymbol;
    private TileContentType content;
    private TileFactory tileFactory;

    public TileContentType Content
    {
        get => content;
        set
        {
            if (content != null) content.Recycle();
            content = value;
            content.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -1);
            content.tileContainer = this;
        }
    }
    #endregion

    #region Unity methods
    #endregion

    #region Private methods
    private bool IsCrystalOrSpawnNear()
    {
        foreach (Tile neighbor in Game.GetNeighbor(this))
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
    public void Initialize(int x, int y, TileContentType type, TileFactory factory)
    {
        gameObject.name = "Tile_" + x + "_" + y;
        gameObject.isStatic = true;
        this.x = x;
        this.y = y;
        Content = type;
        tileFactory = factory;
    }

    public void ToggleContent(TileType type)
    {
        if (Content.type == TileType.Plain &&
            !IsCrystalOrSpawnNear())
        {
            Content = tileFactory.GetTile(type);
        }
        else if (Content.type == type)
        {
            Content = tileFactory.GetTile(TileType.Plain);
        }
    }

    public void HasPath()
    {
        pathSymbol.SetActive(true);
    }

    public void NoPath()
    {
        pathSymbol.SetActive(false);
    }
    #endregion
}