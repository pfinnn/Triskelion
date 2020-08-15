using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingStation : MonoBehaviour
{
    [SerializeField]
    private float addingIntervallInSeconds;
    [SerializeField]
    private Inventory.resourceType resourceType;
    [SerializeField]
    private Inventory resourceSystem;

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= addingIntervallInSeconds)
        {
            resourceSystem.AddResource(resourceType, 1);
            timer = 0f;
        }
    }
}
