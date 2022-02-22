using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

	#region Fields
	public GameObject bigSurvivalButton;
	public GameObject mediumSurvivalButton;
	public GameObject smallSurvivalButton;

    private static StartMenu instance;
    public static GameConfig gameConfiguration;
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
    }
    #endregion

    #region Private methods
    private void SetButtonsActives()
    {
        bigSurvivalButton.SetActive(true);
        mediumSurvivalButton.SetActive(true);
        smallSurvivalButton.SetActive(true);
    }

    private void SetButtonsDeactives()
    {
        bigSurvivalButton.SetActive(false);
        mediumSurvivalButton.SetActive(false);
        smallSurvivalButton.SetActive(false);
    }

    private static void NewGame()
    {
        instance.SetButtonsDeactives();
        TransitionManager.Instance.LoadScene("Scenario01");
        //SceneManager.LoadScene("Scenario01", LoadSceneMode.Single);
    }
	#endregion
	
	#region Public / Protected methods
	public static void StartClicked()
    {
        ConfigurationChoose.gameConfiguration = Resources.Load<GameConfig>("Configs/Scenario1");
        NewGame();
    }

	public static void SurvivalClicked()
    {
		instance.SetButtonsActives();
    }

	public static void HowClicked()
    {
        instance.SetButtonsDeactives();
    }

	public static void ExitClicked()
    {
		Application.Quit();
    }

    public static void SmallSurvivalClicked()
    {
        ConfigurationChoose.gameConfiguration = Resources.Load<GameConfig>("Configs/SurvivalSmall");
        NewGame();
    }

    public static void MediumSurvivalClicked()
    {
        ConfigurationChoose.gameConfiguration = Resources.Load<GameConfig>("Configs/SurvivalMedium");
        NewGame();
    }

    public static void BigSurvivalClicked()
    {
        ConfigurationChoose.gameConfiguration = Resources.Load<GameConfig>("Configs/SurvivalBig");
        NewGame();
    }
	#endregion
}