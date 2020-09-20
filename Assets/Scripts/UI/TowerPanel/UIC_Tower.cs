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

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        //repairButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tower.IsDamaged())
        {
            if (!canvas.enabled)
            {
                canvas.enabled = true;
            }

            //if (!repairButton.IsActive())
            //{
            //    repairButton.gameObject.SetActive(true);
            //}
        } else
        {
            if (canvas.enabled)
            {
                canvas.enabled = false;
            }
            //if (repairButton.IsActive())
            //{
            //    repairButton.gameObject.SetActive(false);
            //}
        }
    }

    public void OnRepairButtonClicked()
    {
        if (tower.IsDamaged())
        {
            Debug.Log("Repair");
            tower.Repair();
        }
        audioManager.PlayUISound(AudioManager.UISoundTypes.ButtonClicked);
    }


}
