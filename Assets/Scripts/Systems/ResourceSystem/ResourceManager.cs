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

    private int foodProdPerWorker = 3;
    private int woodProdPerWorker = 2;
    private int feidhProdPerWorker = 1;

    private float foodConsPerWorker = 1.5f;
    private float woodConsPerWorker = 2.5f;
    private float feidhConsPerWorker = 3.0f;

    float consumptionModifier = 1f;

    private int currentConsumptionFood = 0;
    private int currentConsumptionWood = 0;
    private int currentConsumptionFeidh = 0;

    private int currentProductionFood = 0;
    private int currentProductionWood = 0;
    private int currentProductionFeidh = 0;

    private float timer_update = 0f;
    private float timer_update_interval = 6.5f;
    private int timer_starving = 0;
    private int timer_starving_interval = 3;

    public enum Resource
    {
        FOOD, WOOD, FEIDH, NONE
    }

    // Start is called before the first frame update
    void Start()
    {
        foodAmount = 100;
        woodAmount = 50;
        feidhAmount = 25000;
        uic.SetMaxValueTimeSlider(timer_update_interval);
        UpdateConsumptionValues();
        Notify_UIC();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer_update += Time.deltaTime;
        if (timer_update >= timer_update_interval)
        {
            PopulationProduces();
            PopulationConsumes();
            timer_update = 0f;
            if (foodAmount == 0)
            {
                timer_starving += 1;
                if (timer_starving >= timer_starving_interval)
                {
                    workerManager.Starve();
                    timer_starving = 0;
                }
            }
        }
        Notify_UIC();
        UpdateProductionValues();
        UpdateConsumptionValues();
    }

    private void UpdateConsumptionValues()
    {
        int population = workerManager.GetCurrentPopulation();
        currentConsumptionFood = Mathf.RoundToInt(population / foodConsPerWorker * consumptionModifier);
        currentConsumptionWood = Mathf.RoundToInt(population / woodConsPerWorker * consumptionModifier);
        currentConsumptionFeidh = Mathf.RoundToInt(population / feidhConsPerWorker * consumptionModifier);
    }

    private void UpdateProductionValues()
    {
        int population = workerManager.GetCurrentPopulation();
        currentProductionFood = workerManager.GetFarmersCount() * foodProdPerWorker;
        currentProductionWood = workerManager.GetWoodcuttersCount() * woodProdPerWorker;
        currentProductionFeidh = workerManager.GetDruidsCount() * feidhProdPerWorker;
    }

    private void PopulationProduces()
    {
        foodAmount += currentProductionFood;
        woodAmount += currentProductionWood;
        feidhAmount += currentProductionFeidh;
    }

    private void PopulationConsumes()
    {
        UpdateConsumptionValues();
        foodAmount -= currentConsumptionFood;
        if (foodAmount < 0)
        {
            foodAmount = 0;
        }
        woodAmount -= currentConsumptionWood;
        if (woodAmount < 0)
        {
            woodAmount = 0;
        }
        feidhAmount -= currentConsumptionFeidh;
        if (feidhAmount < 0)
        {
            feidhAmount = 0;
        }
    }

    // THIS IS VERYYY BAD PRACTICE
    private void Notify_UIC()
    {
        uic.OnFoodAmountChanged(foodAmount);
        uic.OnWoodAmountChanged(woodAmount);
        uic.OnFeidhAmountChanged(feidhAmount);
        uic.OnFoodConsumptionAmountChanged(currentConsumptionFood);
        uic.OnWoodConsumptionAmountChanged(currentConsumptionWood);
        uic.OnFeidhConsumptionAmountChanged(currentConsumptionFeidh);
        uic.OnFoodProductionAmountChanged(currentProductionFood);
        uic.OnWoodProductionAmountChanged(currentProductionWood);
        uic.OnFeidhProductionAmountChanged(currentProductionFeidh);
        uic.OnTimerSliderChanged(timer_update);
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
        Notify_UIC();
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
