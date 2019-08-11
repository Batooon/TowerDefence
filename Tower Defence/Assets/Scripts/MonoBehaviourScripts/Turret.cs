using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public TurretObject turret;
    private Transform target;

    /*[Header("Attributes")]

    [Range(2f,4f)]
    public float range = 3f;
    [Range(1f,5f)]
    public float fireRate = 1f;
    public float fireCountdown = 0f;*/

    [Header("Unity Setup Fields")]

    /*[HideInInspector]
    public float speedRotation = 10f;*/
    [HideInInspector]
    private string enemyTag = "Enemy";
    [HideInInspector]
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SearchForEnemy", 0f, 0.5f);
    }

    void SearchForEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= turret.range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        RotateToEnemy();

        if (turret.fireCountdown <= 0f)
        {
            Shoot();
            turret.fireCountdown = 1f / turret.fireRate;
        }
        turret.fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.FindTarget(target);
    }

    private void RotateToEnemy()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turret.speedRotation).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turret.range);
    }
}
