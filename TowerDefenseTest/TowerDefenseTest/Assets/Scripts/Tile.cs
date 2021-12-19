using UnityEngine;

public class Tile : MonoBehaviour
{

    #region Fields
    public int x;
    public int y;
    public Tile parentNode;
    public GameObject pathSymbol;
    private TileContentType content;

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

    #endregion

    #region Public / Protected methods
    public void Initialize(int x, int y, TileContentType type)
    {
        gameObject.name = "Tile_" + x + "_" + y;
        gameObject.isStatic = true;
        this.x = x;
        this.y = y;
        Content = type;
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