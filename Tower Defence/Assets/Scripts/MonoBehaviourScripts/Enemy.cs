using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public struct EnemyDieEvent
{
    public int moneyBonus;
}

public class Enemy : MonoBehaviour
{
    float Health;

    [SerializeField]
    float rotationSpeed;

    protected Vector3 target;
    private IWayPoint _nextWaypoint;

    Quaternion startRotation;
    Quaternion finalRotation;
    Vector3 axis;

    public EnemyObject enemyObject;

    float lifeTime;

    private IWayPoint wayPoint
    {
        get => _nextWaypoint;
        set
        {
            _nextWaypoint = value;
            target = _nextWaypoint.GetWaypointTransform().position;
        }
    }

    public void Init(IWayPoint wayPoint)
    {
        this.wayPoint = wayPoint.GetNextWayPoint();
    }

    private void Awake()
    {
        Health = enemyObject.Hp;
        startRotation = UnityEngine.Random.rotationUniform;
        finalRotation = UnityEngine.Random.rotationUniform;
        axis = UnityEngine.Random.onUnitSphere;

        lifeTime = 0f;
    }

    void Update()
    {
        if (Level.singleton.state == GlobalState.TUTORIALPAUSE)
            return;

        lifeTime += Time.deltaTime;

        transform.Rotate(axis, rotationSpeed * Time.deltaTime);

        Move();
        if (Vector3.Distance(transform.position, target) <= 0.2f)
        {
            GetNextWaypoint();
            //transform.LookAt(target);//Сейчас враги симметричные, поэтому нет смысла это делать
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
        EnemyDieEvent enemyDieEvent;
        enemyDieEvent.moneyBonus = enemyObject.moneyBonus;
        Level.singleton.OnEnemyDied(enemyDieEvent);
        Destroy(gameObject);
    }

    protected void Move()
    {
        Vector3 dir = target - transform.position;

        transform.Translate(dir.normalized * enemyObject.speed * Time.deltaTime, Space.World);
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
        Level.singleton.waveSpawner.EnemiesAlive--;
        BuildManager.singleton.OnUpdateLives();
        gameObject.SetActive(false);
    }
}
