using UnityEngine;

public class ConfigurationChoose : MonoBehaviour
{

	#region Fields
	public static GameConfig gameConfiguration;
    public static int level;
    #endregion

    #region Unity methods
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        level = 1;
    }
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
    public static void LoadNextScenario()
    {
       gameConfiguration = Resources.Load<GameConfig>("Configs/Scenario" + level);
    }
    #endregion
}