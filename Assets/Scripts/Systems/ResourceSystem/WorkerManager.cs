using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<GameObject, FarmingStation> workers = new Dictionary<GameObject, FarmingStation>();

    [SerializeField]
    private int amountOfSpawningWorkers = 10;
    [SerializeField]
    private GameObject workerPrefab;
    [SerializeField]
    private Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWorkers(amountOfSpawningWorkers);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnWorkers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject worker = Instantiate(workerPrefab, spawnPoint);
            workers.Add(worker, null);
        }
    }

    private void AssignWorkerToFarmingStation(GameObject worker, FarmingStation station)
    {
        workers[worker] = station;
        Worker workerComponent = worker.GetComponent<Worker>();
        workerComponent.setWorkingPlace(station);
        workerComponent.setProfession(station.GetResource());
    }
}
