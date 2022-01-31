using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingIsNear", menuName = "IA/Decision/BuildingIsNear")]
public class BuildingIsNear : Decision
{

    #region Fields

    #endregion

    #region Unity methods

    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods

    #endregion
    public override bool Evaluate(Enemy enemy)
    {
        List<Tile> neighbors = Game.GetNeighbor(enemy.currentTile);
        foreach (Tile neighbor in neighbors)
        {
            if ((enemy.CanAttackBuilding &&
                neighbor.Content.Health > 0) ||
                neighbor.Content.type == TileType.Crystal)
                return true;
        }
        return false;

    }
}