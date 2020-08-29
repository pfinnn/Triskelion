﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIC_ResourceManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI foodAmount;
    [SerializeField]
    private TextMeshProUGUI woodAmount;
    [SerializeField]
    private TextMeshProUGUI feidhAmount;

    [SerializeField]
    private TextMeshProUGUI foodConsumption;
    [SerializeField]
    private TextMeshProUGUI woodConsumption;
    [SerializeField]
    private TextMeshProUGUI feidhConsumption;

    [SerializeField]
    private TextMeshProUGUI foodProduction;
    [SerializeField]
    private TextMeshProUGUI woodProduction;
    [SerializeField]
    private TextMeshProUGUI feidhProduction;

    [SerializeField]
    private Slider timeSlider;

    // Start is called before the first frame update
    void Start()
    {
    }

    internal void OnTimerSliderChanged(float value)
    {
        float maxValue = timeSlider.maxValue;
        timeSlider.value = maxValue - value;
    }
    internal void OnFoodAmountChanged(int _amount)
    {
        foodAmount.text = _amount.ToString();
    }

    internal void OnWoodAmountChanged(int _amount)
    {
        woodAmount.text = _amount.ToString();
    }

    internal void OnFeidhAmountChanged(int _amount)
    {
        feidhAmount.text = _amount.ToString();
    }

    internal void SetMaxValueTimeSlider(float maxValue)
    {
        timeSlider.maxValue = maxValue;
    }

    internal void OnFoodConsumptionAmountChanged(int _amount)
    {
        foodConsumption.text = _amount.ToString();
    }

    internal void OnWoodConsumptionAmountChanged(int _amount)
    {
        woodConsumption.text = _amount.ToString();
    }

    internal void OnFeidhConsumptionAmountChanged(int _amount)
    {
        feidhConsumption.text = _amount.ToString();
    }
    internal void OnFoodProductionAmountChanged(int _amount)
    {
        foodProduction.text = _amount.ToString();
    }

    internal void OnWoodProductionAmountChanged(int _amount)
    {
        woodProduction.text = _amount.ToString();
    }

    internal void OnFeidhProductionAmountChanged(int _amount)
    {
        feidhProduction.text = _amount.ToString();
    }
}
