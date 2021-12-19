using UnityEngine;

public class Turret : MonoBehaviour
{

    #region Fields
    protected Enemy target;

    [SerializeField, Range(1, 10)]
    protected float range;
    [SerializeField]
    protected float cooldown;
    [SerializeField]
    protected float bulletSpeed;
    protected float lastShoot;

    [SerializeField]
    protected Transform head;
    #endregion

    #region Unity methods
    private void Update()
    {
        GameUpdate();
    }

    #endregion

    #region Private methods



    #endregion

    #region Public / Protected methods
    protected void GameUpdate()
    {
        if (TargetInRange())
        {
            //Calculate the angle between the turret and the target to get the direction of the bullet
            //Then, move the head to the enemy
            //With this sprites, I don't activate the head, it is desactivate
            Vector3 differencePosition = target.transform.position - head.transform.position;
            float angle = Mathf.Atan2(differencePosition.y, differencePosition.x) * Mathf.Rad2Deg;
            head.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            float distance = differencePosition.magnitude;
            Vector2 direction = differencePosition / distance;
            direction.Normalize();
            Shoot(angle, direction);
        }
        else
        {
            SearchTarget();
        }
        lastShoot += Time.deltaTime;
    }

    protected virtual void SearchTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, range, 1 << 6);
        if (targets.Length > 0)
        {
            target = targets[0].GetComponent<Enemy>();
        }
    }
    protected bool TargetInRange()
    {
        if (target == null)
        {
            return false;
        }
        if (Vector3.Distance(transform.localPosition, target.transform.position) > range)
        {
            target = null;
            return false;
        }
        return true;
    }
    protected virtual void Shoot(float angle, Vector2 direction)
    {

    }
    #endregion
}