using UnityEngine;

public class DisableOnTime : MonoBehaviour
{

    #region Fields
    private float timeSinceObjectIsActive;
    [SerializeField]
    float limitTime;
    #endregion

    #region Unity methods
    private void Update()
    {
        timeSinceObjectIsActive += Time.deltaTime;
        if (timeSinceObjectIsActive > limitTime)
        {
            timeSinceObjectIsActive = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        timeSinceObjectIsActive = 0;
    }
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods

    #endregion
}