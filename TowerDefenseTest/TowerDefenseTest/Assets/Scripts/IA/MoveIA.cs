using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveIA", menuName = "IA/MoveIA")]
public class MoveIA : Action
{

    #region Fields

    #endregion

    #region Unity methods

    #endregion

    #region Private methods
    private void Move(Enemy enemy)
    {
        enemy.MoveEnemy();
    }
    #endregion

    #region Public / Protected methods
    public override void Act(Enemy enemy)
    {
        Move(enemy);
    }
    #endregion

}