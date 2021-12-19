using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchPathIA", menuName = "IA/SearchPathIA")]
public class SearchPathIA : Action
{
    #region Fields
    private Queue<Tile> searchingQueue = new Queue<Tile>();
    private Tile searchingTile;
    #endregion

    #region Unity methods

    #endregion

    #region Private methods
    private void BFSSearch(Enemy enemy)
    {
        Game.ClearTilesForNextPathFinding();
        searchingQueue.Clear();
        Tile origin = enemy.currentTile;

        HashSet<Tile> tilesAlreadySearched = new HashSet<Tile>();
        tilesAlreadySearched.Clear();

        searchingQueue.Enqueue(origin);
        tilesAlreadySearched.Add(origin);

        origin.parentNode = null;

        //The queue is filled with the neighbor of the 
        while (searchingQueue.Count > 0)
        {
            searchingTile = searchingQueue.Dequeue();
            if (searchingTile.Content.type == TileType.Crystal)
            {
                Debug.Log("Crystal found");
                break;
            }

            foreach (Tile neighbor in Game.GetNeighbor(searchingTile))
            {
                if (neighbor.Content.isWalkable &&
                    !tilesAlreadySearched.Contains(neighbor))
                {
                    tilesAlreadySearched.Add(neighbor);
                    searchingQueue.Enqueue(neighbor);
                    neighbor.parentNode = searchingTile;
                }
            }

        }
        enemy.path = CreatePath(searchingTile);
    }

    public Stack<Tile> CreatePath(Tile destination)
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
        BFSSearch(enemy);
    }
    #endregion
}