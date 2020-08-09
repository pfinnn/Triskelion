using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Triskelion : Damageable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // catch button down to debug damage taken

        if (Input.GetKeyDown(KeyCode.Backspace)){
            dealDamage(10);
            // play "damage taken animation"

            // notify UI Manager, should not be handled here
        }

        if (this.getHealth()==0)
        {
            //Game Over Event
        }
    }
}
