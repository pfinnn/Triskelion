using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingStation : MonoBehaviour
{
    [SerializeField]
    private Inventory.resourceType resourceType;
    [SerializeField]
    private Inventory resourceSystem;

    private List<Worker> workers = new List<Worker>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addWorker(Worker worker)
    {
        this.workers.Add(worker);
        worker.setWorkingPlace(this);
    }

    public void removeWorker(Worker worker)
    {
        this.workers.Remove(worker);
        worker.setWorkingPlace(null);
    }

    private void OnDestroy()
    {
        this.workers.RemoveRange(0, this.workers.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        Worker worker = other.GetComponentInParent<Worker>();
        if (worker != null)
        {
            resourceSystem.AddResource(resourceType, worker.getCurrentStorage());
            worker.setCurrentStorage(0);
        }
    }
}
