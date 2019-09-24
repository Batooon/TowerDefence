using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    BuildManager buildManager;

    private GameObject target;
    private Enemy enemy;

    [Range(1, 30)]
    public float speed = 20f;
    public GameObject impactEffect;

    private float TotalLifetime;
    //private float Lifetime;
    private Vector3 A, B;
    private float explosionRadius;

    void Awake()
    {
        //Lifetime = 0;
        buildManager = BuildManager.singleton;
    }

    public void FindTarget(GameObject _target, float explR)
    {
        explosionRadius = explR;
        target = _target;

        enemy = target.GetComponent<Enemy>();

        /*explosionRadius = explR;
        target = _target;
        if (type == TrajectoryType.MISSILE)
        {
            SetTrajectoryType(TrajectoryType.MISSILE);
            CalculateTotalLifetime();
        }
        else
            SetTrajectoryType(TrajectoryType.DEFAULT);
        enemy = target.GetComponent<Enemy>();*/
    }

    //private void CalculateTotalLifetime() => TotalLifetime = Vector3.Distance(transform.position, target.transform.position) / speed;

    /*private void UpdateMissileData()
    {
        //TotalLifetime = Vector3.Distance(transform.position, target.transform.position) / speed;
        A = Vector3.Lerp(transform.position, target.transform.position, 0.3f);
        B = Vector3.Lerp(transform.position, target.transform.position, 0.6f);
    }*/

    void Fly(Vector3 direction)
    {
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        //transform.LookAt(target.transform);

        /*
        UpdateMissileData();

        Lifetime += Time.deltaTime;
        float t = Lifetime / TotalLifetime;

        if (t >= 1)
        {
            HitTarget();
            return;
        }

        Vector3 SA = Vector3.Lerp(transform.position, A, t);
        Vector3 AB = Vector3.Lerp(A, B, t);
        Vector3 BT = Vector3.Lerp(B, target.transform.position + Vector3.up, t);

        Vector3 SAB = Vector3.Lerp(SA, AB, t);
        Vector3 ABT = Vector3.Lerp(AB, BT, t);

        Vector3 SABT = Vector3.Lerp(SAB, ABT, t);
        transform.position = SABT;*/
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.transform.position - transform.position;
        Fly(dir);
    }

    private void HitTarget()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);

        if (explosionRadius > 0)
            Explode();
        else
            Damage(target.transform);

        Destroy(gameObject);

        buildManager.AddMoney(enemy.enemyObject.moneyBonus);

        //Сделать проверку на смерть
        if (enemy.enemyObject.Hp <= 0)//Перенести в Enemy
        {

        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    void Explode()
    {
        Collider[] hitObjects= Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider collider in hitObjects)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }
}
