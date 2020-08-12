using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Text foodUI;
    [SerializeField]
    private Text woodUI;
    [SerializeField]
    private Text feidhUI;
    private int foodAmount { get; set; }
    private int woodAmount { get; set; }
    private int feidhAmount { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        foodAmount = 0;
        woodAmount = 0;
        feidhAmount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foodAmount++;
        woodAmount++;
        feidhAmount++;
        foodUI.text = foodAmount.ToString();
        woodUI.text = woodAmount.ToString();
        feidhUI.text = feidhAmount.ToString();
    }
}
