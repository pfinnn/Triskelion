using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : Damageable
{
    [SerializeField]
    private Warehouse.resourceType profession = Warehouse.resourceType.NONE;
    [SerializeField]
    private Transform warehouseTransform;
    [SerializeField]
    private GameObject workingPlace;

    [SerializeField]
    private int maxStorage = 10;
    private int currentStorage = 0;

    [SerializeField]
    private float timeToAddResInSeconds = 3.0f;
    private float collectingTimer = 0f;

    private AgentMovement agent;

    public enum State
    {
        WAITING,
        COLLECTING_RESOURCE,
        MOVING
    }

    [SerializeField]
    private State currentState = State.WAITING;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        agent = GetComponent<AgentMovement>();
        warehouseTransform = GetComponentInParent<WorkerManager>().getWarehouseTransform();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.COLLECTING_RESOURCE:
                CollectResource();
                break;

            case State.MOVING:
                //Do something
                break;

            case State.WAITING:
                if (agent.IsMoving())
                    agent.StopAgent();
                break;
        }
    }

    public Warehouse.resourceType getProfession()
    {
        return profession;
    }

    public void setProfession(Warehouse.resourceType profession)
    {
        this.profession = profession;
    }

    public int getMaxStorage()
    {
        return maxStorage;
    }

    public void setMaxStorage(int amount)
    {
        maxStorage = amount;
    }

    public int getCurrentStorage()
    {
        return currentStorage;
    }

    public void setCurrentStorage(int amount)
    {
        currentStorage = amount;
    }

    public State getState()
    {
        return currentState;
    }

    public void setState(State state)
    {
        currentState = state;
    }

    public GameObject getWorkingPlace()
    {
        return workingPlace;
    }

    public void setWorkingPlace(GameObject workingPlace)
    {
        this.workingPlace = workingPlace;
    }

    public AgentMovement getAgent()
    {
        return agent;
    }

    private void CollectResource()
    {
        collectingTimer += Time.deltaTime;
        if (collectingTimer >= timeToAddResInSeconds)
        {
            currentStorage++;
            if (currentStorage >= maxStorage)
            {
                agent.SetTargetDestination(warehouseTransform.position);
                agent.StartAgent();
                currentState = State.MOVING;
                collectingTimer = 0f;
            }
            else
            {
                collectingTimer = 0f;
            }
        }
    }
}
