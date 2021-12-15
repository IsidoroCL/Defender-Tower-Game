using System.Collections;
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
        Game.ClearTilesSearch();
        searchingQueue.Clear();
        Tile origin = enemy.currentTile;
        HashSet<Tile> tilesSearched = new HashSet<Tile>();
        tilesSearched.Clear();
        searchingQueue.Enqueue(origin);
        tilesSearched.Add(origin);

        origin.searchFrom = null;

        while (searchingQueue.Count > 0)
        {
            searchingTile = searchingQueue.Dequeue();
            if (searchingTile.Content.type == TileType.Crystal)
            {
                Debug.Log("Crystal found");
                break;
            }

            foreach(Tile neighbor in Game.GetNeighbor(searchingTile))
            {
                if (neighbor.Content.isWalkable &&
                    !tilesSearched.Contains(neighbor))
                {
                    tilesSearched.Add(neighbor);
                    searchingQueue.Enqueue(neighbor);
                    neighbor.searchFrom = searchingTile;
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
            previousTile.Searching();
            path.Push(previousTile);
            previousTile = previousTile.searchFrom;
        }
        Debug.Log("Paht found");

        return path;
    }
    #endregion

    #region Public / Protected methods
    public override void Act(Enemy enemy)
    {
        enemy.nextTile = null;
        BFSSearch(enemy);
    }
    #endregion
}