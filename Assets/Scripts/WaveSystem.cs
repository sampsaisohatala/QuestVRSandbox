using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform[] SpawnPoints;

    [SerializeField] float delayBetweenWaves = 3f;
    [SerializeField] int waveNumber = 1;
    [SerializeField] int enemiesAddedToNextRound = 5;

    int numberOfEnemies;
    int enemiesKilled;

    void Start()
    {
        StartCoroutine(WaitForNextWave());
    }

    IEnumerator WaitForNextWave()
    {
        yield return new WaitForSeconds(delayBetweenWaves);
        StartCoroutine(ActivateWave());
    }

    IEnumerator ActivateWave()
    {
        numberOfEnemies = waveNumber * enemiesAddedToNextRound;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            float random = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(random);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int random = Random.Range(0, SpawnPoints.Length);
        GameObject enemy = Instantiate(EnemyPrefab, SpawnPoints[random].position, SpawnPoints[random].rotation);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled == numberOfEnemies)
            WaveCompleted();
    }

    void WaveCompleted()
    {
        Debug.Log("Wave " + waveNumber + " Completed: " + enemiesKilled);
        enemiesKilled = 0;
        waveNumber++;
        StartCoroutine(WaitForNextWave());
    }
}
