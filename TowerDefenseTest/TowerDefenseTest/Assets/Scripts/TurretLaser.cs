using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : Turret
{

    #region Fields
    LineRenderer laser;
    #endregion

    #region Unity methods
    private void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = transform.position;
        positions[1] = transform.position;
        laser.positionCount = positions.Length;
        laser.SetPositions(positions);
    }
    private void Update()
    {
        GameUpdate();
    }
    #endregion

    #region Private methods

    #endregion

    #region Public / Protected methods
    protected override void Shoot(float angle, Vector2 direction)
    {
        laser.SetPosition(1, target.transform.localPosition);
    }
    #endregion
}