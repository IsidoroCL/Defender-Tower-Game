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
    protected float timeFromLastShoot;

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
            float angle = CalculateAngleWithTarget(target.transform);
            Vector2 direction = CalculateDirectionWithTarget(target.transform);
            MoveHead(angle);
            Shoot(angle, direction);
        }
        else
        {
            SearchTarget();
        }
        timeFromLastShoot += Time.deltaTime;
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

    protected float CalculateAngleWithTarget(Transform target)
    {
        Vector3 differencePosition = target.position - transform.position;
        float angle = Mathf.Atan2(differencePosition.y, differencePosition.x) * Mathf.Rad2Deg;
        return angle;
    }

    protected Vector2 CalculateDirectionWithTarget(Transform target)
    {
        Vector3 differencePosition = target.position - transform.position;
        float distance = differencePosition.magnitude;
        Vector2 direction = differencePosition / distance;
        direction.Normalize();
        return direction;
    }
    protected virtual void Shoot(float angle, Vector2 direction)
    {

    }

    protected void MoveHead(float angle)
    {
        head.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    #endregion
}