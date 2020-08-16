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
    int rows;

    [SerializeField]
    int soldiersPerRow;

    [SerializeField]
    GameObject unitPrefab; // TODO, use different start animatin states on spawn, dont use sperate prefabs you maniac

    List<GameObject> soldiers;

    List<GameObject> targetsInRange;
    float attackRange = 15f;

    GameObject[,] formationGrid;
    bool[,] soldierInFormation;
    Vector3 formationStep;
    Vector3 formationCenter;
    float marginFormation = 0.5f;
    float StepSizeFormation = 15f;

    float timer_Formation = 0f;
    float timestamp_Formation = 0f;
    float waitInFormationIntervall = 10f;

    public enum State_Unit
    {
        Idle,
        Moving,
        Attacking,
        Fleeing,
        Death,
    }

    public State_Unit STATE_UNIT;

    public enum State_Formation
    {
         Grid,
         Loose,
    }

    public State_Formation STATE_FORMATION;

    public enum State_Movement
    {
        Wait,
        Moving,
    }

    public State_Movement STATE_MOVEMENT;


    private void Awake()
    {
        STATE_UNIT = State_Unit.Moving;
        STATE_FORMATION = State_Formation.Grid;
        STATE_MOVEMENT  = State_Movement.Moving;
        soldiers = new List<GameObject>();
        targetsInRange = new List<GameObject>();
        rows = Mathf.RoundToInt(soldiersAmount / soldiersPerRow);
        formationGrid = new GameObject[rows, soldiersPerRow];
        soldierInFormation = new bool[rows, soldiersPerRow];

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
        // Update all necessary values in one place and not inside functions

        DetermineCurrentState();

        switch (STATE_UNIT)
        {
            case State_Unit.Idle:
                IdlePositions();
                break;
            case State_Unit.Attacking:
                IdlePositions();
                // play attack animations 
                break;
            case State_Unit.Moving:
                HandleMovement();
                //MoveWithoutFormation();
                break;
            case State_Unit.Fleeing:
                // run back to spawn area where unit will be deleted
                break;
            case State_Unit.Death:
                // Play animations
                break;
        }


    }


    private void DetermineCurrentState()
    {
        
        State_Unit _state = STATE_UNIT;

        DetermineNextTarget();

        if (CanAttackTarget())
        {
            _state = State_Unit.Attacking;
        }

        // only overwrite if state is not equal
        if (_state != STATE_UNIT)
        {
            STATE_UNIT = _state;
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

    private bool CanAttackTarget()
    {
        float attackRange = 8f;
        return Vector3.Distance(soldiers[0].transform.position, targetPosition) <= attackRange;
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
        Vector3 corner = this.transform.position;
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                Vector3 soldierPos = corner + new Vector3(i, 0, j);
                GameObject soldier = Instantiate(unitPrefab, soldierPos, transform.rotation);
                soldier.transform.SetParent(this.transform);
                soldiers.Add(soldier);
                formationGrid[i, j] = soldier;
                soldierInFormation[i, j] = true;
            }
        }
        formationStep = CalculateFormationStep();
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


    internal Vector3 CalculateFormationStep()
    {
        //return Vector3.MoveTowards(CalculateCenter(), targetPosition, StepSizeFormation); // Center Based
        return Vector3.MoveTowards(soldiers[0].transform.position, targetPosition, StepSizeFormation); // Corner Based

    }

    void UpdateSoldiersInFormation()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                GameObject soldier = formationGrid[i, j];

                Vector3 gridOffset = new Vector3(i, 0, j);

                Vector3 nextSoldierPos = formationStep + gridOffset;

                Vector3 nextSoldierPosNoHeight = new Vector3(nextSoldierPos.x, 0, nextSoldierPos.z);

                Vector3 soldierPos = new Vector3(soldier.transform.position.x, 0 , soldier.transform.position.z);

                Debug.DrawLine(soldier.transform.position, nextSoldierPos, Color.blue);
                          
                if (Vector3.Distance(soldierPos, nextSoldierPosNoHeight) > marginFormation)
                {
                    soldierInFormation[i, j] = false;
                } else
                {
                    soldier.GetComponent<AgentMovement>().StopAgent();
                    soldierInFormation[i, j] = true;
                }
            }
        }
    }

    bool AllSoldiersInFormation()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                if (soldierInFormation[i, j] == false)
                {
                    return false;
                }
            }
        }
        Debug.Log("All soldiers in formation: "+ STATE_FORMATION);
        return true;
    }

    internal void HandleMovement()
    {
        timer_Formation += Time.deltaTime;
        formationCenter = CalculateCenter();
        UpdateSoldiersInFormation();
        
        if (AllSoldiersInFormation() || timer_Formation - timestamp_Formation >= waitInFormationIntervall)
        {
            Debug.Log("Recalculating Formation Step and start moving again");
            formationStep = CalculateFormationStep();
            timer_Formation = 0f;
            timestamp_Formation = Time.deltaTime;
            StartAllSoldiers();
        } else
        {
            MoveInFormation();
        }


        
    }

    void StopAllSoldiers()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                AgentMovement soldier = formationGrid[i, j].GetComponent<AgentMovement>();
                soldier.StopAgent();
            }
        }
    }

    void StartAllSoldiers()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                AgentMovement soldier = formationGrid[i, j].GetComponent<AgentMovement>();
                soldier.StartAgent();
            }
        }
    }

    void MoveInFormation()
    {
        switch (STATE_FORMATION)
        {
            case State_Formation.Loose:
                //MoveInLooseFormation();
                break;
            case State_Formation.Grid:
                MoveInGridFormation();
                break;
        }

    }

    void MoveInGridFormation()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                Vector3 nextSoldierPos = formationStep + new Vector3(i, 0, j);
                formationGrid[i, j].GetComponent<AgentMovement>().SetTargetDestination(nextSoldierPos);
            }
        }
    }

    internal void MoveInLooseFormation()
    {
        for (int i = 0; i < soldiersAmount; i++)
        {
            Vector3 relativeToCenter = soldiers[i].GetComponent<AgentMovement>().GetCurrentPosition() - formationCenter;
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
