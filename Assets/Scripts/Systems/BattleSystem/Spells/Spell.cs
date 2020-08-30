using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField]
    protected float range = 20;
    // Start is called before the first frame update
    protected List<GameObject> CalculateUnitsInRange()
    {
        List<GameObject> units = new List<GameObject>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20);
        foreach (Collider collider in colliders)
        {
            if (collider.tag.Equals("attackers") && !units.Contains(collider.gameObject))
            {
                units.Add(collider.gameObject);
            }
        }
        return units;
    }

    protected List<GameObject> CalculateSolidersInRange()
    {
        List<GameObject> soldiers = new List<GameObject>();
        foreach (GameObject obj in CalculateUnitsInRange())
        {
            soldiers.AddRange(obj.GetComponentInParent<UnitController>().GetSoldiers());
        }
        return soldiers;
    }

    public abstract void DoSpell();
}
