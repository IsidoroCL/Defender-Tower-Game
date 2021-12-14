using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

	#region Fields
	public int x;
	public int y;
	
	private TileContentType content;
    public GameObject searchingSymbol;

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

    public Tile searchFrom;
    #endregion

    #region Unity methods
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
    public void Searching()
    {
        searchingSymbol.SetActive(true);
    }

    public void NoSearching()
    {
        searchingSymbol.SetActive(false);
    }
    #endregion
}