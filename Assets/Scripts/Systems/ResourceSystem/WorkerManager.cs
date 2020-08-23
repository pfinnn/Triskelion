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
    private GameObject workerPrefab;

    [SerializeField]
    private Transform triskelion; // triskelion

    [SerializeField]
    private List<Transform> fields = new List<Transform>();

    [SerializeField]
    private List<Transform> lumberCamps = new List<Transform>();

    private int inactiveWorkers = 25;
    private List<Worker> activeFarmers = new List<Worker>();
    private List<Worker> activeWoodcutters = new List<Worker>();
    private List<Worker> activeDruids = new List<Worker>();

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnWorkers(int amount, Vector3 spawnPoint)
    {
        for (int i = 0; i < amount; i++)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(spawnPoint, out hit, 10f, NavMesh.AllAreas);
            GameObject worker = Instantiate(workerPrefab, hit.position, Quaternion.identity);
            worker.transform.SetParent(this.transform);
            ++inactiveWorkers;
        }
    }

    private GameObject SpawnWorker(Vector3 spawnPoint)
    {
        GameObject worker = Instantiate(workerPrefab, spawnPoint, Quaternion.identity);
        worker.transform.SetParent(this.transform);
        //--inactiveWorkers;
        return worker;
    }

    private GameObject ArrangeWorkers(ResourceManager.Resource profession, Vector3 workingPlacePos)
    {
        GameObject worker = null;
        Vector3 spawnPoint;
        switch (profession)
        {
            case ResourceManager.Resource.FOOD:
                spawnPoint = new Vector3(
                    workingPlacePos.x+Random.Range(-10 ,10), 
                    workingPlacePos.y, 
                    workingPlacePos.z + Random.Range(-10, 10));
                worker = SpawnWorker(spawnPoint);
                return worker;
            case ResourceManager.Resource.WOOD:
                spawnPoint = new Vector3(
                    workingPlacePos.x + Random.Range(-6, 6),
                    workingPlacePos.y,
                    workingPlacePos.z + Random.Range(-6, 6));
                worker = SpawnWorker(spawnPoint);
                return worker;
            case ResourceManager.Resource.FEIDH:
                float radius = 16;
                spawnPoint = new Vector3(triskelion.position.x + Random.Range(-radius, radius),
                    triskelion.position.y, triskelion.position.z +
                    Random.Range(-radius, radius));
                worker = SpawnWorker(spawnPoint);
                return worker;
        }
        return worker;
    }

    private bool HasFreeWorkers()
    {
        return inactiveWorkers > 0;
    }

    private Worker InstantiateFarmer()
    {
        Transform field = fields[Random.Range(0, fields.Count - 1)];
        GameObject workerGO = ArrangeWorkers(ResourceManager.Resource.FOOD, field.position);
        Worker worker = workerGO.GetComponent<Worker>();

        worker.SetWorkingPlace(field);
        worker.SetState(Worker.State.COLLECTING_RESOURCE);
        return worker;
    }

    private Worker InstantiateWoodcutter()
    {
        Transform camp = lumberCamps[Random.Range(0, lumberCamps.Count - 1)];
        GameObject workerGO = ArrangeWorkers(ResourceManager.Resource.WOOD, camp.position);
        Worker worker = workerGO.GetComponent<Worker>();

        worker.SetWorkingPlace(camp);
        worker.SetState(Worker.State.COLLECTING_RESOURCE);
        return worker;
    }

    private Worker InstantiateDruid()
    {
        GameObject workerGO = ArrangeWorkers(ResourceManager.Resource.FEIDH, triskelion.position);
        Worker worker = workerGO.GetComponent<Worker>();
        worker.SetWorkingPlace(triskelion);
        worker.SetState(Worker.State.COLLECTING_RESOURCE);
        return worker;
    }

    internal void AddFarmer()
    {
        if (HasFreeWorkers())
        {
            activeFarmers.Add(InstantiateFarmer());
            --inactiveWorkers;
        }
    }

    internal void RemoveFarmer()
    {
        if (activeFarmers.Count > 0)
        {
            Destroy(activeFarmers[0].gameObject);
            activeFarmers.RemoveAt(0);
            ++inactiveWorkers;
        }
    }

    internal void AddWoodcutter()
    {
        if (HasFreeWorkers())
        {
            activeWoodcutters.Add(InstantiateWoodcutter());
            --inactiveWorkers;
        }
    }

    internal void RemoveWoodcutter()
    {
        if (activeWoodcutters.Count > 0)
        {
            Destroy(activeWoodcutters[0].gameObject);
            activeWoodcutters.RemoveAt(0);
            ++inactiveWorkers;
        }
    }

    internal void AddDruid()
    {
        if (HasFreeWorkers())
        {
            activeDruids.Add(InstantiateDruid());
            --inactiveWorkers;
        }
    }

    internal void RemoveDruid()
    {
        if (activeDruids.Count > 0)
        {
            Destroy(activeDruids[0].gameObject);
            activeDruids.RemoveAt(0);
            ++inactiveWorkers;
        }
    }

    internal int GetInactiveWorkerCount()
    {
        return inactiveWorkers;
    }

    internal int GetFarmersCount()
    {
        return activeFarmers.Count;
    }

    internal int GetWoodcutterCount()
    {
        return activeWoodcutters.Count;
    }

    internal int GetDruidsCount()
    {
        return activeDruids.Count;
    }
}
