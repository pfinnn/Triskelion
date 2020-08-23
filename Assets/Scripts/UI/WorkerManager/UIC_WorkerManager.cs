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

    [SerializeField]
    WorkerManager workerManager;

    public void AddFarmer()
    {
        workerManager.AddFarmer();
    }
    public void RemoveFarmer()
    {
        workerManager.RemoveFarmer();
    }
    public void AddWoodcutter()
    {
        //workerManager.AddWoodcutter();
    }
    public void RemoveWoodcutter()
    {
        //workerManager.RemoveWoodcutter();
    }
    public void AddDruid()
    {
        //workerManager.AddDruid();
    }
    public void RemoveDruid()
    {
        //workerManager.RemoveDruid();
    }
}
