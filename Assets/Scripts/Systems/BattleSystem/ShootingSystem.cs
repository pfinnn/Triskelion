using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;

    // Update is called once per frame
    void Update()
    {
    }

    private Vector3 LocateEstimatedPositionOfTargetWhileMoving(Transform targetPosition, float targetSpeed)
    {
        return Vector3.MoveTowards(targetPosition.position, targetPosition.position, 10f);
        //return targetPosition.position + (targetPosition.forward.normalized * targetSpeed);
    }

    public void LaunchProjectileWithArcMovingTarget(Transform target, float targetSpeed)
    {
        Vector3 Vo = CalculateVelocity(LocateEstimatedPositionOfTargetWhileMoving(target, targetSpeed), transform.position, 2f);
        Vector3 shootingPoint = Vector3.MoveTowards(transform.position, target.position, 5f);
        GameObject obj = Instantiate(projectile, shootingPoint, Quaternion.identity);
        obj.GetComponent<Rigidbody>().velocity = Vo;
    }

    public void LaunchProjectileWithArcStaticTarget(Transform origin, Transform target)
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
