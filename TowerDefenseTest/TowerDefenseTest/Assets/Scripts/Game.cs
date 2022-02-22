using System.Collections;
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
    private TextMeshProUGUI textTime;
    [SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private TextMeshProUGUI textRecord;
    [SerializeField]
    private GameObject textReplay;
    private EnemyWave enemies;

    private static Game instance;
    private static Vector2Int sizeGridScenario;
    private static bool isGameEnd;
    private static bool isSurvival;
    private bool stopCheckNumberOfEnemies;
    private static float timeOfSurvivalGame;

    public static int money;
    public static TileType tileTypeSelected;
    public static HashSet<TileContentType> castles;
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
            Destroy(this.gameObject);
        }
        isGameEnd = false;
        castles = new HashSet<TileContentType>();
        tileTypeSelected = TileType.CannonTurret;
        stopCheckNumberOfEnemies = false;
    }

    private void Start()
    {
        if (ConfigurationChoose.gameConfiguration != null)
        {
            gameConfiguration = ConfigurationChoose.gameConfiguration;
        }
        NewGame();
    }

    private void Update()
    {
        textMoney.text = money.ToString();
        if (isSurvival)
        {
            timeOfSurvivalGame += Time.deltaTime;
            textTime.text = Mathf.FloorToInt(timeOfSurvivalGame).ToString();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }
        if (!isGameEnd)
            enemies.Progress();
        else if (!stopCheckNumberOfEnemies)
            CheckNumberOfEnemies();
    }
    #endregion

    #region Private methods

    private void NewGame()
    {
        Time.timeScale = 1;
        textWin.SetActive(false);
        textLost.SetActive(false);
        textReplay.SetActive(false);
        textScore.gameObject.SetActive(false);
        textRecord.gameObject.SetActive(false);
        sizeGridScenario = gameConfiguration.sizeGrid;
        gridScenario.Initialize(gameConfiguration);
        cameraControl.Initialize(sizeGridScenario.x);
        money = gameConfiguration.startMoney;
        isSurvival = gameConfiguration.survivalMode;
        enemies = gameConfiguration.enemies;
        enemies.CurrentGame = this;
        enemies.Initialize();

        if (isSurvival)
        {
            textTime.gameObject.SetActive(true);
            timeOfSurvivalGame = 0;
        }
    }

    private void CheckNumberOfEnemies()
    {
        GameObject[] enemiesInGame = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemiesInGame.Length < 1 && !isSurvival)
        {
            stopCheckNumberOfEnemies = true;
            Win();
        }
        else if (enemiesInGame.Length < 1 && isSurvival)
        {
            enemies.Initialize();
            isGameEnd = false;
        }
    }

    private void Win()
    {
        textWin.SetActive(true);
        ConfigurationChoose.level++;
        StartCoroutine(NextGame());
    }

    private void CheckRecord(int score)
    {
        int currentRecord;
        string recordKey;
        switch (sizeGridScenario.x)
        {
            case 10: //Small
                recordKey = "RecordSmall";
                break;
            case 15: //Medium
                recordKey = "RecordMedium";
                break;
            case 20: //Big
                recordKey = "RecordBig";
                break;
            default:
                recordKey = "RecordSmall";
                break;
        }
        currentRecord = PlayerPrefs.GetInt(recordKey, 0);
        if (score > currentRecord)
        {
            PlayerPrefs.SetInt(recordKey, score);
        }
        textRecord.gameObject.SetActive(true);
        textRecord.text += PlayerPrefs.GetInt(recordKey, 0);
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSecondsRealtime(3);
        ConfigurationChoose.LoadNextScenario();
        TransitionManager.Instance.ReLoadScene();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    
    public static void CheckCastles()
    {
        if (castles.Count < 1)
        {
            GameOver();
        }
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
        if (isSurvival)
        {
            instance.textScore.gameObject.SetActive(true);
            instance.textScore.text += " " + Mathf.FloorToInt(timeOfSurvivalGame).ToString();
            instance.CheckRecord(Mathf.FloorToInt(timeOfSurvivalGame));
        }
    }
    #endregion
}