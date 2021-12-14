using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathIsClear", menuName = "IA/Decision/PathIsClear")]
public class PathIsClear : Decision
{

    #region Fields

    #endregion

    #region Unity methods

    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
    public override bool Evaluate(Enemy enemy)
    {
       return true;
       if (enemy.nextTile.Content.type == TileType.Plain ||
            enemy.nextTile.Content.type == TileType.Crystal)
        {
            return true;
        }
       else
        {
            return false;
        }
    }
    #endregion

}