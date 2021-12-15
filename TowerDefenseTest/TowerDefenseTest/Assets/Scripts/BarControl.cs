using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour
{

	#region Fields
	private Enemy enemy;
	private SpriteRenderer bar;
    private float totalHealth;
    #endregion

    #region Unity methods
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        bar = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        totalHealth = enemy.Health;
    }

    private void Update()
    {
        float relation = (float) enemy.Health / totalHealth;
        transform.localScale = new Vector3(relation, 0.1f, 1.0f);
    }
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods

    #endregion
}