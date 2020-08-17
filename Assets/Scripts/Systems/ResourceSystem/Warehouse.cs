using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warehouse : MonoBehaviour
{
    [SerializeField]
    private Text foodUI;
    [SerializeField]
    private Text woodUI;
    [SerializeField]
    private Text feidhUI;

    private int foodAmount = 0;
    private int woodAmount = 0;
    private int feidhAmount = 0;

    public enum resourceType
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
        foodUI.text = foodAmount.ToString();
        woodUI.text = woodAmount.ToString();
        feidhUI.text = feidhAmount.ToString();
    }

    public bool Buy(resourceType type, int price)
    {
        bool ableToBuy = type != resourceType.NONE && price <= Get(type);
        if(ableToBuy)
        {
            Set(type, Get(type) - price);
        }
        return ableToBuy;
    }

    public void AddResource(resourceType type, int amount)
    {
        Set(type, Get(type) + amount);
    }

    private void Set(resourceType type, int amount)
    {
        switch(type)
        {
            case resourceType.FOOD:
                setFoodAmount(amount);
                break;
            case resourceType.WOOD:
                setWoodAmount(amount);
                break;
            case resourceType.FEIDH:
                setFeidhAmount(amount);
                break;
        }
    }

    private int Get(resourceType type)
    {
        switch(type)
        {
            case resourceType.FOOD:
                return getFoodAmount();
            case resourceType.WOOD:
                return getWoodAmount();
            case resourceType.FEIDH:
                return getFeidhAmount();
        }
        return -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        Worker worker = other.GetComponent<Worker>();
        if (worker != null)
        {
            worker.setState(Worker.State.WAITING);
            AddResource(worker.getProfession(), worker.getCurrentStorage());
            worker.setCurrentStorage(0);
            worker.getAgent().SetTargetDestination(worker.getWorkingPlace().gameObject.transform.position);
            worker.setState(Worker.State.MOVING);
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
