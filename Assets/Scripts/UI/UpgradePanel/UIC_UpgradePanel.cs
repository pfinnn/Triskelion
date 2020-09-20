using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIC_UpgradePanel : MonoBehaviour
{

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    UpgradeManager upgradeManager;

    [SerializeField]
    private TextMeshProUGUI farmerTechLVL;
    
    [SerializeField]
    private TextMeshProUGUI woodcutterTechLVL;

    [SerializeField]
    private TextMeshProUGUI druidTechLVL;

    [SerializeField]
    private TextMeshProUGUI farmerCost;

    [SerializeField]
    private TextMeshProUGUI woodcutterCost;

    [SerializeField]
    private TextMeshProUGUI druidCost;

    // Start is called before the first frame update
    void Start()
    {
        // tech level
        farmerTechLVL.text = upgradeManager.GetFarmerTechLevel().ToString();
        woodcutterTechLVL.text = upgradeManager.GetWoodcutterTechLevel().ToString();
        druidTechLVL.text = upgradeManager.GetDruidTechLevel().ToString();
        // cost
        farmerCost.text = upgradeManager.GetFarmerCost().ToString();
        woodcutterCost.text = upgradeManager.GetWoodcutterCost().ToString();
        druidCost.text = upgradeManager.GetDruidCost().ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // this should be either inherited or directly notified to the AM
    public void NotifyAudioManagerButtonClicked()
    {
        audioManager.PlayUISound(AudioManager.UISoundTypes.ButtonClicked);
    }

    public void UpgradeFarmerTech()
    {
        upgradeManager.UpgradeFarmerTech();
    }

    public void UpgradeWoodcutterTech()
    {
        upgradeManager.UpgradeWoodcutterTech();
    }

    public void UpgradDruidTech()
    {
        upgradeManager.UpgradeDruidTech();
    }

    internal void RefreshFarmerValues()
    {
        farmerTechLVL.text = upgradeManager.GetFarmerTechLevel().ToString();
        farmerCost.text = upgradeManager.GetFarmerCost().ToString();
    }
    internal void RefreshWoodcutterValues()
    {
        woodcutterTechLVL.text = upgradeManager.GetWoodcutterTechLevel().ToString();
        woodcutterCost.text = upgradeManager.GetWoodcutterCost().ToString();
    }
    internal void RefreshDruidValues()
    {
        druidTechLVL.text = upgradeManager.GetDruidTechLevel().ToString();
        druidCost.text = upgradeManager.GetDruidCost().ToString();
    }
}
