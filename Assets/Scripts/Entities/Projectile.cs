using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damageable damageable;
        if (collision.gameObject.TryGetComponent<Damageable>(out damageable))
        {
            damageable.DealDamage(damage);
        }
        Destroy(this.gameObject);
    }

    public int getDamage()
    {
        return damage;
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }
}
