using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float health;

    // Start is called before the first frame update
    public virtual void Start()
    {
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if(IsDead())
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        Destroy(this);
    }

    public void DealDamage(float amount)
    {
        //Debug.Log(this.name + " received Damage " + amount);
        health -= amount;
    }

    public void Repair(float amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }

    public void CompleteRepair()
    {
        float price = (maxHealth - health) / 15;
        ResourceManager resourceManager = Camera.main.GetComponentInParent<ResourceManager>();
        if (resourceManager.Buy(ResourceManager.Resource.WOOD, (int) price))
        {
            health = maxHealth;
        }
    }

    public bool IsDead()
    {
        return (health <= 0);
    }

    public void OnDestroyedEvent()
    {
        //TODO Display Death Animation
        throw new NotImplementedException();
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public bool IsDamaged()
    {
        return health < maxHealth;
    }
}
