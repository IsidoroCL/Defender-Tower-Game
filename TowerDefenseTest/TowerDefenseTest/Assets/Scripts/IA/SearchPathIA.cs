using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchPathIA", menuName = "IA/SearchPathIA")]
public class SearchPathIA : Action
{
    #region Fields
    private Queue<Tile> searchingQueue = new Queue<Tile>();
    private HashSet<Tile> tilesAlreadySearched = new HashSet<Tile>();
    private Tile searchingTile;
    #endregion

    #region Unity methods

    #endregion

    #region Private methods
    private void BFSearch(Enemy enemy)
    {
        ClearCollections();
        Tile origin = enemy.currentTile;
        searchingQueue.Enqueue(origin);
        tilesAlreadySearched.Add(origin);
        origin.parentNode = null;

        while (searchingQueue.Count > 0)
        {
            searchingTile = searchingQueue.Dequeue();
            if (enemy.CanAttackBuilding)
            {
                if (searchingTile.Content.Health > 0)
                {
                    break;
                }
            } 
            else
            {
                if (searchingTile.Content.type == TileType.Crystal)
                {
                    break;
                }
            }
            EnqueueNeighbors(Game.GetNeighbor(searchingTile), enemy);
        }
        enemy.path = CreatePath(searchingTile);
    }

    private void ClearCollections()
    {
        Game.ClearTilesForNextPathFinding();
        searchingQueue.Clear();
        tilesAlreadySearched.Clear();
    }
    
    private void EnqueueNeighbors(List<Tile> neighbors, Enemy enemy)
    {
        foreach (Tile neighbor in neighbors)
        {
            if ((neighbor.Content.isWalkable || 
                enemy.CanFly ||
                (enemy.CanAttackBuilding && neighbor.Content.Health > 0)) 
                &&
                !tilesAlreadySearched.Contains(neighbor))
            {
                tilesAlreadySearched.Add(neighbor);
                searchingQueue.Enqueue(neighbor);
                neighbor.parentNode = searchingTile;
            }
        }
    }
    private Stack<Tile> CreatePath(Tile destination)
    {
        Stack<Tile> path = new Stack<Tile>();

        Tile previousTile = destination;
        while (previousTile != null)
        {
            previousTile.HasPath();
            path.Push(previousTile);
            previousTile = previousTile.parentNode;
        }

        return path;
    }
    #endregion

    #region Public / Protected methods
    public override void Act(Enemy enemy)
    {
        enemy.nextTile = null;
        enemy.CleanPath();
        BFSearch(enemy);
    }
    #endregion
}