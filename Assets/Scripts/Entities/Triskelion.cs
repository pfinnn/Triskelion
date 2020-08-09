using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triskelion : Damagable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.getHealth()==0)
        {
            EventManager.current.eventGameOver.Invoke();
        }
    }
}
