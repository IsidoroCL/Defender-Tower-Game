using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "EnemyWave")]
public class EnemyWave : ScriptableObject
{
    #region Auxiliar Class
    [System.Serializable]
    class Wave
    {
        public EnemyType type;
        public int numberOfEnemies;
        [Range(0.5f, 5f)]
        public float frequencyBetweenEnemies;
    }
    #endregion
    #region Fields
    [SerializeField]
    Wave[] waves;
    private float timeProgress;
    private int currentWave;
    private int numberEnemiesSpawned;

    public Game CurrentGame { get; set; }
    #endregion

    #region Unity methods

    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
    public void Initialize()
    {
        currentWave = 0;
        timeProgress = 0;
    }

    public void Progress()
    {
        timeProgress += Time.deltaTime;
        if (timeProgress >= waves[currentWave].frequencyBetweenEnemies)
        {
            timeProgress -= waves[currentWave].frequencyBetweenEnemies;
            CurrentGame.CreateEnemy(waves[currentWave].type);
            numberEnemiesSpawned++;
            if (numberEnemiesSpawned >= waves[currentWave].numberOfEnemies)
            {
                currentWave++;
                numberEnemiesSpawned = 0;
                if (currentWave >= waves.Length)
                {
                    Game.End();
                }
            }
        }
    }
    #endregion
}