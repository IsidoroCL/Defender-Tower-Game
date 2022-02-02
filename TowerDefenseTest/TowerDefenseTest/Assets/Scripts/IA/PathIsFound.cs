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
        else if (enemy.CanAttackBuilding)
        {
            return true;
        } 
        else
        {
            foreach (TileContentType castle in Game.castles)
            {
                if (enemy.path.Contains(castle.tileContainer))
                {
                    return true;
                }
            }
            return false;
        }

    }
    #endregion

}