using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct WaveSpawnData
{
    GameObject EnemyToSpawn;
    IWayPoint StartWayPoint;

    public WaveSpawnData(GameObject enemy,IWayPoint waypoint)
    {
        EnemyToSpawn = enemy;
        StartWayPoint = waypoint;
    }

    public void Deconstruct(out GameObject enemy, out IWayPoint wayPoint)
    {
        enemy = EnemyToSpawn;
        wayPoint = StartWayPoint;
    }
}

public interface IWave
{
    WaveSpawnData GetNextEnemySpawnData();
}
