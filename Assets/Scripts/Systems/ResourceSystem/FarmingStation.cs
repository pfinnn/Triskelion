using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingStation : MonoBehaviour
{
    [SerializeField]
    private ResourceManager.Resource resourceType;
    [SerializeField]
    private ResourceManager resourceSystem;

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
            worker.SetState(Worker.State.COLLECTING_RESOURCE);
        }
    }

    public ResourceManager.Resource GetResource()
    {
        return resourceType;
    }
}
