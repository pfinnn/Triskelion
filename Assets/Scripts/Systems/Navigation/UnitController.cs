using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    Transform target;    
    
    Vector3 targetPosition;

    //List<Formations> formations;

    [SerializeField]
    int soldiersAmount;

    [SerializeField]
    int soldiersPerRow;

    [SerializeField]
    GameObject unitPrefab; // TODO, use different start animatin states on spawn, dont use sperate prefabs you maniac

    List<GameObject> soldiers;

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
        currentState = State.Idle;

        if (target == null)
        {
            targetPosition = this.transform.position;
        }
        else
        {
            targetPosition = target.position;
        }

        soldiers = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnUnitsInFormation();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                Debug.Log(this.name + " State: Idle");
                IdlePositions();
                break;
            case State.Attacking:
                Debug.Log(this.name+" State: Attacking");
                break;
            case State.Moving:
                Debug.Log(this.name + " State: Moving");
                CalculateNewTargetPosition();
                break;
            case State.Fleeing:
                Debug.Log(this.name + " State: Fleeing");
                break;
            case State.Death:
                Debug.Log(this.name + " State: Death");
                break;
        }


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

    internal void CalculateNewTargetPosition()
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

    internal void DetermineNextTarget()
    {

    }

}
