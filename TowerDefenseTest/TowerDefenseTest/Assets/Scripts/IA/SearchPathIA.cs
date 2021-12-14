using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchPathIA", menuName = "IA/SearchPathIA")]
public class SearchPathIA : Action
{
    #region Fields
    private Queue<Tile> searchingQueue = new Queue<Tile>();
    private List<Tile> tilesSearched = new List<Tile>();
    private Tile searchingTile;
    #endregion

    #region Unity methods

    #endregion

    #region Private methods
    private void BFSSearch(Enemy enemy)
    {
        Tile origin = enemy.currentTile;
        searchingQueue.Enqueue(origin);
        tilesSearched.Clear();
        while (searchingQueue.Count > 0)
        {
            searchingTile = searchingQueue.Dequeue();
            tilesSearched.Add(searchingTile);
            if (searchingTile.Content.type == TileType.Crystal)
            {
                Debug.Log("Crystal found");
                enemy.path = CreatePath(searchingTile);
                break;
            }
            else
            {
                List<Tile> neighbors = Game.GetNeighbor(searchingTile);
                foreach(Tile neighbor in neighbors)
                {
                    if (neighbor.Content.isWalkable &&
                        !searchingQueue.Contains(neighbor) &&
                        !tilesSearched.Contains(neighbor))
                    {
                        searchingQueue.Enqueue(neighbor);
                        neighbor.searchFrom = searchingTile;
                    }
                }
            }
        }
    }

    public Stack<Tile> CreatePath(Tile destination)
    {
        Stack<Tile> path = new Stack<Tile>();
        path.Push(destination);
        Tile previousTile = destination.searchFrom;
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
        BFSSearch(enemy);
    }
    #endregion
}