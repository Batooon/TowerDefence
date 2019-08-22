using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public event Action<Transform> onTurretNull;
    public void OnTurretNull(Transform t) { onTurretNull.Invoke(t); }

    public static BuildManager singleton;

    public int money = 400;

    private GameObject turretToBuild;
    private TurretObject turretData;

    void Awake()
    {
        if (singleton != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        singleton = this;
    }

    public void ChooseTurret(GameObject turret)
    {
        turretToBuild = turret;
    }

    public void GetTurretData(TurretObject data)
    {
        turretData = data;
    }

    public void BuildTurretOn(Platform platform)
    {
        if (money<turretData.cost)
        {
            Debug.Log("Not enough money!");
            return;
        }

        money -= turretData.cost;
        GameObject turret = Instantiate(turretToBuild, platform.GetBuildPosition(), Quaternion.identity);
        platform.turret = turret;
        Debug.Log("Turret build! Money left: " + money);
    }

    public bool CanBuild
    {
        get
        {
            return turretToBuild != null;
        }
    }
}
