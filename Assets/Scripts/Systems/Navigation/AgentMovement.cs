using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMovement : MonoBehaviour
{
    UnitController uc;

    Vector3 targetPosition;

    NavMeshAgent nma;

    void Awake()
    {
        uc = GetComponentInParent<UnitController>();
        targetPosition = this.transform.position;
        nma = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        nma.SetDestination(targetPosition);
    }

    public void SetTargetDestination(Vector3 _destination)
    {
        nma.SetDestination(_destination);
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
