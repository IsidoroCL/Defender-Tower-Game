using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region Fields
    public float speed = 5;
    float Health { get; set; }
    private EnemyFactory factory;
    public Stack<Tile> path = new Stack<Tile>();
    public Tile nextTile;
    private Game game;
    [SerializeField]
    public StateIA state;

    public Tile currentTile;

    #endregion

    #region Unity methods
    private void Update()
    {
        if (Health < 0)
        {
            factory.Reclaim(this);
        }
        state.GameUpdate(this);
        /*if (transform.position == nextTile.transform.position)
        {
            currentTile = nextTile;
            nextTile = path.Pop();
        }
        if (nextTile == null)
        {
            Game.GameOver();
        }
        else
        {
            transform.localPosition = Vector3.LerpUnclamped(currentTile.transform.position, nextTile.transform.position, Time.deltaTime);
        }*/

    }
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
    public void Config(float speed, float health, EnemyFactory factory)
    {
        this.speed = speed;
        Health = health;
        this.factory = factory;
    }

    
    #endregion
}