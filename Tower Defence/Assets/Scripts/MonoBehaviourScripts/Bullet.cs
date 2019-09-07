using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TrajectoryType
{
    DEFAULT,
    MISSILE
}
public class Bullet : MonoBehaviour
{
    BuildManager buildManager;

    private TrajectoryType trajectoryType;

    private GameObject target;
    private Enemy enemy;

    [Range(1, 30)]
    public float speed = 20f;
    public GameObject impactEffect;

    private float TotalLifetime;
    private Vector3 A, B;

    void Awake()
    {
        buildManager = BuildManager.singleton;
    }

    public void FindTarget(GameObject _target,TrajectoryType type)
    {
        target = _target;
        if (type == TrajectoryType.MISSILE)
            InitMissileData();
        else
            SetTrajectoryType(TrajectoryType.DEFAULT);
        enemy = target.GetComponent<Enemy>();
    }

    private void InitMissileData()
    {
        SetTrajectoryType(TrajectoryType.MISSILE);
        TotalLifetime = Vector3.Distance(transform.position, target.transform.position) / speed;
        A = Vector3.Lerp(transform.position, target.transform.position, 0.3f) + UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(2f, 4f) + Vector3.up;
        B = Vector3.Lerp(transform.position, target.transform.position, 0.6f) + UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(3f, 5f) + Vector3.up * 2;
    }

    public void SetTrajectoryType(TrajectoryType type) => trajectoryType = type;

    void Fly(TrajectoryType type,Vector3 direction)
    {
        switch (type)
        {
            case TrajectoryType.DEFAULT:

                float distanceThisFrame = speed * Time.deltaTime;

                if (direction.magnitude <= distanceThisFrame)
                {
                    HitTarget();
                    return;
                }

                transform.Translate(direction.normalized * distanceThisFrame, Space.World);
                break;

            case TrajectoryType.MISSILE:
                float LifeTime = 0;

                LifeTime += Time.deltaTime;
                float t = LifeTime / TotalLifetime;

                if (t >= 1)
                {
                    HitTarget();
                    return;
                }

                Vector3 SA = Vector3.Lerp(transform.position, A, t);
                Vector3 AB = Vector3.Lerp(A, B, t);
                Vector3 BT = Vector3.Lerp(B, target.transform.position, t);

                Vector3 SAB = Vector3.Lerp(SA, AB, t);
                Vector3 ABT = Vector3.Lerp(AB, BT, t);

                Vector3 SABT = Vector3.Lerp(SAB, ABT, t);
                transform.position = SABT;
                break;
        }
    }

    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.transform.position - transform.position;
        Fly(trajectoryType, dir);
    }

    private void HitTarget()
    {
        //Сделать проверку на смерть
        if (enemy.enemyObject.Hp <= 0)//Перенести в Enemy
        {

        }

        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);

        Destroy(target);

        Destroy(gameObject);

        buildManager.AddMoney(enemy.enemyObject.moneyBonus);
    }
}
