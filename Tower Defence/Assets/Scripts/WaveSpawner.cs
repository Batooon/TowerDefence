using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static bool IsWaveIncoming = false;
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 15f;
    public float countdown = 10f;
    private float spawnBetweenEnemies = 0.5f;

    public Text wavecountdownText;

    private int waveNumber = 1;

    void Update()
    {
        if (countdown <= 0f)
        {
            IsWaveIncoming = true;
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        wavecountdownText.text = Mathf.Round(countdown).ToString();
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnBetweenEnemies);
        }
        waveNumber++;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
