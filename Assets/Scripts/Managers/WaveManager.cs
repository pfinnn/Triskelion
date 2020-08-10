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

    private int waveCounter;
    private float timer = 0.0f;
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
        if (!WaveInProgress())
        {
            timer += Time.deltaTime;
            if (timer - lastWaveTime > baseWaveInterval)
            {
                waveCounter += 1;
                Debug.Log("New Wave Number: " + waveCounter);
                int amountEnemies = CalculateEnemiesAmount();
                SpawnEnemies(amountEnemies);
                ResetTimer();
            }
        }
    }

    private int CalculateEnemiesAmount()
    {
        // do something smart here with wave counter, set difficulty, player strength and randomness
        return Random.Range(0, waveCounter) + waveCounter + 3;
    }

    bool WaveInProgress()
    {
        return spawnedEnemies.Count != 0;
    }

    void ResetTimer()
    {
        timer = 0f;
    }

    void TriggerHornSound()
    {

    }


    void SpawnEnemies(int amountEnemies)
    {
        // save positions to check for overlaps
        List<Vector3> usedPositions = new List<Vector3>();

        // Random Vector in direction of radius from world center
        for (int i = 0; i < amountEnemies; i++)
        {
            float angle, x , z , randomX, randomZ;
            int randomDistanceMax = (waveCounter / 2) + 10;
            Vector3 randomVector = Vector3.zero;

            bool foundValidPosition = false;
            while (!foundValidPosition)
            {
                // Random Position on Circle
                angle = Random.Range(0, 360f);
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                // Add Random Distance
                randomX = Random.Range(0, randomDistanceMax);
                randomZ = Random.Range(0, randomDistanceMax);
                // Random Position
                randomVector = new Vector3(x + randomX, 0, z + randomZ);
                foundValidPosition = ValidPosition(randomVector, usedPositions);
            }
            // TODO
            // should never be thrown, error would actually result in endless loop. fix this by adding a max tries for the while loop
            if (randomVector == Vector3.zero) throw new ArgumentNullException("Could not find a random Vector to spawn enemy");
            // Calculate Rotation towards Triskelion and Spawn Enemies
            Vector3 lookVector = worldCenter.position - randomVector;
            Quaternion rotation = Quaternion.LookRotation(lookVector);
            spawnedEnemies.Add(Instantiate(enemyPrefab, randomVector, rotation)); // rotate towards world center
        }
        Debug.Log("Spawned " + amountEnemies + " Enemies");
    }

    bool ValidPosition(Vector3 randomVector, List<Vector3> usedPositions)
    {
        if (usedPositions.Count == 0) return true;
        foreach (Vector3 pos in usedPositions)
        {
            if (randomVector.Equals(pos)) return true;
            float distance = Vector3.Distance(randomVector, pos);
            if (distance < 10) return true;
        }
        return false;
    }

    void OnEnemyDied(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }
}
