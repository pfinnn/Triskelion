using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovementBubble : MonoBehaviour
{
    public Transform target;

    private BubbleHandler bh;

    private Vector3 currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        bh = GetComponent<BubbleHandler>();
        bh.SetTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = target.position - transform.position;
        Vector3 nextDirection = target.position - transform.position;

        if (bh.HasObstacles())
        {
            Debug.Log("Agent avoiding obstacles");

            nextDirection = bh.GetBestDirection(this.transform.position, target.transform.position, nextDirection);

            Debug.DrawLine(transform.position, nextDirection, Color.yellow);

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(nextDirection.x, 0, nextDirection.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
            transform.position += nextDirection.normalized * 3f * Time.deltaTime;
        }
        else
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.green);
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
            transform.position += transform.forward * 3f * Time.deltaTime;
        }

        currentDirection = nextDirection;
    }


}
