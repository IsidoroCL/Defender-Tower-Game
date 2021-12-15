using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : Turret
{

    #region Fields
    LineRenderer laser;
    [SerializeField]
    float damage;
    #endregion

    #region Unity methods
    private void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = transform.position + Vector3.back;
        positions[1] = transform.position;
        laser.positionCount = positions.Length;
        laser.SetPositions(positions);
        laser.enabled = false;
    }
    private void Update()
    {
        GameUpdate();
    }
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
    protected override void SearchTarget()
    {
        laser.enabled = false;
        base.SearchTarget();
    }

    protected override void Shoot(float angle, Vector2 direction)
    {
        laser.enabled = true;
        laser.SetPosition(1, target.transform.localPosition + Vector3.back);
        target.Health -= damage;
    }
    #endregion
}