using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    COUNTDOWN,
    SPAWN,
    END
}
public class WaveSpawner : MonoBehaviour , IEnemySpawn, IGenerateWave
{
    public WaypointBase[] spawnWaypoints;

    public GameObject[] defaultEnemies;

    public event Action onWaveStateChanged;

    //private int EnemiesAlive = 0;

    public State state;
    private int nextWaveIndex = 0;
    private Wave _wave;

    public Wave GetIncomingWave()
    {
        return _wave = GenerateWave(nextWaveIndex);
    }

    public bool isWaveIncoming;

    public float countdown;

    void Start()
    {
        _wave = GenerateWave(nextWaveIndex);
        countdown = _wave.Countdown;//waves[0].countdown;
    }

    void Update()
    {
        onWaveStateChanged?.Invoke();

        if (!isWaveIncoming)
        {
            state = State.COUNTDOWN;
            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            if (countdown <= 0)
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
        nextWaveIndex++;

        countdown = _wave.Countdown;
        isWaveIncoming = false;
    }

    public Wave GenerateWave(int waveNumber)
    {
        return new Wave(WaveFabric.UsualWave(nextWaveIndex, defaultEnemies, spawnWaypoints), SpawnEnemy, PrepareNextWave);
    }
}

public static class WaveFabric
{
    public static WaveData UsualWave(int waveNumber, GameObject[] enemies, WaypointBase[] spawnWaypoints)
    {
        WaveData usualWave = new WaveData();

        //Создаем массив врагов, длина которого зависит от номера волны
        usualWave.Enemies = new GameObject[2 * (waveNumber + 1) + 3];
        for (int i = 0; i < usualWave.Enemies.Length; i++)
            usualWave.Enemies[i] = enemies[UnityEngine.Random.Range(0, enemies.Length)];

        //отсчет до волны. Тоже зависит от номера волны
        usualWave.countdown = waveNumber + 5;

        usualWave.SpawnBetweenEnemies = 0.5f;

        usualWave.StartWaypoints = spawnWaypoints;

        return usualWave;
    }

    /*public WaveData BuildBossWave()
    {

    }*/
}

