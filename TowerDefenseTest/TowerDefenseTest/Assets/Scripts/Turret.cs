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
            Vector3 dir = target.transform.position - head.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            head.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            float distance = dir.magnitude;
            Vector2 direction = dir / distance;
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
            Debug.Log("Target obtained in " + gameObject.name);
        }
    }
    protected bool TargetInRange()
    {
        if (target == null)
        {
            return false;
        }
        Vector3 a = transform.localPosition;
        Vector3 b = target.transform.position;
        if (Vector3.Distance(a, b) > range)
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