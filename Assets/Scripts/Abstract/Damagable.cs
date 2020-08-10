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

    public void DealDamage(int amount)
    {
        health -= amount;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealth(int _health)
    {
        health = _health;
    }
}
