using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform shootingPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Vector3 LocateEstimatedPositionOfTargetWhileMoving(Transform targetPosition, float targetSpeed)
    {
        return targetPosition.position + (targetPosition.forward.normalized * targetSpeed);
    }

    public void LaunchProjectileWithArc(Transform target, float speed)
    {
        Vector3 Vo = CalculateVelocity(LocateEstimatedPositionOfTargetWhileMoving(target, speed), transform.position, 2f);
        GameObject obj = Instantiate(projectile, shootingPoint.position, Quaternion.identity);
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
