using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "EnemyWave")]
public class EnemyWave : ScriptableObject
{
    #region Auxiliar Class
    [System.Serializable]
	class Wave
	{
		public EnemyType type;
		public int number;
		[Range(0.5f, 5f)]
		public float frequency;
		public float cooldown;
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
	public void Init()
    {
		currentWave = 0;
		timeProgress = 0;
    }

	public void Progress()
    {
		timeProgress += Time.deltaTime;
		if (timeProgress >= waves[currentWave].frequency)
        {
			timeProgress -= waves[currentWave].frequency;
			CurrentGame.CreateEnemy(waves[currentWave].type);
			numberEnemiesSpawned++;
			if (numberEnemiesSpawned >= waves[currentWave].number)
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