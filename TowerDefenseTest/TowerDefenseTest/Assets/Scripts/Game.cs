using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    #region Fields
    public GridScenario scenario;
    [SerializeField]
    private GameConfig config;
    [SerializeField]
    private EnemyFactory factory;
    private static Game instance;
    private EnemyWave enemies;
    private static Vector2Int sizeGrid;
    private static bool isGameEnd;
    #endregion

    #region Unity methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
        isGameEnd = false;
    }

    private void Start()
    {
        sizeGrid = config.sizeGrid;
        scenario.Init(sizeGrid, config.numberOfMountains, config.numberOfCrystals, config.numberOfSpawns);
        enemies = config.enemies;
        enemies.CurrentGame = this;
        enemies.Init();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                HandleTouchAlt();
            }
            else
            {
                HandleTouch();
            }
            
        }
        if (!isGameEnd) enemies.Progress();
    }
    #endregion

    #region Private methods
    private void HandleTouch()
    {
        GameObject objectTouch = SelectedObjectByMouse();
        if (objectTouch != null &&
            objectTouch.GetComponent<Tile>())
        {
            scenario.ToggleContent(objectTouch.GetComponent<Tile>(), TileType.LaserTurret);
        }
    }

    private void HandleTouchAlt()
    {
        GameObject objectTouch = SelectedObjectByMouse();
        if (objectTouch != null &&
            objectTouch.GetComponent<Tile>())
        {
            scenario.ToggleContent(objectTouch.GetComponent<Tile>(), TileType.CannonTurret);
        }
    }
    #endregion

    #region Public / Protected methods
    public static GameObject SelectedObjectByMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("Objeto pulsado: " + hit.collider.transform.gameObject.name);
            return hit.collider.transform.gameObject;
        }
        return null;
    }

    public void CreateEnemy(EnemyType type)
    {
        Enemy enemy = factory.GetEnemy(type);
        int randomIndex = Random.Range(0, scenario.spawnPoints.Count);
        Vector3 spawnPosition = scenario.spawnPoints[randomIndex].transform.position;
        enemy.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, -2);
        enemy.currentTile = scenario.spawnPoints[randomIndex];
    }

    public static List<Tile> GetNeighbor(Tile tile)
    {
        int x = tile.x;
        int y = tile.y;
        List<Tile> neighbors = new List<Tile>();
        for (int i = 0; i < 2; i++)
        {
            if (x + i < 0 ||
                    x + i >= sizeGrid.x)
            {
                continue;
            }
            for (int j = 0; j < 2; j++)
            {
                if (y + j < 0 ||
                    y + j >= sizeGrid.y)
                {
                    continue;
                }
                if (y == 0 && x == 0)
                {
                    continue;
                }
                neighbors.Add(instance.scenario.map[x + i, y + j]);                
            }
        }
        return neighbors;
    }
    public static void End()
    {
        Debug.Log("You WIN");
        isGameEnd = true;
    }

    public static void GameOver()
    {
        Debug.Log("You LOST");
        isGameEnd = true;
    }
    #endregion
}