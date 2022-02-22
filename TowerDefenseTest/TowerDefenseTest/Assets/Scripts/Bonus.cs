using UnityEngine;

public class Bonus : MonoBehaviour
{

	#region Fields
	public int addMoney;
	private AudioSource audioSource;
    #endregion

    #region Unity methods
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
    public void Touched()
    {
		Game.money += addMoney;
        AudioSource.PlayClipAtPoint(audioSource.clip, Camera.main.transform.position, 1f);
        gameObject.SetActive(false);
    }
	#endregion
}