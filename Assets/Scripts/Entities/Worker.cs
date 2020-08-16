using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Damageable
{
    [SerializeField]
    private Inventory.resourceType profession = Inventory.resourceType.NONE;
    [SerializeField]
    private int maxStorage = 10;
    private int currentStorage = 0;

    private FarmingStation workingPlace = null;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        workingPlace.removeWorker(this);
    }

    public int getMaxStorage()
    {
        return maxStorage;
    }

    public void setMaxStorage(int amount)
    {
        maxStorage = amount;
    }
    public int getCurrentStorage()
    {
        return currentStorage;
    }

    public void setCurrentStorage(int amount)
    {
        currentStorage = amount;
    }

    public FarmingStation getWorkungPlace()
    {
        return workingPlace;
    }

    public void setWorkingPlace(FarmingStation place)
    {
        workingPlace = place;
    }
}
