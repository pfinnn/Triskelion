using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventManager : MonoBehaviour
{
    public static EventManager current;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }
}
