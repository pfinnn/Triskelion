using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Damageable
{
    private void FixedUpdate()
    {
        if (IsDead())
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        Debug.Log(this.name + "dying");
        this.GetComponentInParent<UnitController>().OnSoldierDying(this.gameObject);
        Destroy(this.gameObject);
    }
}
