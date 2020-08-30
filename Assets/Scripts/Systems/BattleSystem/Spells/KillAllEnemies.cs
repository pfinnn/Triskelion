using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAllEnemies : Spell
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void DoSpell()
    {
        foreach (GameObject soldier in CalculateSolidersInRange())
        {
            if (Vector3.Distance(transform.position, soldier.transform.position) <= range)
            {
                soldier.GetComponent<Damageable>().DealDamage(float.MaxValue);
            }
        }
    }
}
