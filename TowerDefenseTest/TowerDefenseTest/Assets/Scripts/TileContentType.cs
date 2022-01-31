using System.Collections.Generic;
using UnityEngine;

public class TileContentType : MonoBehaviour, IHealth
{

    #region Fields
    public TileType type;
    public TileFactory factory;
    public Tile tileContainer;
    public int cost;
    public int influenceZone; //Indicate the distance that allow build.
    public bool isWalkable;

    [SerializeField]
    private float health;

    private bool isBuildable;
    private SpriteRenderer[] sprites;

    public bool IsBuildable
    {
        get
        {
            return isBuildable;
        }
        set
        {
            isBuildable = value;
            if (isBuildable)
            {
                if (tileContainer != null &&
                    tileContainer.IsCrystalOrSpawnNear())
                {
                    isBuildable = false;
                }
                else
                {
                    foreach (SpriteRenderer sprite in sprites)
                    {
                        sprite.color = Color.green;
                    }
                }
            }
            else
            {
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.color = Color.white;
                }
            }
        }
    }

    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health < 0)
            {
                DestroyContent();
            }
        }
    }
    #endregion

    #region Unity methods
    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        isBuildable = false;
    }

    private void Start()
    {
        if (type == TileType.Crystal)
        {
            Game.castles.Add(this);
        }
    }
    #endregion

    #region Private methods
    private void OrderSprite()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = 100 - Mathf.RoundToInt(transform.position.y);
        }
    }

    private HashSet<Tile> InfluenceTiles()
    {
        HashSet<Tile> tilesInfluenced = new HashSet<Tile>();
        List<Tile> tempList = new List<Tile>();
        tilesInfluenced.Add(tileContainer);
        for (int i = 0; i < influenceZone; i++)
        {
            foreach (Tile neighbor in tilesInfluenced)
            {
                tempList.AddRange(Game.GetNeighbor(neighbor));
            }
            foreach (Tile neighborInTempList in tempList)
            {
                tilesInfluenced.Add(neighborInTempList);
            }
        }
        tilesInfluenced.Remove(tileContainer);
        if (type == TileType.Crystal)
        {
            List<Tile> neighborsCastle = Game.GetNeighbor(tileContainer);
            foreach(Tile neighborOfCastle in neighborsCastle)
            {
                tilesInfluenced.Remove(neighborOfCastle);
            }
        }
        return tilesInfluenced;
    }

    private void TilesInfluencedAreBuildable(HashSet<Tile> tilesInfluenced, bool boolValue)
    {
        foreach (Tile tileInfluenced in tilesInfluenced)
        {
            if (tileInfluenced.Content.type == TileType.Plain)
            {
                tileInfluenced.Content.IsBuildable = boolValue;
            }
        }
    }

    private void CallOtherBuildings()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach(GameObject building in buildings)
        {
            if (building == this.gameObject)
            {
                continue;
            }
            TileContentType tileContentType = building.GetComponent<TileContentType>();
            tileContentType.Initialize();
        }
    }
    #endregion

    #region Public / Protected methods
    public void Recycle()
    {
        factory.Recycle(this);
    }

    public void Initialize()
    {
        OrderSprite();
        TilesInfluencedAreBuildable(InfluenceTiles(), true);
    }

    public float GetHealth()
    {
        return health;
    }

    public void DestroyContent()
    {
        IsBuildable = true;
        TilesInfluencedAreBuildable(InfluenceTiles(), false);
        tileContainer.ToggleContent(TileType.Plain);
        CallOtherBuildings();
        if (type == TileType.Crystal)
        {
            Game.castles.Remove(this);
        }
        Game.CheckCastles();
    }
    #endregion
}