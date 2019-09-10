using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public event Action<Transform> TurretAlert;
    public event Action MoneyUpdate;
    public event Action LivesUpdate;
    public event Action<Sprite> ActivateShopUI;
    public event Action<Sprite> DeactivateShopUI;
    public event Action<TurretObject[]> InitTurretsEvent;  

    public void TurretError(Transform t) { TurretAlert.Invoke(t); }

    public TurretObject[] turrets;

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
    
    private void Start()
    {
        InitTurretsEvent?.Invoke(turrets);
    }

    public void SelectTurret(GameObject turretGO)
    {
        if (turretGO == selectedTurret)
        {
            //ActivateShopUI?.Invoke(turretData.deselectedTurretUI);
            selectedTurret = null;
            return;
        }

        // 1) disselect turret
        //if (selectedTurret != null)
            //ActivateShopUI?.Invoke(turretData.deselectedTurretUI);

        // 2) =
        selectedTurret = turretGO;

        Turret turret = selectedTurret.GetComponent<Turret>();
        SetTurretData(turret != null ? turret.turret : null);

        // select
       // ActivateShopUI?.Invoke(turretData.selectedTurretUI);

        void SetTurretData(TurretObject data) => turretData = data;
    }

    public TurretObject GetTurretData() => turretData;

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

        if (selectedTurret == null)
        {
            TurretError(platform.transform);
            return;
        }

        if (!IsEnoughMoney())
        {
            TurretError(platform.transform);
            return;
        }

        selectedPlatform = platform;

        BuildTurretOn(platform);
    }

    public GameObject GetSelectedTurret() => selectedTurret;

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

    public bool CanBuild() => selectedTurret != null;
    public bool IsEnoughMoney() => money >= turretData.cost;
}
