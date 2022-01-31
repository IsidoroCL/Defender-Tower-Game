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
        if (enemy.nextTile == null) return true;
        if (enemy.nextTile.Content.isWalkable
            || enemy.CanFly)
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