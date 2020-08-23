using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WorkerManager : MonoBehaviour
{
    [SerializeField]
    private int amountOfSpawningWorkers = 10;

    [SerializeField]
    private GameObject workerPrefab;

    [SerializeField]
    private Transform warehouse;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private List<Transform> fields = new List<Transform>();

    [SerializeField]
    private List<Transform> woodcutterHuts = new List<Transform>();

    [SerializeField]
    private Transform druidWorkplaces; // triskelion

    private int inactiveWorkers = 10;
    private List<GameObject> activeFarmers = new List<GameObject>();
    private List<GameObject> activeWoodcutters = new List<GameObject>();
    private List<GameObject> activeDruids = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnWorkers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(spawnPoint.position, out hit, 10f, NavMesh.AllAreas);
            GameObject worker = Instantiate(workerPrefab, hit.position, Quaternion.identity);
            worker.transform.SetParent(this.transform);
            ++inactiveWorkers;
        }
    }

    private GameObject SpawnWorker()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(spawnPoint.position, out hit, 10f, NavMesh.AllAreas);
        GameObject worker = Instantiate(workerPrefab, hit.position, Quaternion.identity);
        worker.transform.SetParent(this.transform);
        --inactiveWorkers;
        return worker;
    }

    private bool HasFreeWorkers()
    {
        Worker workerComponent = worker.GetComponent<Worker>();
        workerComponent.setProfession(null == station ? Warehouse.resourceType.NONE : station.GetComponent<FarmingStation>().GetResource());
        workerComponent.setWorkingPlace(station);
        workerComponent.getAgent().SetTargetDestination(station == null ? spawnPoint.position : station.transform.position);
        workerComponent.setState(station == null ? Worker.State.WAITING : Worker.State.MOVING);
        workers[worker] = station;
    }

    private Worker InstantiateFarmer()
    {
        GameObject workerGO = SpawnWorker();
        Worker worker = workerGO.GetComponent<Worker>();
        Transform field = fields[Random.Range(0, fields.Count - 1)];
        worker.SetWorkingPlace(field);
        worker.SetCurrentTarget(field);
        worker.SetWarehouse(warehouse);
        worker.SetState(Worker.State.MOVING_START);
        return worker;
    }

    internal void AddFarmer()
    {
        if (HasFreeWorkers())
        {
            --inactiveWorkers;
            InstantiateFarmer();
        }
    }

    internal void RemoveFarmer()
    {
        if (activeFarmers.Count > 0)
        {
            activeFarmers.RemoveAt(0);
        }
    }

    //internal void AddWoodcutter()
    //{
    //    throw new NotImplementedException();
    //}

    //internal void RemoveWoodcutter()
    //{
    //    throw new NotImplementedException();
    //}

    //internal void RemoveDruid()
    //{
    //    throw new NotImplementedException();
    //}

    //internal void AddDruid()
    //{
    //    throw new NotImplementedException();
    //}







}
