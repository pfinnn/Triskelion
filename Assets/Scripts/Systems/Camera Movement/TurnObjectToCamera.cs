using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObjectToCamera : MonoBehaviour
{
    private void Start()
    {
        GetComponentInParent<Canvas>().worldCamera = Camera.main;
    }
    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
