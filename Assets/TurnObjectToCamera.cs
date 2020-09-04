using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnObjectToCamera : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        slider.maxValue = GetComponentInParent<Tower>().GetMaxHealth();
    }
    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
        slider.value = GetComponentInParent<Tower>().GetHealth();
    }
}
