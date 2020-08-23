using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : Damageable
{
    [SerializeField]
    private ResourceManager.Resource profession = ResourceManager.Resource.NONE;

    [SerializeField]
    private Transform workingPlace;

    [SerializeField]
    private int maxStorage = 10;
    private int currentStorage = 0;

    [SerializeField]
    private float timeToAddResInSeconds = 3.0f;
    private float collectingTimer = 0f;

    public enum State
    {
        WAITING,
        COLLECTING_RESOURCE,
    }
    
    [SerializeField]
    private State currentState = State.WAITING;

    // Update is called once per frame
    void Update()
    {
        //switch (currentState)
        //{
            //case State.COLLECTING_RESOURCE:
            //    CollectResource();
            //    break;

            //case State.MOVING_WORKPLACE:
            //    //agent.SetTargetDestination(workingPlace.position);
            //    if (agent.ReachedDestination(workingPlace.position))
            //    {
            //        currentState = State.COLLECTING_RESOURCE;
            //    }
            //    break;
            //case State.MOVING_START:
            //    agent.SetTargetDestination(workingPlace.position);
            //    currentState = State.MOVING_WORKPLACE;
            //    break;
                //case State.MOVING_WAREHOUSE:
                //    if (agent.ReachedDestination(warehouse.position))
                //    {
                //        currentState = State.WAITING;
                //    }
                //    break;

                //case State.WAITING:
                //    if (agent.IsMoving())
                //        agent.StopAgent();
                //    break;
        //}
    }

    public ResourceManager.Resource GetProfession()
    {
        return profession;
    }

    public int GetMaxStorage()
    {
        return maxStorage;
    }

    public void SetMaxStorage(int amount)
    {
        maxStorage = amount;
    }

    public int GetCurrentStorage()
    {
        return currentStorage;
    }

    public void SetCurrentStorage(int amount)
    {
        currentStorage = amount;
    }

    public State GetState()
    {
        return currentState;
    }

    public void SetState(State state)
    {
        currentState = state;
    }

    public Transform GetWorkingPlace()
    {
        return workingPlace;
    }

    internal void SetWorkingPlace(Transform _workingPlace)
    {
        workingPlace = _workingPlace;
    }

    private void CollectResource()
    {
        collectingTimer += Time.deltaTime;
        if (collectingTimer >= timeToAddResInSeconds)
        {
            currentStorage++;
            if (currentStorage >= maxStorage)
            {
                currentState = State.COLLECTING_RESOURCE;
                collectingTimer = 0f;
            }
            else
            {
                collectingTimer = 0f;
            }
        }
    }
}
