using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMovement : MonoBehaviour
{
    Vector3 targetPosition;

    NavMeshAgent nma;

    void Awake()
    {
        targetPosition = this.transform.position;
        nma = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // set layer cost for water and ground

        nma.SetDestination(targetPosition);
    }

    public void SetTargetDestination(Vector3 _destination)
    {
        if (nma.isStopped)
        {
            nma.isStopped = false;
        }

        nma.SetDestination(_destination);
    }

    public void StopAgent()
    {
        //nma.velocity = Vector3.zero; // no sliding
        nma.isStopped = true; 
    }

    public void StartAgent()
    {
        nma.isStopped = false;
    }

    public bool IsMoving()
    {
        return !nma.isStopped;
    }

    public bool ReachedDestination(Vector3 destination)
    {
        return Vector3.Distance(this.transform.position, destination) <= 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    internal Quaternion GetCurrentRotation()
    {
        return transform.rotation;
    }
}
