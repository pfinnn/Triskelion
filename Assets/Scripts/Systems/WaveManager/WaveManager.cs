using QFSW.MOP2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform worldCenter;

    [SerializeField]
    private float radius = 50;

    [SerializeField]
    private int baseWaveInterval = 10;

    [SerializeField]
    private int waveCounter = 0;

    [SerializeField]
    private Transform terrainTransform;

    [SerializeField]
    float heightDifferenceSpawning = 20f;

    [SerializeField]
    float distanceBetweenAgents = 10;

    [SerializeField]
    float distanceBetweenCircles = 10;

    private float waveTimer = 0.0f;
    private float lastWaveTime;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        lastWaveTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            KillWave();
        }
 
        if (!WaveInProgress())
        {
            waveTimer += Time.deltaTime;
            if (waveTimer - lastWaveTime > baseWaveInterval)
            {
                waveCounter += 1;
                Debug.Log("asWave Number: " + waveCounter);
                int amountEnemies = CalculateEnemiesAmount();
                SpawnEnemies(amountEnemies);
                ResetWaveTimer();
            }
        }
    }

    private void KillWave()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();
    }

    private int CalculateEnemiesAmount()
    {
        // do something smart here with wave counter, set difficulty, player strength and randomness
        int difficultyMultiplier = Mathf.RoundToInt(waveCounter / 5)+1;
        return (Random.Range(waveCounter, waveCounter + 3 * difficultyMultiplier)); // needs more variation
    }

    bool WaveInProgress()
    {
        return spawnedEnemies.Count != 0;
    }

    void ResetWaveTimer()
    {
        waveTimer = 0f;
    }

    void TriggerHornSound()
    {

    }


    // TODO use Spawn Entities pre instantiated (or spawned by this loop) and place in the world handling the spawning locally

    void SpawnEnemies(int amountEnemies)
    {
        int placedEnemies = 0;
        int counterCircle = 0;

        while ( placedEnemies <= amountEnemies )
        {
            ++counterCircle;
            int segments = Mathf.RoundToInt(360 / distanceBetweenAgents);
            float marginCircles = counterCircle* distanceBetweenCircles;

            for (int s = 0; s < segments; s++)
            {
                if (placedEnemies >= amountEnemies) break;

                float height = terrainTransform.position.y + heightDifferenceSpawning;
                var rad = Mathf.Deg2Rad * (s * 360f / segments - 1);
                Vector3 enemyPosition = new Vector3((Mathf.Sin(rad) * (radius + marginCircles)), height, (Mathf.Cos(rad) * (radius + marginCircles)));
                Vector3 lookVector = worldCenter.position - enemyPosition;
                Quaternion rotation = Quaternion.LookRotation(lookVector);

                GameObject enemy = Instantiate(enemyPrefab, enemyPosition, rotation);
                enemy.GetComponent<UnitController>().SetTarget(worldCenter.position);
                enemy.transform.SetParent(transform);
                spawnedEnemies.Add(enemy);

                ++placedEnemies;
            }
            if (placedEnemies >= amountEnemies) break;
        }
        Debug.Log("Spawned " + placedEnemies + " Enemies in "+counterCircle+" Circles");
    }

    bool ValidPosition(Vector3 randomVector, List<Vector3> usedPositions)
    {
        if (usedPositions.Count == 0) return true;

        bool positionIsValid = true;

        foreach (Vector3 pos in usedPositions)
        {
            if (randomVector.Equals(pos))
            {
                return false;
            }
            float distance = Vector3.Distance(randomVector, pos);
            if (distance < 5)
            {
                return false;
            }
        }
        return positionIsValid;
    }

    void OnEnemyDied(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        Destroy(enemy);
    }
}
