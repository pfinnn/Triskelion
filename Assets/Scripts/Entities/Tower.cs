using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Damageable
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            DealDamage(10);
            if (GetHealth() <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Shoot()
    {

    }
}
