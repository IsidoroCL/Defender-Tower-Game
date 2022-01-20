using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    #region Fields
    public GridScenario gridScenario;
    [SerializeField]
    private GameConfig gameConfiguration;
    [SerializeField]
    private EnemyFactory enemyFactory;
    [SerializeField]
    private CameraControl cameraControl;
    [SerializeField]
    private GameObject textWin;
    [SerializeField]
    private GameObject textLost;
    [SerializeField]
    private TextMeshProUGUI textMoney;
    [SerializeField]
    private GameObject textReplay;
    private static Game instance;
    private EnemyWave enemies;
    private static Vector2Int sizeGridScenario;
    private static bool isGameEnd;

    public static int money;
    public static TileType tileTypeSelected;
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

        tileTypeSelected = TileType.CannonTurret;
    }

    private void Start()
    {
        Time.timeScale = 1;
        textWin.SetActive(false);
        textLost.SetActive(false);
        textReplay.SetActive(false);
        sizeGridScenario = gameConfiguration.sizeGrid;
        gridScenario.Initialize(gameConfiguration);
        cameraControl.Initialize(sizeGridScenario.x);
        money = gameConfiguration.startMoney;
        enemies = gameConfiguration.enemies;
        enemies.CurrentGame = this;
        enemies.Initialize();
    }

    private void Update()
    {
        textMoney.text = money.ToString();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (!isGameEnd)
            enemies.Progress();
        else
            CheckNumberOfEnemies();
    }
    #endregion

    #region Private methods

    private void CheckNumberOfEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length < 1)
        {
            Win();
        }
    }

    private void Win()
    {
        textWin.SetActive(true);
        textReplay.SetActive(true);
        Time.timeScale = 0;
    }
    #endregion

    #region Public / Protected methods

    public void CreateEnemy(EnemyType type)
    {
        Enemy enemy = enemyFactory.GetEnemy(type);

        //I addded this IF code because I have a rare error sometimes where spawnPoint is null
        if (gridScenario.spawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, gridScenario.spawnPoints.Count);
            Vector3 spawnPosition = gridScenario.spawnPoints[randomIndex].transform.position;
            enemy.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, -2);
            enemy.currentTile = gridScenario.spawnPoints[randomIndex];
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static List<Tile> GetNeighbor(Tile tile)
    {
        return instance.gridScenario.GetNeighbor(tile);
    }

    public static void ClearTilesForNextPathFinding()
    {
        instance.gridScenario.ClearTilesForNextPathFinding();
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