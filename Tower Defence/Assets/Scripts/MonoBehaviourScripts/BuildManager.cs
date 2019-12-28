using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public event Action<Transform> TurretMaxLevelAllert;
    public event Action<Transform> TurretAlert;
    public event Action<Transform> NotEnoughMoney;
    public event Action<Transform> TurretAlreadyPlacedHere;
    public event Action MoneyUpdate;
    public event Action LivesUpdate;
    public event Action<TurretObject[]> InitTurretsEvent;

    public void TurretError(Transform t) { TurretAlert.Invoke(t); }
    public void TurretMaxLevelalert(Transform t) { TurretMaxLevelAllert.Invoke(t); }

    public void ActivateNotEnoughMoneyEvent(Transform t) { NotEnoughMoney?.Invoke(t); }

    public TurretObject[] turrets;

    public static BuildManager singleton;

    public int money = 400;

    private GameObject selectedTurret;
    private TurretObject turretData;
    private ShopButtonUI selectedButtonUI;

    public Platform selectedPlatform { get; private set; }

    void Awake()
    {
        if (singleton != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        singleton = this;
    }


    public void TrySelectPlatform(Platform platform)
    {
        if (platform != null && platform.IsEmpty())
        {
            if (!CanBuild())
            {
                TurretError(platform.transform);
                return;
            }
            if (!IsEnoughMoney())
            {
                TurretError(platform.transform);
                return;
            }
        }
        SelectPlatform(platform);
    }

    public bool TryUpgradeTurret(Platform platform)
    {
        if (platform == null || platform.IsEmpty())
            return false;

        if (!IsEnoughMoneyToUpgrade())
        {
            ActivateNotEnoughMoneyEvent(platform.transform);
            return false;
        }

        UpgradeTurretOn(platform);

        return true;

        //platform.turret.GetComponent<Turret>().UpgradeTurret();
    }

    public void SelectTurret(GameObject turretGO, ShopButtonUI button)
    {
        if (turretGO == selectedTurret) 
        {
            selectedButtonUI.Deselect();
            selectedTurret = null;
            button = null;
            return;
        }

        // 1) disselect turret
        if (selectedButtonUI != null)
            selectedButtonUI.Deselect();

        // 2) =
        selectedTurret = turretGO;
        selectedButtonUI = button;

        // select
        Turret turret = selectedTurret.GetComponent<Turret>();
        SetTurretData(turret?.CurrentTurret);
        selectedButtonUI.Select();
        

        void SetTurretData(TurretObject data) => turretData = data;
    }

    public TurretObject GetTurretData() => turretData;

    public void SelectPlatform(Platform platform)
    {
        if (selectedPlatform != null)
        {
            selectedPlatform.Unchoose();
        }

        selectedPlatform = platform;

        if (selectedPlatform != null)
        {
            if (selectedPlatform.IsEmpty())
                BuildTurretOn(platform);

            selectedPlatform.Choose();
        }
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

    public void SellTurretOn(Platform platform)
    {
        money += turretData.sellCost;
        MoneyUpdate?.Invoke();
        Destroy(platform.turret);
        selectedPlatform = null;
    }

    public void UpgradeTurretOn(Platform platform)
    {
        money -= platform.turret.GetComponent<Turret>().CurrentTurret.UpgardeCost;
        MoneyUpdate?.Invoke();
    }

    public void ClearEvents()
    {
        InitTurretsEvent = null;
    }

    internal void OnUpdateLives() => LivesUpdate?.Invoke();


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
    public bool IsEnoughMoneyToUpgrade() => money >= turretData.UpgardeCost;
}
