using UnityEngine;

public class TurretCannon : Turret
{

    #region Fields
    private ObjectPool pool;
    #endregion

    #region Unity methods
    private void Start()
    {
        pool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectPool>();
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
        if (timeFromLastShoot > cooldown)
        {
            timeFromLastShoot = 0;
            GameObject bullet = pool.GetPooledObject() as GameObject;
            bullet.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -2);
            bullet.SetActive(true);
            bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        }
    }
    #endregion
}