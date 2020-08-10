using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{

    enum State
    {
        Attacking,
        Moving,
        Dying
    }

    State currentState = State.Attacking;

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            // play death animation
            // play death sound
            // notify wave manager, handles destruction

            
        }
    }


}
