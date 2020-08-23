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
