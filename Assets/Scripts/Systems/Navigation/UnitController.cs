using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    GameObject unitPrefab;
    [SerializeField]
    int soldiersAmount = 30;
    [SerializeField]
    int soldiersPerRow = 5;
    int rows;

    Damageable target;
    Vector3 targetPosition;
    Vector3 triskelionPosition; // remember triskelion position to always be able to go back there without searching for the GO
    ShootingSystem shootingSystem;
    List<GameObject> soldiers;
    float soldierSpeed;

    // Target Detection
    List<Damageable> targetsInRange;
    float attackRange = 15f;
    SphereCollider triggerVolume;

    // Attacking
    float rangeAttackSalves = 0;
    float rangeAttackMaxSalves = 3;
    float meleeAttackIntervall = 2;
    float missProbabilityFactorMelee = 0.95f;
    float missProbabilityFactorRange = 0.5f;
    float damageRangeAttack = 0.5f;
    float damageMeleeAttack = 0.1f;
    float timestamp_lastAttack = 0f;
    float timer_lastAttack = 0f;
    float rangeAttackIntervall = 3f;
    float keepDistanceToTarget = 15f;

    // Formation
    GameObject[,] formationGrid;
    bool[,] soldiersInFormation;
    Vector3 formationStep;
    Vector3 formationCenter;
    float marginFormation = 0.5f;
    float StepSizeFormation = 40f;
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

    public enum State_AttackFlow
    {
        Positioning,
        ValidFormation,
        Range,
        Melee
    }

    public State_AttackFlow STATE_ATTACKFLOW = State_AttackFlow.Positioning;

    private void Awake()
    {
        STATE_UNIT = State_Unit.Moving;
        STATE_FORMATION = State_Formation.Grid;
        soldiers = new List<GameObject>();
        targetsInRange = new List<Damageable>();
        rows = Mathf.RoundToInt(soldiersAmount / soldiersPerRow);
        formationGrid = new GameObject[rows, soldiersPerRow];
        soldiersInFormation = new bool[rows, soldiersPerRow];
        shootingSystem = this.GetComponent<ShootingSystem>();
        targetPosition = GameObject.Find("Triskelion").transform.position;
        triskelionPosition = targetPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "attackers";
        triggerVolume = GetComponentInChildren<SphereCollider>();
        triggerVolume.tag = "attackers";
        SpawnUnitsInFormation();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(soldiers[0].transform.position, formationStep, Color.blue);

        triggerVolume.transform.position = CalculateCenter();

        DetermineCurrentUnitState();

        switch (STATE_UNIT)
        {
            case State_Unit.Idle:
                IdlePositions();
                break;
            case State_Unit.Attacking:
                HandleAttackingFlow();
                // play attack animations 
                break;
            case State_Unit.Moving:
                HandleMovement();
                break;
            case State_Unit.Fleeing:
                // run back to spawn area where unit will be deleted
                break;
            case State_Unit.Death:
                // Play animations
                break;
        }

    }

    private void HandleAttackingFlow()
    {
        
        switch (STATE_ATTACKFLOW)
        {
            case State_AttackFlow.Positioning:
                timer_Formation += Time.deltaTime;
                bool timerFormationExceeded = timer_Formation - timestamp_Formation >= (waitInFormationIntervall);
                if (timerFormationExceeded || AllSoldiersInFormation())
                {
                    timer_Formation = 0f;
                    STATE_ATTACKFLOW = State_AttackFlow.ValidFormation;
                    return;
                }

                formationStep = Vector3.MoveTowards(targetPosition, formationCenter, keepDistanceToTarget);

                MoveInCurrentFormation();
                break;

            case State_AttackFlow.ValidFormation:
                if (rangeAttackSalves < rangeAttackMaxSalves)
                {
                    STATE_ATTACKFLOW = State_AttackFlow.Range;
                } else
                {
                    timer_lastAttack = 0f;
                    STATE_ATTACKFLOW = State_AttackFlow.Melee;
                }
                break;

            case State_AttackFlow.Range:
                timer_lastAttack += Time.deltaTime;
                if (rangeAttackSalves >= rangeAttackMaxSalves)
                {
                    STATE_ATTACKFLOW = State_AttackFlow.Melee;

                    STATE_FORMATION = State_Formation.Loose;
                    keepDistanceToTarget = 1f;
                    formationStep = Vector3.MoveTowards(formationCenter, targetPosition, keepDistanceToTarget);
                    MoveInCurrentFormation();
                    timestamp_Formation = Time.deltaTime;
                }
                else if (timer_lastAttack - timestamp_lastAttack >= rangeAttackIntervall )
                {
                    rangeAttack();
                }
                break;

            case State_AttackFlow.Melee:
                timer_lastAttack += Time.deltaTime;
                if (rangeAttackSalves >= rangeAttackMaxSalves)
                {
                    meleeAttack();
                }
                break;

        }

    }


    private void meleeAttack()
    {
        // attack based on timer
        timer_lastAttack = 0f;
        timestamp_lastAttack = Time.deltaTime;

        // play melee animation

        // play sound

        // calculcate hits and dmg
        float dmg = 0;
        // calculcate hits and dmg
        for (int i = 0; i < soldiersAmount; i++)
        {
            if (Random.value < 1 - missProbabilityFactorMelee)
            {
                dmg += damageMeleeAttack;
            }
        }

        // Deal damage to target
        
        target.DealDamage(dmg);

    }

    private void rangeAttack()
    {
        // attack based on timer
        timer_lastAttack = 0f;
        timestamp_lastAttack = Time.deltaTime;

        // play spear animation

        // play sound
        float dmg = 0;
        // calculcate hits and dmg
        for (int i = 0; i < soldiersAmount; i++)
        {
            if (Random.value < 1 - missProbabilityFactorRange)
            {
                dmg += damageRangeAttack;
            }
        }
        // Deal damage to target
        target.DealDamage(dmg);
        ++rangeAttackSalves;
    }

    private void DetermineCurrentUnitState()
    {
        
        State_Unit _state = STATE_UNIT;

        DetermineNextTarget();

        if (CanAttackTarget())
        {
            _state = State_Unit.Attacking;
            timestamp_Formation = Time.deltaTime;
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
            Damageable possibleTarget = targetsInRange[0];
            target = possibleTarget;
            targetPosition = possibleTarget.transform.position;
            keepDistanceToTarget = 15f;
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
                soldiersInFormation[i, j] = true;
            }
        }
        soldierSpeed = soldiers[0].GetComponent<NavMeshAgent>().speed;
        formationStep = CalculateFormationStep();
    }

    internal Vector3 CalculateCenter()
    {
        Vector3 total = Vector3.zero;
        foreach (GameObject s in soldiers)
        {
            if (s != null)
            {
                total += s.transform.position;
            }
        }
        return total/soldiers.Count;
    }

    internal Vector3 CalculateFormationStep() { 

        Vector3 destination = Vector3.MoveTowards(soldiers[0].transform.position, targetPosition, StepSizeFormation);
        NavMeshHit hit;
        NavMesh.SamplePosition(destination, out hit, 100f, NavMesh.AllAreas);
        return hit.position;
    }

    void UpdateSoldiersInFormation()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                if (formationGrid[i,j] != null)
                {
                    GameObject soldier = formationGrid[i, j];  // you maniac create a new list every update ???????????????????

                    Vector3 gridOffset = new Vector3(i, 0, j);

                    Vector3 nextSoldierPos = formationStep + gridOffset;

                    Vector3 nextSoldierPosNoHeight = new Vector3(nextSoldierPos.x, 0, nextSoldierPos.z);

                    Vector3 soldierPos = new Vector3(soldier.transform.position.x, 0, soldier.transform.position.z);

                    if (Vector3.Distance(soldierPos, nextSoldierPosNoHeight) > marginFormation)
                    {
                        soldiersInFormation[i, j] = false;
                    }
                    else
                    {
                        soldier.GetComponent<AgentMovement>().StopAgent();
                        soldiersInFormation[i, j] = true;
                    }
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
                if(formationGrid[i,j] != null)
                {
                    if (soldiersInFormation[i, j] == false)
                    {
                        return false;
                    }
                }

            }
        }
        return true;
    }

    internal void HandleMovement()
    {
        timer_Formation += Time.deltaTime;
        formationCenter = CalculateCenter();
        UpdateSoldiersInFormation();
        
        if (AllSoldiersInFormation() || timer_Formation - timestamp_Formation >= waitInFormationIntervall)
        {
            formationStep = CalculateFormationStep();
            timer_Formation = 0f;
            timestamp_Formation = Time.deltaTime;
            StartAllSoldiers();
        } else
        {
            MoveInCurrentFormation();
        }

    }

    void StopAllSoldiers()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                if (formationGrid[i,j] != null)
                {
                    AgentMovement soldier = formationGrid[i, j].GetComponent<AgentMovement>();
                    soldier.StopAgent();
                }
            }
        }
    }

    public void StartAllSoldiers()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < soldiersPerRow; j++)
            {
                if (formationGrid[i, j] != null)
                {
                    AgentMovement soldier = formationGrid[i, j].GetComponent<AgentMovement>();
                    soldier.StartAgent();
                }

            }
        }
    }

    void MoveInCurrentFormation()
    {
        switch (STATE_FORMATION)
        {
            case State_Formation.Loose:
                MoveInLooseFormation();
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
                if (formationGrid[i, j] != null)
                {
                    Vector3 nextSoldierPos = formationStep + new Vector3(i, 0, j);
                    formationGrid[i, j].GetComponent<AgentMovement>().SetTargetDestination(nextSoldierPos);
                }
            }
        }
    }

    internal void MoveInLooseFormation()
    {
        for (int i = 0; i < soldiersAmount; i++)
        {
            if (soldiers[i] != null)
            {
                Vector3 relativeToCenter = soldiers[i].GetComponent<AgentMovement>().GetCurrentPosition() - formationCenter;
                Vector3 agentDirection = targetPosition + relativeToCenter;
                Debug.DrawLine(soldiers[i].transform.position, agentDirection);
                soldiers[i].GetComponent<AgentMovement>().SetTargetDestination(agentDirection);
            }
        }
    }

    internal void SetTarget(Vector3 _targetPosition)
    {
        targetPosition = _targetPosition;
    }

    // Event Handlers

    internal void OnChildTriggerEnter(Damageable other)
    {
        String tag = other.gameObject.tag;
        
        if (tag == "defenders")
        {
            targetsInRange.Add(other);
        }

    }

    internal void OnChildTriggerExit(Damageable other)
    {
        targetsInRange.Remove(other);
    }

    internal void OnSoldierDying(GameObject dyingSoldier)
    {
        --soldiersAmount;
        soldiers.Remove(dyingSoldier);

        if (soldiers.Count == 0)
        {
            Destroy(this.gameObject); // TODO, use Object Pooling to handle units and soldier instantiation!
        }

    }

    // Getter and Setter

    public float GetSoldierSpeed()
    {
        return soldierSpeed;
    }

    public List<GameObject> GetSoldiers()
    {
        return soldiers;
    }

}
