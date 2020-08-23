using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private WorkerManager workerManager;

    private int foodAmount = 0;
    private int woodAmount = 0;
    private int feidhAmount = 0;

    public enum Resource
    {
        FOOD, WOOD, FEIDH, NONE
    }

    // Start is called before the first frame update
    void Start()
    {
        foodAmount = 0;
        woodAmount = 0;
        feidhAmount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateResourceValues();
    }

    private void UpdateResourceValues()
    {
        //throw new NotImplementedException();
    }

    public bool Buy(Resource type, int price)
    {
        bool ableToBuy = type != Resource.NONE && price <= Get(type);
        if(ableToBuy)
        {
            Set(type, Get(type) - price);
        }
        return ableToBuy;
    }

    public void AddResource(Resource type, int amount)
    {
        Set(type, Get(type) + amount);
    }

    private void Set(Resource type, int amount)
    {
        switch(type)
        {
            case Resource.FOOD:
                setFoodAmount(amount);
                break;
            case Resource.WOOD:
                setWoodAmount(amount);
                break;
            case Resource.FEIDH:
                setFeidhAmount(amount);
                break;
        }
    }

    private int Get(Resource type)
    {
        switch(type)
        {
            case Resource.FOOD:
                return getFoodAmount();
            case Resource.WOOD:
                return getWoodAmount();
            case Resource.FEIDH:
                return getFeidhAmount();
        }
        return -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        Worker worker = other.GetComponent<Worker>();
        if (worker != null)
        {
            worker.SetState(Worker.State.WAITING);
            AddResource(worker.GetProfession(), worker.GetCurrentStorage());
            worker.SetCurrentStorage(0);
            worker.GetAgent().SetTargetDestination(worker.GetWorkingPlace().gameObject.transform.position);
            worker.SetState(Worker.State.MOVING_WORKPLACE);
        }
    }

    public int getFoodAmount()
    {
        return foodAmount;
    }

    public void setFoodAmount(int foodAmount)
    {
        this.foodAmount = foodAmount;
    }

    public int getWoodAmount()
    {
        return woodAmount;
    }

    public void setWoodAmount(int woodAmount)
    {
        this.woodAmount = woodAmount;
    }

    public int getFeidhAmount()
    {
        return feidhAmount;
    }

    public void setFeidhAmount(int feidhAmount)
    {
        this.feidhAmount = feidhAmount;
    }
}
