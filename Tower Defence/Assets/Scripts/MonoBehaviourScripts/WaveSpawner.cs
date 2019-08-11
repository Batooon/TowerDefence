using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    private int waveNumber = 0;

    public Transform enemyPrefab;

    public Transform spawnPoint;

    public bool isWaveIncoming;

    public float countdown;
    private float spawnBetweenEnemies = 0.5f;

    public Text wavecountdownText;

    void Start()
    {
        countdown = waves[0].countdown;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 0;
        }

        if (!isWaveIncoming)
        {
            countdown -= Time.deltaTime;

            if (waveNumber < waves.Length && countdown < 0)
            {
                isWaveIncoming = true;
                StartCoroutine(SpawnWave());
            }
        }
        else
            countdown = 0f;

        wavecountdownText.text = Mathf.CeilToInt(countdown).ToString();
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
    }

    private void SpawnEnemy(int index)
    {
        GameObject newEnemy = waves[waveNumber].GetNextEnemy(index);

        Instantiate(newEnemy, spawnPoint.position, spawnPoint.rotation);
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
}