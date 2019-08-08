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


    void Update()
    {
        if (countdown <= 0f)
        {
            if (waveNumber < waves.Length && !isWaveIncoming)
            {
                isWaveIncoming = true;
                StartCoroutine(SpawnWave());
                countdown = waves[waveNumber].countdown;
            }
        }

        countdown -= Time.deltaTime;
        if (!isWaveIncoming)
            wavecountdownText.text = Mathf.CeilToInt(countdown).ToString();
        else
            wavecountdownText.text = 0.ToString();
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < waves[waveNumber].amountOfEnemies; i++)
        {
            SpawnEnemy(i);
            yield return new WaitForSeconds(spawnBetweenEnemies);
        }
        yield return new WaitForSeconds(10f);//TODO: сделать проверку, когда волна заканчивается и врагов нет на поле, чтобы начать отсчет до новой волны
        isWaveIncoming = false;
        waveNumber++;
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