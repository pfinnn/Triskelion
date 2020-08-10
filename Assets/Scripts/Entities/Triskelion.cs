using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Triskelion : Damageable
{
    private Material mat;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        mat = GetComponent<MeshRenderer>().material;
        Debug.Log("Max Health: " + GetMaxHealth());
    }

    // Update is called once per frame
    void Update()
    {
        // catch button down to debug damage taken

        if (Input.GetKeyDown(KeyCode.F2)){
            DealDamage(10);
            Debug.Log("Health: " + GetHealth());
            if (GetHealth() <= GetMaxHealth()/2)
            {
                mat.SetColor("_EmissionColor", Color.red);
            }
            // play "damage taken animation"

            // notify UI Manager, should not be handled here
        }

        if (this.GetHealth()==0)
        {
            //TODO
        }
    }
}
