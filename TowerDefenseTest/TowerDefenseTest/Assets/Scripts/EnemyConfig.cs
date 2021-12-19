using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject
{

    #region Fields
    [Range(1, 10)]
    public float speed;
    [Range(10, 100)]
    public int health;
    public Enemy prefab;
    #endregion

    #region Unity methods

    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods

    #endregion
}