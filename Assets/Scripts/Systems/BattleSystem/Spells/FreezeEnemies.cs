using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeEnemies : Spell
{
    public override void DoSpell()
    {
        StartCoroutine(Work());
    }

    IEnumerator Work()
    {
        List<GameObject> soldiers = CalculateSolidersInRange();
        foreach (GameObject soldier in soldiers)
        {
            soldier.GetComponent<NavMeshAgent>().enabled = false;
        }
        yield return new WaitForSecondsRealtime(5f);
        foreach (GameObject soldier in soldiers)
        {
            NavMeshAgent agent = soldier.GetComponent<NavMeshAgent>();
            agent.enabled = true;
            agent.isStopped = false;
        }
    }

}
