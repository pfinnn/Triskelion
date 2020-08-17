using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkerGUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inactiveTextField;
    [SerializeField]
    private TextMeshProUGUI farmerTextField;
    [SerializeField]
    private TextMeshProUGUI woodcutterTextField;
    [SerializeField]
    private TextMeshProUGUI druidsTextField;

    [SerializeField]
    private UnityEngine.UI.Button addFarmerButton;
    [SerializeField]
    private UnityEngine.UI.Button removeFarmerButton;
        /**
    [SerializeField]
    private Button addWoodcutterButton;
    [SerializeField]
    private Button removeWoodcutterButton;
    [SerializeField]
    private Button addDroidButton;
    [SerializeField]
    private Button removeDroidButton;
    */

    private WorkerManager workerManager;

    private int inactiveWorkers;
    private int farmerWorkers;
    private int woodcutterWorkers;
    private int droidsWorkers;
    // Start is called before the first frame update
    void Start()
    {
        workerManager = GetComponentInParent<WorkerManager>();
        /**
        addFarmerButton.onClick.AddListener(AddFarmer);
        removeFarmerButton.onClick.AddListener(RemoveFarmer);
        addWoodcutterButton.onClick.AddListener(AddWoodcutter);
        removeWoodcutterButton.onClick.AddListener(RemoveWoodcutter);
        addDroidButton.onClick.AddListener(AddDroid);
        removeDroidButton.onClick.AddListener(RemoveDroid);
    */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateCounts();
        inactiveTextField.text = inactiveWorkers.ToString();
        farmerTextField.text = farmerWorkers.ToString();
        woodcutterTextField.text = woodcutterWorkers.ToString();
        druidsTextField.text = droidsWorkers.ToString();
    }

    private void UpdateCounts()
    {
        int[] counts = workerManager.getWorkerCount();
        inactiveWorkers = counts[0];
        farmerWorkers = counts[1];
        woodcutterWorkers = counts[2];
        droidsWorkers = counts[3];
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
    public void AddDroid()
    {
        if (inactiveWorkers > 0)
        {
            GameObject worker = workerManager.getOneInactiveWorker();
            workerManager.AssignWorkerToFarmingStation(worker, workerManager.getWoodCutterHut());
        }
    }
    public void RemoveDroid()
    {
        if (farmerWorkers > 0)
        {
            GameObject worker = workerManager.getOneWoodcutterWorker();
            workerManager.AssignWorkerToFarmingStation(worker, null);
        }
    }
}
