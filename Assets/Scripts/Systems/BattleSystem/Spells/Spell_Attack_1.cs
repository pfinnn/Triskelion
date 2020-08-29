using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Attack_1 : MonoBehaviour
{
    [SerializeField]
    private float range = 20;
    private List<GameObject> units = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DoSpell(Vector3 mousePosition)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20);
        foreach (Collider collider in colliders)
        {
            if (collider.tag.Equals("attackers") && !units.Contains(collider.gameObject))
            {
                units.Add(collider.gameObject);
            }
        }
        foreach (GameObject unit in units)
        {
            List<GameObject> soldiers = unit.GetComponentInParent<UnitController>().GetSoldiers();
            foreach (GameObject soldier in soldiers)
            {
                if (Vector3.Distance(mousePosition, soldier.transform.position) <= GetComponent<SphereCollider>().radius)
                {
                    soldier.GetComponent<Damageable>().DealDamage(float.MaxValue);
                }
            }
        }
    }
}
