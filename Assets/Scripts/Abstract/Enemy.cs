using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Damageable
{

    enum State
    {
        Attacking,
        Moving,
        Dying
    }

    State currentState = State.Moving;

    // Enemies choose Targets only if they are close and in direction if Triskelion
    // if there is no target, current target will always be Triskelion
    Transform target;

    float movementSpeedScalar = 5f;

    // how far the targets can be away to attack
    float attackRange = 5f;

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            // play death animation
            // play death sound
            // notify wave manager, handles destruction
        }

        // Chose Target

        // In Target Range ?
        if (IsTargetInRange())
        {
            currentState = State.Attacking;
        }

        if (currentState == State.Moving)
        {
            MoveToTarget();
        }

    }

    public void SetTarget(Transform _Target)
    {
        target = _Target;
    }

    bool IsTargetInRange()
    {
        return attackRange >= Vector3.Distance(transform.position, target.position);
    }

    void ChooseTarget()
    {
        // Target in Range?
        
        // Change current Target if closer
    }

    void calculateOrientationTerrainBased()
    {
        // Estimate future position by orientation + some distance

        // Get terrain height

        // calculate new orientation
    }

    // MoveToTarget should be triggered each update
    void MoveToTarget()
    {
        //agent.destination = target.position;


        //// Rotate towards Target
        //Vector3 lookVector = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(lookVector); // rotate towards world center
        //float str = 0.5f;
        //str = Mathf.Min(movementSpeedScalar * Time.deltaTime, 1);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, str);
        //// Transform towards Target

        //Vector3 forwardVector = transform.forward;
        //Vector3 extraDist = new Vector3(1, 0, 1);
        //forwardVector = forwardVector + extraDist;


        //transform.position = Vector3.MoveTowards(transform.position, target.position, str);
    }

    void AttackTarget()
    {
        // Play Attack Animation
        // dice if hit
        // Notify target when hit
    }

}
