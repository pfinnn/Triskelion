using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingStation : MonoBehaviour
{
    [SerializeField]
    private Warehouse.resourceType resourceType;
    [SerializeField]
    private Warehouse resourceSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Worker worker = other.GetComponentInParent<Worker>();
        if (worker != null)
        {
            worker.setState(Worker.State.COLLECTING_RESOURCE);
        }
    }

    public Warehouse.resourceType GetResource()
    {
        return resourceType;
    }
}
