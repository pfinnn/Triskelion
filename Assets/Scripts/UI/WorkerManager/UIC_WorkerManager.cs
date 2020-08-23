﻿using System.Collections;
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

    [SerializeField]
    WorkerManager workerManager;

    private void Start()
    {
        inactiveTextField.text = workerManager.GetInactiveWorkerCount().ToString();
        farmerTextField.text = workerManager.GetFarmersCount().ToString();
        woodcutterTextField.text = workerManager.GetWoodcutterCount().ToString();
        druidsTextField.text = workerManager.GetDruidsCount().ToString();
    }

    public void AddFarmer()
    {
        workerManager.AddFarmer();
        inactiveTextField.text = workerManager.GetInactiveWorkerCount().ToString();
        farmerTextField.text = workerManager.GetFarmersCount().ToString();
    }
    public void RemoveFarmer()
    {
        workerManager.RemoveFarmer();
        inactiveTextField.text = workerManager.GetInactiveWorkerCount().ToString();
        farmerTextField.text = workerManager.GetFarmersCount().ToString();
    }
    public void AddWoodcutter()
    {
        workerManager.AddWoodcutter();
        inactiveTextField.text = workerManager.GetInactiveWorkerCount().ToString();
        woodcutterTextField.text = workerManager.GetWoodcutterCount().ToString();
    }
    public void RemoveWoodcutter()
    {
        workerManager.RemoveWoodcutter();
        inactiveTextField.text = workerManager.GetInactiveWorkerCount().ToString();
        woodcutterTextField.text = workerManager.GetWoodcutterCount().ToString();
    }
    public void AddDruid()
    {
        workerManager.AddDruid();
        inactiveTextField.text = workerManager.GetInactiveWorkerCount().ToString();
        druidsTextField.text = workerManager.GetDruidsCount().ToString();
    }
    public void RemoveDruid()
    {
        workerManager.RemoveDruid();
        inactiveTextField.text = workerManager.GetInactiveWorkerCount().ToString();
        druidsTextField.text = workerManager.GetDruidsCount().ToString();
    }
}
