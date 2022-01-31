using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject
{

    #region Fields
    [Range(1, 10)]
    public float speed;
    [Range(5, 100)]
    public int health;
    public int attack;
    public int money;
    public bool fly;
    public bool attackBuilding;
    public Enemy prefab;
    #endregion

    #region Unity methods

    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods

    #endregion
}