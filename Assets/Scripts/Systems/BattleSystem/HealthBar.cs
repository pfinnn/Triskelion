using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        slider.maxValue = GetComponentInParent<Tower>().GetMaxHealth();
    }

    private void FixedUpdate()
    {
        slider.value = GetComponentInParent<Tower>().GetHealth();
    }
}
