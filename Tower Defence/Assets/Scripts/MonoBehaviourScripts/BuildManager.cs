using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public event Action<Transform> TurretAlert;
    public event Action MoneyUpdate;
    public void TurretError(Transform t) { TurretAlert.Invoke(t); }

    public static BuildManager singleton;

    public int money = 400;

    private GameObject turretToBuild;
    private TurretObject turretData;
    private Platform selectedPlatform;

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
    public void ChoosePatform(Platform platform)
    {
        selectedPlatform = platform;
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
        MoneyUpdate.Invoke();
        GameObject turret = Instantiate(turretToBuild, platform.GetBuildPosition(), Quaternion.identity);
        turret.transform.SetParent(platform.transform);
        platform.turret = turret;
    }

    public void ExtractMoney(int amount)
    {
        money -= amount;
        MoneyUpdate.Invoke();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        MoneyUpdate.Invoke();
    }

    public bool CanBuild
    {
        get
        {
            return turretToBuild != null;
        }
    }

    public bool IsEnoughMoney
    {
        get
        {
            return money >= turretData.cost;
        }
    }
}
