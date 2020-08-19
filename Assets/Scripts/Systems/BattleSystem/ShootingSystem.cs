using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;

    private Vector3 shootingPoint;

    Vector3 DEBUG_ORIGIN;
    Vector3 DEBUG_TARGET;

    private void Start()
    {
        shootingPoint = new Vector3(this.transform.position.x, this.transform.position.y+6, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(DEBUG_ORIGIN, DEBUG_TARGET, Color.red);
    }

    internal void ShootSoldierInUnit(Transform soldier, float soldierSpeed)
    {        
        DEBUG_ORIGIN = shootingPoint;
        DEBUG_TARGET = soldier.position;

        soldier.GetComponent<Soldier>().DealDamage(100);
    }

    private Vector3 EstimatePosMovingTarget(Transform target, float targetSpeed)
    {
        //return Vector3.MoveTowards(targetPosition.position, targetPosition.position, 10f);
        return target.position + (target.forward.normalized * targetSpeed);
    }

    internal void ShootArcMovingTarget(Transform target, float targetSpeed)
    {
        Vector3 Vo = CalculateVelocity(EstimatePosMovingTarget(target, targetSpeed), transform.position, 2f);
        Vector3 shootingPoint = Vector3.MoveTowards(transform.position, target.position, 5f);
        GameObject obj = Instantiate(projectile, shootingPoint, Quaternion.identity);
        obj.GetComponent<Rigidbody>().velocity = Vo;
    }

    internal void ShootArcStaticTarget(Transform origin, Transform target)
    {
        Vector3 Vo = CalculateVelocity(target.position, origin.position, 5f);
        Vector3 shootingPoint = origin.position + new Vector3 (0,1,0);
        GameObject obj = Instantiate(projectile, shootingPoint, Quaternion.identity);
        obj.GetComponent<Rigidbody>().velocity = Vo;
    }

    private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0.0f;

        float Vxz = distanceXZ.magnitude / time;
        float Vy = distance.y / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized * Vxz;
        result.y = Vy;

        return result;
    }
}
