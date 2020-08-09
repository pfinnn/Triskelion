using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private string difficulty; // enum

    [SerializeField]
    private Transform worldCenter;

    [SerializeField]
    private float radius;

    private int counter;
    private float timer = 0.0f;
    private float lastWaveTime;
    private bool waveInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        lastWaveTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waveInProgress)
        {
            timer += Time.deltaTime;
            if (timer - lastWaveTime > 10)
            {
                SpawnEnemies();
            }
        }
    }

    void TriggerHornSound()
    {

    }

    void SpawnEnemies()
    {
        // remember last wave time
        timer = 0f;

        // Number to spawn random based on difficulty and (player strength)
        int amountEnemies = 10;
   
        // Random Vector in direction of radius from world center
        for (int i = 0; i < amountEnemies; i++)
        {
            float angle = Random.Range(0, 360f);
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            Vector3 randomVector = new Vector3(x, 0, z);
            Instantiate(enemyPrefab, randomVector, Quaternion.identity); // rotate towards world center
        }

    }
}
