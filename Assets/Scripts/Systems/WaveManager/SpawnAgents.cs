using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAgents : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    float distanceBetweenAgents = 10;

    [SerializeField]
    int amountAgents = 3;

    [SerializeField]
    Transform target;

    [SerializeField]
    float heightMargin = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAgentsInLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAgentsInLine()
    {
        for (int i = 0; i < amountAgents; i++)
        {
            Vector3 enemyPosition = new Vector3(transform.position.x + i * distanceBetweenAgents, transform.position.y, transform.position.z);
            GameObject enemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation);
            //enemy.GetComponent<Enemy>().SetTarget(target);
            enemy.GetComponent<ObstacleAvoidance>().SetTarget(target);
            enemy.transform.SetParent(transform);

        }



    }

}
