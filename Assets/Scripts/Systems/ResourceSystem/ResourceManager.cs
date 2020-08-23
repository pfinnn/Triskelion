using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private WorkerManager workerManager;

    [SerializeField]
    private UIC_ResourceManager uic;

    private int foodAmount = 0;
    private int woodAmount = 0;
    private int feidhAmount = 0;

    private int foodPerWorker = 3;
    private int woodPerWorker = 2;
    private int feidhPerWorker = 1;

    private float timer = 0f;
    private float timer_intervall_update = 6.5f;

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
        Notify_UIC();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= timer_intervall_update)
        {
            UpdateResourceValues();
            Notify_UIC();
            timer = 0f;
        }
    }

    private void UpdateResourceValues()
    {
        foodAmount += workerManager.GetFarmersCount() * foodPerWorker;
        woodAmount += workerManager.GetWoodcuttersCount() * woodPerWorker;
        feidhAmount += workerManager.GetDruidsCount() * feidhPerWorker;
    }

    private void Notify_UIC()
    {
        uic.OnFoodAmountChanged(foodAmount);
        uic.OnWoodAmountChanged(woodAmount);
        uic.OnFeidhAmountChanged(feidhAmount);
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
                SetFoodAmount(amount);
                break;
            case Resource.WOOD:
                SetWoodAmount(amount);
                break;
            case Resource.FEIDH:
                SetFeidhAmount(amount);
                break;
        }
    }

    private int Get(Resource type)
    {
        switch(type)
        {
            case Resource.FOOD:
                return GetFoodAmount();
            case Resource.WOOD:
                return GetWoodAmount();
            case Resource.FEIDH:
                return GetFeidhAmount();
        }
        return -1;
    }

    public int GetFoodAmount()
    {
        return foodAmount;
    }

    public void SetFoodAmount(int foodAmount)
    {
        this.foodAmount = foodAmount;
        Notify_UIC();
    }

    public int GetWoodAmount()
    {
        return woodAmount;
    }

    public void SetWoodAmount(int woodAmount)
    {
        this.woodAmount = woodAmount;
        Notify_UIC();
    }

    public int GetFeidhAmount()
    {
        return feidhAmount;
    }

    public void SetFeidhAmount(int feidhAmount)
    {
        this.feidhAmount = feidhAmount;
        Notify_UIC();
    }
}
