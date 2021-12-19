using UnityEngine;

[CreateAssetMenu(fileName = "PathIsFound", menuName = "IA/Decision/PathIsFound")]
public class PathIsFound : Decision
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
        if (enemy.path.Count < 1)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    #endregion

}