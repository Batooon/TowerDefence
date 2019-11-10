using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Health;

    protected Vector3 target;
    private IWayPoint _nextWaypoint;
    private IWayPoint wayPoint
    {
        get => _nextWaypoint;
        set
        {
            _nextWaypoint = value;
            target = _nextWaypoint.GetWaypointTransform().position;
        }
    }

    public EnemyObject enemyObject;
    public void Init(IWayPoint wayPoint)
    {
        this.wayPoint = wayPoint.GetNextWayPoint();
    }

    private void Awake()
    {
        Health = enemyObject.Hp;
    }

    void Update()
    {
        if (Level.singleton.state == GlobalState.TUTORIALPAUSE)
            return;

        Move();
        if (Vector3.Distance(transform.position, target) <= 0.1f)
        {
            GetNextWaypoint();
            transform.LookAt(target);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        WaveSpawner.EnemiesKilled++;
        Destroy(gameObject);
        Level.singleton.EnemiesCounter += Level.singleton.Score;
        Level.singleton.EnemiesCounterChange?.Invoke();
        WaveSpawner.EnemiesAlive--;
        BuildManager.singleton.AddMoney(enemyObject.moneyBonus);
    }

    protected void Move()
    {
        Vector3 dir = target - transform.position;

        transform.Translate(dir.normalized * enemyObject.speed * Time.deltaTime, Space.World);
        //transform.rotation
        //transform.position = Vector3.Lerp(transform.position, target, enemyObject.speed * Time.deltaTime);
    }

    protected void GetNextWaypoint()
    {
        if (wayPoint is IInteractableWayPoint)
            ((IInteractableWayPoint)wayPoint).OnEnemyComesIn(this);

        IWayPoint newWayPoint = wayPoint.GetNextWayPoint();
        if(wayPoint == newWayPoint)
        {
            EndPath();
            return;
        }
        wayPoint = newWayPoint;
    }

    void EndPath()
    {
        WaveSpawner.EnemiesAlive--;
        BuildManager.singleton.OnUpdateLives();
        gameObject.SetActive(false);
    }
}
