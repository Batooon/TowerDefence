﻿using System;
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
public class WaveSpawner : MonoBehaviour
{
    public event Action onWaveStateChanged;

    public int EnemiesAlive = 0;

    public State state;
    public Wave[] waves;
    private int waveNumber = 0;

    //public Transform enemyPrefab;


    public bool isWaveIncoming;

    public float countdown;
    private float spawnBetweenEnemies = 0.5f;


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

            if (waveNumber < waves.Length && countdown <= 0)
            {
                state = State.SPAWN;
                isWaveIncoming = true;
                StartCoroutine(SpawnWave());
            }
        }
        else
            countdown = 0f;
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < waves[waveNumber].amountOfEnemies; i++)
        {
            SpawnEnemy(i);
            yield return new WaitForSeconds(spawnBetweenEnemies);
        }

        waveNumber++;

        if (waveNumber < waves.Length)
        {
            countdown = waves[waveNumber].countdown;
            isWaveIncoming = false;
        }
        else
            state = State.END;
    }

    private void SpawnEnemy(int index)
    {
        GameObject newEnemy = waves[waveNumber].GetNextEnemy(index);

        Transform spawnPoint = waves[waveNumber].StartWayPoints[0].transform;

        GameObject spawnedBuddy = Instantiate(newEnemy, spawnPoint);
        spawnedBuddy.transform.parent = null;
        /*GameObject spawnedBuddy = ObjectPooler.singleton.GetPooledObjects(newEnemy.tag);
        if (spawnedBuddy != null)
        {
            spawnedBuddy.transform.position = spawnPoint.position;
            spawnedBuddy.transform.rotation = spawnPoint.rotation;
            spawnedBuddy.SetActive(true);
        }*/
        Enemy enemy = spawnedBuddy.GetComponent<Enemy>();
        enemy.Init(waves[waveNumber].StartWayPoints[0]);

        EnemiesAlive++;
    }

}

[Serializable]
public class Wave
{
    public float countdown;
    public int amountOfEnemies
    {
        get => waveData.Length;
    }
    public GameObject[] waveData;

    public GameObject GetNextEnemy(int index) => waveData[index];

    public WaypointBase[] StartWayPoints;


}