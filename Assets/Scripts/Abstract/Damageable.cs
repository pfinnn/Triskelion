using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int health;
    // Start is called before the first frame update
    public virtual void Start()
    {
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if(isDead())
        {
            Destroy(this);
        }
    }

    public void DealDamage(int amount)
    {
        health -= amount;
    }

    public void Repair(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }

    public bool isDead()
    {
        return (health <= 0);
    }

    public void OnDestroyedEvent()
    {
        //TODO Display Death Animation
        throw new NotImplementedException();
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int health)
    {
        this.health = health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void setMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }
}
