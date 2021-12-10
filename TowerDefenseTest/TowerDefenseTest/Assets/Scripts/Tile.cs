using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

	#region Fields
	public int x;
	public int y;
	
	private TileContentType content;

    public TileContentType Content
    {
        get => content;
        set
        {
            if (content != null) content.Recycle();
            content = value;
            content.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }
    #endregion

    #region Unity methods
    private void Awake()
    {
        
    }
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods

    #endregion
}