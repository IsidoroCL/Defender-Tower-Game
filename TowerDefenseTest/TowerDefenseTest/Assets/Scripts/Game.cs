using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    #region Fields
    public GridScenario scenario;
    [SerializeField]
    private GameConfig config;
    [SerializeField]
    private EnemyFactory factory;
    [SerializeField]
    private CameraControl camera;
    [SerializeField]
    private GameObject textWin;
    [SerializeField]
    private GameObject textLost;
    [SerializeField]
    private GameObject textReplay;
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
        }
        else
        {
            Destroy(this);
        }
        isGameEnd = false;
    }

    private void Start()
    {
        Time.timeScale = 1;
        textWin.SetActive(false);
        textLost.SetActive(false);
        textReplay.SetActive(false);
        sizeGrid = config.sizeGrid;
        scenario.Init(sizeGrid, config.numberOfMountains, config.numberOfForests, config.numberOfCrystals, config.numberOfSpawns);
        camera.Init(sizeGrid.x);
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (!isGameEnd) enemies.Progress();
        else CheckEnemies();
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

    private void CheckEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if ( enemies.Length < 1)
        {
            textWin.SetActive(true);
            textReplay.SetActive(true);
            Time.timeScale = 0;
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

        //I addded this code because I have a rare error sometimes where spawnPoint is null
        if (scenario.spawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, scenario.spawnPoints.Count);
            Vector3 spawnPosition = scenario.spawnPoints[randomIndex].transform.position;
            enemy.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, -2);
            enemy.currentTile = scenario.spawnPoints[randomIndex];
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static List<Tile> GetNeighbor(Tile tile)
    {
        int x = tile.x;
        int y = tile.y;
        List<Tile> neighbors = new List<Tile>();
        if (x - 1 >= 0) neighbors.Add(instance.scenario.map[x - 1, y]);
        if (x + 1 < sizeGrid.x) neighbors.Add(instance.scenario.map[x + 1, y]);
        if (y - 1 >= 0) neighbors.Add(instance.scenario.map[x, y - 1]);
        if (y + 1 < sizeGrid.y) neighbors.Add(instance.scenario.map[x, y + 1]);
        
        return neighbors;
    }

    public static void ClearTilesSearch()
    {
        instance.scenario.ClearAllsearchFrom();
    }
    public static void End()
    {
        isGameEnd = true;
    }

    public static void GameOver()
    {
        instance.textLost.SetActive(true);
        instance.textReplay.SetActive(true);
        isGameEnd = true;
        Time.timeScale = 0;
    }
    #endregion
}