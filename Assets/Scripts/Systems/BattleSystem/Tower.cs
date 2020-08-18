using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Damageable
{
    private ShootingSystem shootingSystem;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float timer = 0.0f;
    private float _health = 3000;

    enum State
    {
        Idle,
        Attacking,
        Upgrading,
        Reloading
    }

    State currentState = State.Idle;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        shootingSystem = GetComponent<ShootingSystem>();
        this.gameObject.tag = "damageable";
        this.gameObject.tag = "defenders";
        SetMaxHealth(_health);
        SetHealth(_health);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            Shoot();
            timer = 0.0f;
        }
    }

    private void Shoot()
    {
        if (enemiesInRange.Count > 0)
        {
            if(enemiesInRange[0]==null)
            {
                enemiesInRange.RemoveAt(0);
            }
            //shootingSystem.LaunchProjectileWithArcMovingTarget(enemiesInRange[0].transform, 10);
        }
    }

    public List<GameObject> GetEnemiesInRange()
    {
        return enemiesInRange;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<UnitController>())
            enemiesInRange.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<UnitController>())
            enemiesInRange.Remove(other.gameObject);
    }
    
}
