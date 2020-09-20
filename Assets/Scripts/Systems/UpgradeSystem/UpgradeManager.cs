using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    [SerializeField]
    ResourceManager resourceManager;

    [SerializeField]
    UIC_UpgradePanel uic;

    private int farmerTechLVL = 1;
    private int woodcutterTechLVL = 1;
    private int druidTechLVL = 1;

    private int farmerCostBase = 4;
    private int woodcutterCostBase = 5;
    private int druidCostBase = 6;

    private int farmerCost;
    private int woodcutterCost;
    private int druidCost;

    // Start is called before the first frame update
    void Start()
    {
        farmerCost = farmerCostBase * farmerTechLVL;
        woodcutterCost = woodcutterCostBase * woodcutterTechLVL;
        druidCost = druidCostBase * druidTechLVL;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void UpgradeFarmerTech()
    {
        if (resourceManager.GetWoodAmount() >= farmerCost)
        {
            farmerTechLVL += 1;
            farmerCost = farmerCostBase * farmerTechLVL;
            resourceManager.Buy(ResourceManager.Resource.WOOD, farmerCost);
            resourceManager.SetProductionModifier(ResourceManager.Resource.FOOD, farmerTechLVL);
            uic.RefreshFarmerValues();
        }

    }
    internal void UpgradeWoodcutterTech()
    {
        if (resourceManager.GetWoodAmount() >= woodcutterCost)
        {
            woodcutterTechLVL += 1;
            woodcutterCost = woodcutterCostBase * woodcutterTechLVL;
            resourceManager.Buy(ResourceManager.Resource.WOOD, woodcutterCost);
            resourceManager.SetProductionModifier(ResourceManager.Resource.WOOD, woodcutterTechLVL);
            uic.RefreshWoodcutterValues();
        }
    }
    internal void UpgradeDruidTech()
    {
        if (resourceManager.GetWoodAmount() >= druidCost)
        {
            druidTechLVL += 1;
            druidCost = druidCostBase * druidCost;
            resourceManager.Buy(ResourceManager.Resource.WOOD, druidCost);
            resourceManager.SetProductionModifier(ResourceManager.Resource.FEIDH, druidTechLVL);
            uic.RefreshDruidValues();
        }
    }


    internal int GetFarmerTechLevel()
    {
        return farmerTechLVL;
    }

    internal int GetWoodcutterTechLevel()
    {
        return woodcutterTechLVL;
    }

    internal int GetDruidTechLevel()
    {
        return druidTechLVL;
    }

    internal int GetFarmerCost()
    {
        return farmerCost;
    }

    internal int GetWoodcutterCost()
    {
        return woodcutterCost;
    }

    internal int GetDruidCost()
    {
        return druidCost;
    }

}
