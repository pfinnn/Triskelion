using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Damageable
{

    [SerializeField]
    private GameObject projectilePrefab;

    private List<Collider> enemiesInRange = new List<Collider>();
    private float timer = 0.0f;

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
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5)
        {
            Shoot();
            timer = 0.0f;
        }
    }

    private void Shoot()
    {
        if (enemiesInRange.Count > 0)
        {
            Vector3 targetPosition = enemiesInRange[0].transform.position;
            Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(transform.position-targetPosition));
            currentState = State.Reloading;
        }
    }

    void OnTriggerEnter(Collider other)
    {
       // if (other.gameObject.GetComponent<Bullet>()!=null)
            enemiesInRange.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        enemiesInRange.Remove(other);
    }
}
