using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradeable : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    private Damageable dmg;

    public enum items
    {
        HEALTH, PROJECTILE
    }
    // Start is called before the first frame update
    void Start()
    {
        dmg = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade(items upgradeType, int addAmount, Inventory.resourceType resourceType , int price)
    {
        switch(upgradeType)
        {
            case items.HEALTH:
                if (inventory.Buy(resourceType, price))
                    dmg.setHealth(dmg.getMaxHealth() + addAmount);
                break;

            case items.PROJECTILE:
                if (inventory.Buy(resourceType, price))
                {
                    Projectile projectile = dmg.GetComponent<Projectile>();
                    projectile.setDamage(projectile.getDamage() + addAmount);
                }
                break;
        }
    }
}
