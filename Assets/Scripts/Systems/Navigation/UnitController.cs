using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class UnitController : MonoBehaviour
{
   
    Vector3 targetPosition;
    Vector3 triskelionPosition; // remember triskelion position to always be able to go back there without searching for the GO

    //List<Formations> formations;

    [SerializeField]
    int soldiersAmount;

    [SerializeField]
    int soldiersPerRow;

    [SerializeField]
    GameObject unitPrefab; // TODO, use different start animatin states on spawn, dont use sperate prefabs you maniac

    List<GameObject> soldiers;

    List<GameObject> targetsInRange;


    public enum State
    {
        Idle,
        Moving,
        Attacking,
        Fleeing,
        Death,
    }

    public State currentState;

    private void Awake()
    {
        currentState = State.Moving;

        soldiers = new List<GameObject>();
        targetsInRange = new List<GameObject>();

        targetPosition = GameObject.Find("Triskelion").transform.position;
        triskelionPosition = targetPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnUnitsInFormation();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineCurrentState();

        switch (currentState)
        {
            case State.Idle:
                Debug.Log(this.name + " State: Idle");
                IdlePositions();
                break;
            case State.Attacking:
                Debug.Log(this.name + " State: Attacking");
                IdlePositions();
                break;
            case State.Moving:
                Debug.Log(this.name + " State: Moving");
                SetAgentsDestinations();
                break;
            case State.Fleeing:
                Debug.Log(this.name + " State: Fleeing");
                // run back to spawn area where unit will be deleted
                break;
            case State.Death:
                Debug.Log(this.name + " State: Death");
                // Play animations
                break;
        }


    }

    private bool CanAttackTarget()
    {
        float attackRange = 3f;
        return Vector3.Distance(this.transform.position, targetPosition) <= attackRange;
    }

    private void DetermineCurrentState()
    {
        
        State _state = currentState;


        
        DetermineNextTarget();

        if (CanAttackTarget())
        {
            _state = State.Attacking;
        }

        // only overwrite if state is not equal
        if (_state != currentState)
        {
            currentState = _state;
        }
    }

    internal void DetermineNextTarget()
    {
        // eventually choose with probability and weight so the behavior is unpredictable but not random

        if (targetsInRange.Count <= 0)
        {
            targetPosition = triskelionPosition;
        }else
        {
            GameObject possibleTarget = targetsInRange[0];
            targetPosition = possibleTarget.transform.position;
        }


        // also change target when under attack ?

    }

    private void IdlePositions()
    {
        for (int i = 0; i < soldiersAmount; i++)
        {
            //Quaternion currentRotation = soldiers[i].GetComponent<AgentMovement>().GetCurrentRotation();
            Vector3 currentPosition = soldiers[i].GetComponent<AgentMovement>().GetCurrentPosition();
            soldiers[i].GetComponent<AgentMovement>().SetTargetDestination(currentPosition);
        }
    }

    void SpawnUnitsInFormation()
    {
        Vector3 center = this.transform.position;

        int rows = Mathf.RoundToInt(soldiersAmount / soldiersPerRow);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                Vector3 soldierPos = center + new Vector3(i, 0, j);
                GameObject soldier = Instantiate(unitPrefab, soldierPos, transform.rotation);
                soldier.transform.SetParent(this.transform);
                soldiers.Add(soldier);
            }
        }
    }

    internal Vector3 CalculateCenter()
    {
        Vector3 total = Vector3.zero;
        foreach (GameObject s in soldiers)
        {
            total +=  s.transform.position;
        }

        return total/soldiers.Count;
    }

    internal void SetAgentsDestinations()
    {
        Vector3 center = CalculateCenter();

        for (int i = 0; i < soldiersAmount; i++)
        {
            Vector3 relativeToCenter = soldiers[i].GetComponent<AgentMovement>().GetCurrentPosition() - center;
            Vector3 agentDirection = targetPosition + relativeToCenter;
            Debug.DrawLine(soldiers[i].transform.position, agentDirection);
            soldiers[i].GetComponent<AgentMovement>().SetTargetDestination(agentDirection);
        }

    }

    internal void SetTarget(Vector3 _targetPosition)
    {
        targetPosition = _targetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " has entered Unit Trigger " + this.name);
        targetsInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        targetsInRange.Remove(other.gameObject);
    }

}
