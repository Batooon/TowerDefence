using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public event Action<Transform> TurretAlert;
    public event Action MoneyUpdate;
    public event Action LivesUpdate;
    public void TurretError(Transform t) { TurretAlert.Invoke(t); }

    public static BuildManager singleton;

    public int money = 400;

    private GameObject selectedTurret;
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

    public void SelectTurret(GameObject turret)
    {
        if (turret == selectedTurret)
            return;

        selectedTurret = null;
        // 1) disselect turret

        // 2) =
        selectedTurret = turret;

        // select
    }
    public void SelectPlatform(Platform platform)
    {
        if (selectedPlatform == platform)
        {
            if (platform.IsTurretActive)
                platform.turret.GetComponent<Turret>().Activate();
            else
                platform.turret.GetComponent<Turret>().Deactivate();
            return;
        }

        if (platform.turret != null)
        {

            if (selectedPlatform != null)
            {
                selectedPlatform.turret.GetComponent<Turret>().Deactivate();

                selectedPlatform = null;
            }

            selectedPlatform = platform;

            selectedPlatform.turret.GetComponent<Turret>().Activate();

            return;
        }

        if (selectedPlatform != null)
        {
            selectedPlatform.turret.GetComponent<Turret>().Deactivate();

            selectedPlatform = null;
        }

        selectedPlatform = platform;

        BuildTurretOn(platform);
    }

    public void GetTurretData(TurretObject data)
    {
        turretData = data; 
    }

    public void BuildTurretOn(Platform platform)
    {
        money -= turretData.cost;
        MoneyUpdate.Invoke();
        GameObject turret = Instantiate(selectedTurret, platform.GetBuildPosition(), Quaternion.identity);
        turret.transform.SetParent(platform.transform);
        platform.turret = turret;
    }

    internal void UpdateLives()
    {
        LivesUpdate?.Invoke();
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

    public bool CanBuild()
    {
        return selectedTurret != null;
    }
    //TODO: объединить в один метод
    public bool IsEnoughMoney()
    {
        return money >= turretData.cost;
    }
}
