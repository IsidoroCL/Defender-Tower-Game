using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region Fields
    public float speed = 5;
    public float Health { get; set; }
    public Stack<Tile> path = new Stack<Tile>();
    public Tile nextTile;
    public StateIA state;
    public Tile currentTile;

    private float progress;
    private EnemyFactory factory;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
            Health -= 10;
        }
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
        if (currentTile.Content.type == TileType.Forest)
        {
            progress += Time.deltaTime * (speed / 2);
        }
        else
        {
            progress += Time.deltaTime * speed;
        }
        
        while (progress >= 1f)
        {
            progress -= 1f;
            currentTile = nextTile;
            if (currentTile.Content.type == TileType.Crystal)
            {
                Game.GameOver();
            }
            else
            {
                if (path.Count > 0) nextTile = path.Pop();
            }
        }
        transform.localPosition =
            Vector3.LerpUnclamped(currentTile.transform.position, nextTile.transform.position, progress);
    }

    
    #endregion
}