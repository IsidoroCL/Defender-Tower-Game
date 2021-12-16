using UnityEngine;

public class Tile : MonoBehaviour
{

	#region Fields
	public int x;
	public int y;
    public Tile searchFrom;
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
            content.homeTile = this;
        }
    }
    #endregion

    #region Unity methods
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
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