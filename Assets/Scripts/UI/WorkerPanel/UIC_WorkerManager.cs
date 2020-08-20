using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIC_WorkerManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inactiveTextField;
    [SerializeField]
    private TextMeshProUGUI farmerTextField;
    [SerializeField]
    private TextMeshProUGUI woodcutterTextField;
    [SerializeField]
    private TextMeshProUGUI druidsTextField;

    private WorkerManager workerManager;

    List<Worker> workingAtFarms = new List<Worker>();

    private int inactiveWorkers;
    private int farmerWorkers;
    private int woodcutterWorkers;
    private int druidsWorkers;
    // Start is called before the first frame update
    void Start()
    {
        workerManager = GetComponentInParent<WorkerManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateCounts();
        inactiveTextField.text = inactiveWorkers.ToString();
        farmerTextField.text = farmerWorkers.ToString();
        woodcutterTextField.text = woodcutterWorkers.ToString();
        druidsTextField.text = druidsWorkers.ToString();
    }

    private void UpdateCounts()
    {
        int[] counts = workerManager.getWorkerCount();
        inactiveWorkers = counts[0];
        farmerWorkers = counts[1];
        woodcutterWorkers = counts[2];
        druidsWorkers = counts[3];
    }

    public void AddFarmer()
    {
        Debug.Log("Button Add clicked");
        if (inactiveWorkers > 0)
        {
            GameObject worker = workerManager.getOneInactiveWorker();
            workerManager.AssignWorkerToFarmingStation(worker, workerManager.getField());
        }
    }
    public void RemoveFarmer()
    {
        Debug.Log("Button Remove clicked");
        if (farmerWorkers > 0)
        {
            GameObject worker = workerManager.getOneFarmerWorker();
            workerManager.AssignWorkerToFarmingStation(worker, null);
        }
    }
    public void AddWoodcutter()
    {
        if (inactiveWorkers > 0)
        {
            GameObject worker = workerManager.getOneInactiveWorker();
            workerManager.AssignWorkerToFarmingStation(worker, workerManager.getWoodCutterHut());
        }
    }
    public void RemoveWoodcutter()
    {
        if (farmerWorkers > 0)
        {
            GameObject worker = workerManager.getOneWoodcutterWorker();
            workerManager.AssignWorkerToFarmingStation(worker, null);
        }
    }
    public void AddDruid()
    {
        if (inactiveWorkers > 0)
        {
            GameObject worker = workerManager.getOneInactiveWorker();
            workerManager.AssignWorkerToFarmingStation(worker, workerManager.getWoodCutterHut());
        }
    }
    public void RemoveDruid()
    {
        if (farmerWorkers > 0)
        {
            GameObject worker = workerManager.getOneWoodcutterWorker();
            workerManager.AssignWorkerToFarmingStation(worker, null);
        }
    }
}
