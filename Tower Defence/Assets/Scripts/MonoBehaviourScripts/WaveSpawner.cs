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

public enum WaveType
{
    TUTORIALWAVE,
    USUALWAVE,
    INFINITYWAVE
}

public class WaveSpawner : MonoBehaviour , IEnemySpawn, IGenerateWave
{
    public int EnemyIncreaser;

    public int currentAmountOfEnemies;

    public WaypointBase[] spawnWaypoints;

    public GameObject[] defaultEnemies;

    public event Action onWaveStateChanged;
    public event Action WavePassed;

    protected void ActivateWaveStateChangedEvent()
    {
        onWaveStateChanged?.Invoke();
    }

    public int amountOfWaves;
    public int waveIndex;
    public WaveType waveType;

    [HideInInspector]
    public int EnemiesAlive = 0;

    public State state;
    private int nextWaveIndex = 0;
    public Wave _wave;

    [HideInInspector]
    public static int EnemiesKilled;

    public Wave GetIncomingWave()
    {
        return _wave = GenerateWave(nextWaveIndex);
    }

    public bool isWaveIncoming;

    public float countdown;

    void Start()
    {
        EnemiesAlive = 0;
        EnemiesKilled = 0;
        waveIndex = 0;
        _wave = GenerateWave(nextWaveIndex);
        countdown = _wave.Countdown;
    }

    void Update()
    {
        if (Level.singleton.state == GlobalState.TUTORIALPAUSE)
            return;

        onWaveStateChanged?.Invoke();

        if (EnemiesAlive > 0)
            return;

        if (waveIndex == amountOfWaves && waveType != WaveType.INFINITYWAVE)
        {
            Level.singleton.WinGame();
            return;
        }

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

    protected void SpawnWave()
    {
        Wave incomingWave = GetIncomingWave();

        StartCoroutine(incomingWave.SpawnEnemies());
        WavePassed?.Invoke();
    }

    public void SpawnEnemy(WaveSpawnData waveSpawnData)
    {
        EnemiesAlive++;

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
        waveIndex++;

        countdown = _wave.Countdown;
        isWaveIncoming = false;
    }

    public Wave GenerateWave(int waveNumber)
    {
        switch (waveType)
        {
            case WaveType.USUALWAVE:
                return new Wave(WaveFabric.UsualWave(nextWaveIndex, defaultEnemies, spawnWaypoints, currentAmountOfEnemies + EnemyIncreaser++, waveIndex), SpawnEnemy, PrepareNextWave);
            case WaveType.TUTORIALWAVE:
                return new Wave(WaveFabric.TutorialWave(nextWaveIndex, defaultEnemies, spawnWaypoints, currentAmountOfEnemies + EnemyIncreaser++), SpawnEnemy, PrepareNextWave);
            case WaveType.INFINITYWAVE:
                return new Wave(WaveFabric.InfinityWave(nextWaveIndex, defaultEnemies, spawnWaypoints, currentAmountOfEnemies + EnemyIncreaser++), SpawnEnemy, PrepareNextWave);
        }
        return null;
    }
}

public static class WaveFabric
{
    public static WaveData UsualWave(int waveNumber, GameObject[] enemies, WaypointBase[] spawnWaypoints, int amountOfEnemies, int numberWave)
    {
        /*WaveData usualWave = new WaveData();

        usualWave.Enemies = new GameObject[2 * (waveNumber + 1) + 3];
        for (int i = 0; i < usualWave.Enemies.Length; i++)
            usualWave.Enemies[i] = enemies[UnityEngine.Random.Range(0, enemies.Length)];

        usualWave.countdown = waveNumber + 5;

        usualWave.SpawnBetweenEnemies = 0.5f;

        usualWave.StartWaypoints = spawnWaypoints;

        return usualWave;*/

        WaveData usualWave = new WaveData();

        usualWave.Enemies = new GameObject[amountOfEnemies];

        int startIndex = 0;
        int endIndex = Math.Min(numberWave + 1, enemies.Length);
        for (int i = 0; i < usualWave.Enemies.Length; i++)
            usualWave.Enemies[i] = enemies[UnityEngine.Random.Range(startIndex, endIndex)];

        usualWave.countdown = 5f;

        usualWave.SpawnBetweenEnemies = 0.5f;

        usualWave.StartWaypoints = spawnWaypoints;

        return usualWave;
    }

    public static WaveData InfinityWave(int waveNumber, GameObject[] enemies,WaypointBase[] spawnWaypoints,int amountOfEnemies)
    {
        WaveData infinityWave = new WaveData();

        //Создаем массив врагов, длина которого зависит от номера волны
        infinityWave.Enemies = new GameObject[2 * (amountOfEnemies + 1) + 3];
        for (int i = 0; i < infinityWave.Enemies.Length; i++)
            infinityWave.Enemies[i] = enemies[UnityEngine.Random.Range(0, enemies.Length)];

        //отсчет до волны. Тоже зависит от номера волны
        infinityWave.countdown = waveNumber + 5;

        infinityWave.SpawnBetweenEnemies = 0.5f;

        infinityWave.StartWaypoints = spawnWaypoints;

        return infinityWave;
    }

    public static WaveData TutorialWave(int wavenumber,GameObject[] enemies,WaypointBase[] spawnWaypoints,int amountOfEnemies)
    {
        WaveData tutorialWave = new WaveData();

        tutorialWave.Enemies = new GameObject[amountOfEnemies];
        for (int i = 0; i < tutorialWave.Enemies.Length; i++)
            tutorialWave.Enemies[i] = enemies[0];

        tutorialWave.countdown = 5;

        tutorialWave.SpawnBetweenEnemies = 1f;

        tutorialWave.StartWaypoints = spawnWaypoints;

        return tutorialWave;
    }

    //Тут ещё будет метод для спавна волны с боссом и т.п.
}

