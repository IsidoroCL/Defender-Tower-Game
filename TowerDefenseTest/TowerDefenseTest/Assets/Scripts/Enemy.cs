using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region Fields
    public float speed = 5;
    public int money = 0;
    public StateIA state;
    public Stack<Tile> path = new Stack<Tile>();
    public Tile nextTile;
    public Tile currentTile;

    private float moveProgress;
    private EnemyFactory factory;

    public float Health { get; set; }
    #endregion

    #region Unity methods
    private void Awake()
    {
        moveProgress = 0;
    }

    private void Update()
    {
        if (Health < 0)
        {
            CleanPath();
            currentTile.NoPath();
            factory.Reclaim(this);
            Game.money += money;
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
    private void ReachNextTile()
    {
        moveProgress -= 1f;
        currentTile.NoPath();
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
    #endregion

    #region Public / Protected methods
    public void Configuration(EnemyConfig configuration, EnemyFactory factory)
    {
        this.speed = configuration.speed;
        Health = configuration.health;
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
            moveProgress += Time.deltaTime * (speed / 2);
        }
        else
        {
            moveProgress += Time.deltaTime * speed;
        }
        while (moveProgress >= 1f)
        {
            ReachNextTile();
        }
        transform.localPosition =
            Vector3.LerpUnclamped(currentTile.transform.position, nextTile.transform.position, moveProgress);
    }

    public void CleanPath()
    {
        while (path.Count > 0)
        {
            path.Pop().NoPath();
        }
    }


    #endregion
}