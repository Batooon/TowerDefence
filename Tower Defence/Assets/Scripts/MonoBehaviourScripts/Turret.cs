using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class Turret : MonoBehaviour
{
    int level;
    public TurretObject[] TurretLevels;

    //Radius
    private int segments = 50;
    [HideInInspector]
    public LineRenderer line;

    public GameObject TurretUI;

    public TurretObject CurrentTurret;

    private float fireCountdown;

    private GameObject target;

    public int index;

    [Header("Unity Setup Fields")]

    [HideInInspector]
    private string enemyTag = "Enemy";
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public TextMeshProUGUI SellText;
    public TextMeshProUGUI UpgradeText;

    private Vector3 enemyPosition;

    void Awake()
    {
        level = 0;
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreateRadius();
        fireCountdown = CurrentTurret.fireCountdown;
        InitText();
    }

    void InitText()
    {
        SellText.text = "Sell ₴" + CurrentTurret.sellCost.ToString();
        UpgradeText.text = "Upgrade ₴" + CurrentTurret.UpgardeCost.ToString();
    }

    public void CreateRadius()
    {
        float x, z;

        float angle = 20f;

        for (int i = 0; i < (segments+1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * CurrentTurret.range;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * CurrentTurret.range;

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

        if (nearestEnemy != null && shortestDistance <= CurrentTurret.range)
        {
            target = nearestEnemy;
            enemyPosition = nearestEnemy.transform.position;
        }
        else
            target = null;
    }

    void Update()
    {
        if (target == null)
            return;

        RotateToEnemy();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / CurrentTurret.fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, GetFirePointTransform());
        bulletGO.transform.parent = null;
        bulletGO.transform.localScale = bulletPrefab.transform.localScale;
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.FindTarget(target, bullet.areaDamage);
        }
    }

    public void Activate()
    {
        line.enabled = true;
        TurretUI.SetActive(true);
    }

    public void Deactivate()
    {
        line.enabled = false;
        TurretUI.SetActive(false);
    }

    public virtual Transform GetFirePointTransform() => firePoint;

    private void RotateToEnemy()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * CurrentTurret.speedRotation).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void SellTurret()
    {
        BuildManager.singleton.SellTurretOn(transform.GetComponentInParent<Platform>());
    }

    public void UpgradeTurret()
    {
        if (level == TurretLevels.Length)
        {
            BuildManager.singleton.TurretMaxLevelalert(gameObject.GetComponentInParent<Transform>());
            return;
        }
        CurrentTurret = TurretLevels[level++];
        BuildManager.singleton.UpgradeTurretOn(transform.GetComponentInParent<Platform>());
        updateData();
    }

    private void updateData()
    {
        CreateRadius();
        InitText();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CurrentTurret.range);
    }
}
