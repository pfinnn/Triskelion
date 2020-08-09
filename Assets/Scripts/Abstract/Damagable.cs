using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{   
    [SerializeField]
    private int maxHealth;
    private int health;
    // Start is called before the first frame update
    void Start()
    {   
        health = maxHealth;
    }

    public void dealDamage(int amount)
    {
        health -= amount;
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void setHealth(int _health)
    {
        health = _health;
    }
}
