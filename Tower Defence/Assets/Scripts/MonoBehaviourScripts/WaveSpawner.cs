using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    COUNTDOWN,
    SPAWN,
    END
}
public class WaveSpawner : MonoBehaviour , IEnemySpawn
{
    public event Action onWaveStateChanged;

    public int EnemiesAlive = 0;

    public State state;
    public WaveData[] waves;
    private int nextWaveNumber = 0;
    private Wave _wave;

    public Wave GetIncomingWave()
    {
        return _wave = GenerateWave();
    }

    public bool isWaveIncoming;

    private float countdown;

    void Start()
    {
        countdown = waves[0].countdown;
    }

    void Update()
    {
        onWaveStateChanged?.Invoke();

        if (!isWaveIncoming)
        {
            state = State.COUNTDOWN;
            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            if (nextWaveNumber < waves.Length && countdown <= 0)
            {
                state = State.SPAWN;
                isWaveIncoming = true;
                SpawnWave();
            }
        }
        else
            countdown = 0f;
    }

    private void SpawnWave()
    {
        Wave incomingWave = GetIncomingWave();

        StartCoroutine(incomingWave.SpawnEnemies());

        /*for (int i = 0; i < incomingWave.amountOfEnemies; i++)
        {
            //SpawnEnemy();
            yield return new WaitForSeconds(spawnBetweenEnemies);
        }*/
    }

    public void SpawnEnemy(WaveSpawnData waveSpawnData)
    {
        (GameObject newEnemy, IWayPoint spawnWaypoint) = waveSpawnData;

        GameObject spawnedBuddy = Instantiate(newEnemy, spawnWaypoint.GetWaypointTransform());
        spawnedBuddy.transform.parent = null;
        /*GameObject spawnedBuddy = ObjectPooler.singleton.GetPooledObjects(newEnemy.tag);
        if (spawnedBuddy != null)
        {
            spawnedBuddy.transform.position = spawnPoint.position;
            spawnedBuddy.transform.rotation = spawnPoint.rotation;
            spawnedBuddy.SetActive(true);
        }*/
        Enemy enemy = spawnedBuddy.GetComponent<Enemy>();
        enemy.Init(spawnWaypoint);
    }

    public void PrepareNextWave()
    {
        nextWaveNumber++;

        if (nextWaveNumber < waves.Length)
        {
            countdown = _wave.Countdown;
            isWaveIncoming = false;
        }
        else
            state = State.END;
    }

    public Wave GenerateWave()
    {
        return new Wave(waves[0], SpawnEnemy, PrepareNextWave);
    }
}

