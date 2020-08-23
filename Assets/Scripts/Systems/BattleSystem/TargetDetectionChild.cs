using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetectionChild : MonoBehaviour
{
    // this should be GameObject Type and use Interface to implement notifiers
    UnitController parent;

    private void Start()
    {
        parent = this.GetComponentInParent<UnitController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable damageable;
        if (other.gameObject.TryGetComponent<Damageable>(out damageable))
        {
            parent.OnChildTriggerEnter(damageable);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Damageable damageable;
        if (other.gameObject.TryGetComponent<Damageable>(out damageable))
        {
            parent.OnChildTriggerExit(damageable);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        Damageable damageable;
        if (other.gameObject.TryGetComponent<Damageable>(out damageable))
        {
            if (damageable.gameObject.CompareTag("destroyed"))
            {
                parent.OnChildTriggerExit(damageable);
            }
        }
    }
}
