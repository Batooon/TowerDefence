using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Turret : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;

    [HideInInspector]
    public LineRenderer line;

    public TurretObject turret;
    private GameObject target;

    [Header("Unity Setup Fields")]

    [HideInInspector]
    private string enemyTag = "Enemy";
    [HideInInspector]
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint;

    void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreateRadius();
    }

    public void CreateRadius()
    {
        float x, z;

        float angle = 20f;

        for (int i = 0; i < (segments+1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * turret.range;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * turret.range;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }

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
            target = nearestEnemy;
        else
            target = null;
    }

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
        GameObject bulletGO = Instantiate(bulletPrefab, GetFirePointTransform());
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.FindTarget(target);
    }

    public void Activate()
    {
        line.enabled = true;
    }

    public void Deactivate()
    {
        line.enabled = false;
    }

    public virtual Transform GetFirePointTransform()
    {
        return firePoint;
    }

    private void RotateToEnemy()
    {
        Vector3 dir = target.transform.position - transform.position;
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
