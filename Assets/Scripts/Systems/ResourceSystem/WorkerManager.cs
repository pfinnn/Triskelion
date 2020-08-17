﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<GameObject, GameObject> workers = new Dictionary<GameObject, GameObject>();

    [SerializeField]
    private int amountOfSpawningWorkers = 10;
    [SerializeField]
    private GameObject workerPrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform warehouse;

    [SerializeField]
    private List<GameObject> fields = new List<GameObject>();
    [SerializeField]
    private List<GameObject> woodcutterHuts = new List<GameObject>();
    [SerializeField]
    private List<GameObject> droidWorkplaces = new List<GameObject>();

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
            NavMeshHit hit;
            NavMesh.SamplePosition(spawnPoint.position, out hit, 500f, NavMesh.AllAreas);
            GameObject worker = Instantiate(workerPrefab, hit.position, Quaternion.identity);
            worker.transform.SetParent(this.transform);
            workers.Add(worker, null);
        }
    }

    public void AssignWorkerToFarmingStation(GameObject worker, GameObject station)
    {
        Worker workerComponent = worker.GetComponent<Worker>();
        workerComponent.setProfession(station == null ? Warehouse.resourceType.NONE : station.GetComponent<FarmingStation>().GetResource());
        workerComponent.setWorkingPlace(station);
        workers[worker] = station;
    }

    internal GameObject getOneInactiveWorker()
    {
        foreach  (GameObject worker in workers.Keys)
        {
            if (workers[worker] == null)
                return worker;
        }
        return null;
    }
    internal GameObject getOneFarmerWorker()
    {
        foreach  (GameObject worker in workers.Keys)
        {
            if (worker.GetComponent<Worker>().getProfession() == Warehouse.resourceType.FOOD)
                return worker;
        }
        return null;
    }
    internal GameObject getOneWoodcutterWorker()
    {
        foreach  (GameObject worker in workers.Keys)
        {
            if (worker.GetComponent<Worker>().getProfession() == Warehouse.resourceType.WOOD)
                return worker;
        }
        return null;
    }
    internal GameObject getOneDroidWorker()
    {
        foreach  (GameObject worker in workers.Keys)
        {
            if (worker.GetComponent<Worker>().getProfession() == Warehouse.resourceType.FEIDH)
                return worker;
        }
        return null;
    }

    public int[] getWorkerCount()
    {
        int i = 0;
        int f = 0;
        int w = 0;
        int d = 0;
        foreach (GameObject worker in workers.Keys)
        {
            if (workers[worker] == null)
            {
                i++;
            } else if (workers[worker].GetComponent<FarmingStation>().GetResource().Equals(Warehouse.resourceType.FOOD)) {
                f++;
            } else if (workers[worker].GetComponent<FarmingStation>().GetResource().Equals(Warehouse.resourceType.WOOD)) {
                w++;
            } else if (workers[worker].GetComponent<FarmingStation>().GetResource().Equals(Warehouse.resourceType.FEIDH)) {
                d++;
            }

        }

        int[] result = {i, f, w, d};
        return result;
    }

    public GameObject getField()
    {
        return fields[UnityEngine.Random.Range(0, fields.Count)];
    }

    public GameObject getWoodCutterHut()
    {
        return woodcutterHuts[UnityEngine.Random.Range(0, woodcutterHuts.Count)];
    }
    public GameObject getDroidWorkplaces()
    {
        return droidWorkplaces[UnityEngine.Random.Range(0, droidWorkplaces.Count)];
    }

    public Transform getWarehouseTransform()
    {
        return warehouse;
    }
}
