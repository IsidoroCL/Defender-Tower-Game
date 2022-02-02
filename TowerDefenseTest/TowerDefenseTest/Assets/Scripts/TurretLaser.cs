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
        InitializeLaser();
    }
    private void Update()
    {
        GameUpdate();
    }
    #endregion

    #region Private methods
    private void InitializeLaser()
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(transform.position.x, transform.position.y + 0.42f, -1);
        positions[1] = transform.position;
        laser.positionCount = positions.Length;
        laser.SetPositions(positions);
        laser.enabled = false;
    }
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
        laser.SetPosition(1, target.transform.localPosition + Vector3.back * 3);
        target.Health -= damage;
    }
    #endregion
}