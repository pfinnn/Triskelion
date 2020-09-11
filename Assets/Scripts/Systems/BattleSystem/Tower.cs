using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tower : Damageable
{
    [SerializeField]
    ResourceManager resourceManager;

    private ShootingSystem shootingSystem;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float reloadTimer = 0.0f;

    [SerializeField]
    public GameObject destroyed_Particle;

    private Vector3 shootingPoint;

    private GameObject currentTarget;

    int repairCost = 20;
    float repairAmount;
    int repairRatio = 10;

    internal bool needsRepair = false;

    public enum State
    {
        Idle,
        Attacking,
        Upgrading,
        Reloading,
        Destroyed,
    }

    public State currentState = State.Idle;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        shootingSystem = GetComponent<ShootingSystem>();
        shootingPoint = shootingSystem.GetShootingPoint();
        this.gameObject.tag = "defenders";
        repairAmount = GetMaxHealth() / repairRatio;
    }

    void FixedUpdate()
    {


        if (GetHealth() < 1)
        {
            if (currentState != State.Destroyed )
            {
                Instantiate(destroyed_Particle, shootingPoint, Quaternion.identity );
                currentState = State.Destroyed;
            }
            return;
        } else if (currentState == State.Destroyed)
            {
                currentState = State.Idle;
            }

        if (TargetsInRage())
        {
            UpdateTargets();
            currentState = State.Attacking;
        }
        else
        {
            currentState = State.Idle;
        }

        if(currentState == State.Attacking)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > 1)
            {
                Shoot();
                reloadTimer = 0.0f;
            }
        }
    }

    private void Shoot()
    {
        Transform randomSoldier = GetRandomSoldierPosition();
        float soldierSpeed = 10f;
        shootingSystem.ShootSoldierInUnit(randomSoldier, soldierSpeed);
    }

    private Transform GetRandomSoldierPosition()
    {
        if(enemiesInRange.Count < 1)
        {
            throw new NullReferenceException();
        }
        List<GameObject> soldiers = enemiesInRange[0].GetComponentInParent<UnitController>().GetSoldiers();
        currentTarget = soldiers[Random.Range(0, soldiers.Count - 1)];
        return currentTarget.transform;
    }

    private bool TargetsInRage()
    {
        return enemiesInRange.Count > 0;
    }

    public void UpdateTargets()
    {
        if (enemiesInRange.Count > 0)
        {
            List<GameObject> deadEnemies = new List<GameObject>();
            foreach (GameObject enemy in enemiesInRange)
            {
                if (enemy == null)
                {
                    deadEnemies.Add(enemy);
                }
            }
            // is this loop redundant ??
            foreach (GameObject deadEnemy in deadEnemies)
            {
                enemiesInRange.Remove(deadEnemy);
            }
        }
    }

    public List<GameObject> GetEnemiesInRange()
    {
        return enemiesInRange;
    }


    public void Repair()
    {
        if (resourceManager.GetWoodAmount() >= repairCost)
        {
            base.Repair(repairAmount);
            resourceManager.Buy(ResourceManager.Resource.WOOD, repairCost);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("attackers"))
        {
            if (other.GetComponentInParent<UnitController>())
            {
                enemiesInRange.Add(other.gameObject);
            }
        }
    }



    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("attackers"))
        {
            if (other.GetComponentInParent<UnitController>())
            {
                enemiesInRange.Remove(other.gameObject);
            }

        }
            
    }
    
}

