using UnityEngine;

public class BarControl : MonoBehaviour
{

    #region Fields
    private IHealth creature;
    private SpriteRenderer bar;
    private float totalHealth;
    #endregion

    #region Unity methods
    private void Awake()
    {
        creature = GetComponentInParent<IHealth>();
        bar = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        totalHealth = creature.GetHealth();
    }

    private void Update()
    {
        float relation = (float)creature.GetHealth() / totalHealth;
        transform.localScale = new Vector3(relation, 0.1f, 1.0f);
    }
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods

    #endregion
}