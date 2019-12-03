using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct WaveData
{
    public float countdown;

    public GameObject[] Enemies;

    public float SpawnBetweenEnemies;

    public WaypointBase[] StartWaypoints;
}

[Serializable]
public class Wave : IWave
{
    int i;
    public Wave(WaveData data, Action<WaveSpawnData> spawnEnemyCallbackFunction, Action prepareNextWaveCallback)
    {
        waveData = data;
        SpawnEnemyCallback = spawnEnemyCallbackFunction;
        NextWaveCallback = prepareNextWaveCallback;
    }

    WaveData waveData;

    private Action<WaveSpawnData> SpawnEnemyCallback;
    private Action NextWaveCallback;

    int index = 0;

    public float Countdown
    {
        get => waveData.countdown;
    }

    public int amountOfEnemies
    {
        get => waveData.Enemies.Length;
    }

    public WaveSpawnData GetNextEnemySpawnData()
    {
        i = UnityEngine.Random.Range(0, waveData.StartWaypoints.Length);
        return new WaveSpawnData(waveData.Enemies[index++], waveData.StartWaypoints[i]);
    }

    public IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < amountOfEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waveData.SpawnBetweenEnemies);
        }
        NextWaveCallback();
    }

    public void SpawnEnemy()
    {
        SpawnEnemyCallback(GetNextEnemySpawnData());
    }
}
