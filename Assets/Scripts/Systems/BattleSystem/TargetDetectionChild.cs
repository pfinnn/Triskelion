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
        parent.OnChildTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parent.OnChildTriggerExit(other);
    }


}
