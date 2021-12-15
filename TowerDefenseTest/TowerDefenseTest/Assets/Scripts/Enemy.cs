using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region Fields
    public float speed = 5;
    float Health { get; set; }
    private float progress;
    private EnemyFactory factory;
    public Stack<Tile> path = new Stack<Tile>();
    public Tile nextTile;
    private Game game;
    [SerializeField]
    public StateIA state;

    public Tile currentTile;

    #endregion

    #region Unity methods
    private void Awake()
    {
        progress = 0;
    }

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

    public void MoveEnemy()
    {
        if (nextTile == null &&
            path.Count > 0)
        {
            nextTile = path.Pop();
        }
        progress += Time.deltaTime * speed;
        while (progress >= 1f)
        {
            progress -= 1f;
            currentTile = nextTile;
            nextTile = path.Pop();
        }
        transform.localPosition =
            Vector3.LerpUnclamped(currentTile.transform.position, nextTile.transform.position, progress);
    }

    
    #endregion
}