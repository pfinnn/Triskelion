using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public Transform target;

    private BubbleHandler bh;

    // Start is called before the first frame update
    void Start()
    {
        bh = GetComponent<BubbleHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bh.HasObstacles())
        {
            Vector3 dir = bh.GetBestDirection(this.transform.position);
            Debug.Log("Agent avoiding obstacles");
        }

        Vector3 targetDirection = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
        transform.position += transform.forward * 3f * Time.deltaTime;
    }


}
