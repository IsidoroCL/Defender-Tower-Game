using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackIA", menuName = "IA/AttackIA")]
public class AttackIA : Action
{

    #region Fields

    #endregion

    #region Unity methods

    #endregion

    #region Private methods
    private void Attack(Enemy enemy)
    {
        List<Tile> neighbors = Game.GetNeighbor(enemy.currentTile);
        foreach (Tile neighbor in neighbors)
        {
            if ((enemy.CanAttackBuilding &&
                neighbor.Content.Health > 0) ||
                neighbor.Content.type == TileType.Crystal)
            {
                enemy.AttackBuilding(neighbor);
                break;
            }
        }
    }
    #endregion

    #region Public / Protected methods

    #endregion
    public override void Act(Enemy enemy)
    {
        Attack(enemy);
    }
}