using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIC_Tower : MonoBehaviour
{
    [SerializeField]
    Tower tower;

    [SerializeField]
    Button repairButton;

    // Start is called before the first frame update
    void Start()
    {
        repairButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tower.needsRepair && !repairButton.IsActive())
        {
            repairButton.gameObject.SetActive(true);
            return;
        }
        else if (!tower.needsRepair && repairButton.IsActive())
        {
            repairButton.gameObject.SetActive(false);
            return;
        }
    }

    public void OnRepairButtonClicked()
    {
        Debug.Log("Clicked");
        tower.Repair();
    }


}
