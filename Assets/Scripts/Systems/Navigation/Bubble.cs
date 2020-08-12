using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public string side = "center";

    private void OnTriggerStay(Collider other)
    {
        this.GetComponentInParent<BubbleHandler>().HandleCollisionFromBubble(other, this);
    }
}
