using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{

    #region Fields
    private float speed;
    private int money;
    private bool canFly;
    private bool canAttackBuilding;

    public StateIA state;
    public Stack<Tile> path = new Stack<Tile>();
    [HideInInspector]
    public Tile nextTile;
    [HideInInspector]
    public Tile currentTile;

    [SerializeField]
    private AudioClip deathSound;

    private float moveProgress;
    private float timeFromLastAttack;
    private EnemyFactory factory;
    private SpriteRenderer[] sprites;

    public float Health { get; set; }
    public int Attack { get; set; }
    public bool CanFly { get { return canFly;} }
    public bool CanAttackBuilding { get { return canAttackBuilding; } }
    #endregion

    #region Unity methods
    private void Awake()
    {
        moveProgress = 0;
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Health < 0)
        {
            Death();
        }
        state.GameUpdate(this);
        timeFromLastAttack += Time.deltaTime;
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = 100 - Mathf.FloorToInt(transform.position.y);
        }
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

    private void Death()
    {
        CleanPath();
        currentTile.NoPath();
        factory.Reclaim(this);
        Game.money += money;
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, 1f);
    }
    #endregion

    #region Public / Protected methods
    public void Configuration(EnemyConfig configuration, EnemyFactory factory)
    {
        this.speed = configuration.speed;
        Health = configuration.health;
        money = configuration.money;
        Attack = configuration.attack;
        canFly = configuration.fly;
        canAttackBuilding = configuration.attackBuilding;
        this.factory = factory;
    }

    public void MoveEnemy()
    {
        if (nextTile == null &&
            path.Count > 0)
        {
            nextTile = path.Pop();
        }
        if (currentTile.Content.type == TileType.Forest &&
            !CanFly)
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

    public float GetHealth()
    {
        return Health;
    }

    public void AttackBuilding(Tile target)
    {
        if (timeFromLastAttack > 1)
        {
            timeFromLastAttack = 0;
            target.Content.Health -= Attack;
            if (target.Content.Health <= 0)
            {
                target.Content.DestroyContent();
            }
        }
    }


    #endregion
}