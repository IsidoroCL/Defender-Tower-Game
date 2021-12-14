using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveIA", menuName = "IA/MoveIA")]
public class MoveIA : Action
{

    #region Fields
    Tile nextTile;
    #endregion

    #region Unity methods

    #endregion

    #region Private methods
    private void Move(Enemy enemy)
    {
        /*
        if (nextTile == null &&
            enemy.path.Count > 0)
        {
            nextTile = enemy.path.Pop();
        }
        else
        {
            enemy.transform.localPosition = Vector3.LerpUnclamped(enemy.currentTile.transform.position, nextTile.transform.position, Time.deltaTime);
        }
        if (enemy.transform.localPosition == nextTile.transform.position &&
            enemy.path.Count > 0)
        {
            nextTile = enemy.path.Pop();
        }*/

    }
    #endregion

    #region Public / Protected methods
    public override void Act(Enemy enemy)
    {
        Move(enemy);
    }
    #endregion

}