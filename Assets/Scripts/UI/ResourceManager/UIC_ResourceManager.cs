using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIC_ResourceManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI foodUI;
    [SerializeField]
    private TextMeshProUGUI woodUI;
    [SerializeField]
    private TextMeshProUGUI feidhUI;
    [SerializeField]
    private Slider populationsEatsSlider;

    // Start is called before the first frame update
    void Start()
    {
    }
    internal void OnPopulationEatsTimerChanged(float value)
    {
        float maxValue = populationsEatsSlider.maxValue;
        populationsEatsSlider.value = maxValue - value;
    }

    internal void OnFoodAmountChanged(int _amount)
    {
        foodUI.text = _amount.ToString();
    }

    internal void OnWoodAmountChanged(int _amount)
    {
        woodUI.text = _amount.ToString();
    }

    internal void OnFeidhAmountChanged(int _amount)
    {
        feidhUI.text = _amount.ToString();
    }

    internal void SetMaxValueForSlider(float maxValue)
    {
        populationsEatsSlider.maxValue = maxValue;
    }
}
