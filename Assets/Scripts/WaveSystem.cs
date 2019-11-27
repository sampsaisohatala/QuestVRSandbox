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

    int enemiesInWave;
    int enemiesKilledInWave = 0;
    int totalEnemiesKilled = 0;

    CarryMenu carryMenu;

    void Start()
    {
        carryMenu = GameManager.Instance.CarryMenu;
        Debug.Log(carryMenu);
        StartCoroutine(WaitForNextWave());
    }

    IEnumerator WaitForNextWave()
    {
        yield return new WaitForSeconds(delayBetweenWaves);
        StartCoroutine(ActivateWave());
    }

    IEnumerator ActivateWave()
    {
        carryMenu.RefreshWaveCount(waveNumber);

        enemiesInWave = waveNumber * enemiesAddedToNextRound;
        for (int i = 0; i < enemiesInWave; i++)
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
        enemiesKilledInWave++;
        totalEnemiesKilled++;
        carryMenu.RefreshKillCount(totalEnemiesKilled);

        if (enemiesKilledInWave == enemiesInWave)
            WaveCompleted();
    }

    void WaveCompleted()
    {
        Debug.Log("Wave " + waveNumber + " Completed: " + enemiesKilledInWave);
        enemiesKilledInWave = 0;
        waveNumber++;
        StartCoroutine(WaitForNextWave());
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent blue wirecube at the transforms position
        Gizmos.color = Color.blue;
        foreach (var point in SpawnPoints)
        {
            Gizmos.DrawWireCube(point.position + Vector3.up, new Vector3(1, 2, 1));
        }
        
    }
}
