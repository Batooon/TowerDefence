using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public delegate void ClickAction();
    public event ClickAction onTurretChoose;

    public static BuildManager instance;

    public GameObject shop;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        if (shop == null)
        {
            Debug.LogError("Turret shop is equal to null!!!");
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject[] turrets;

    private GameObject turretToBuild;

    public void ChooseTurret(int index)
    {
        turretToBuild = turrets[index];
        onTurretChoose?.Invoke();
        OpenCloseShop(false);
    }

    void Start()
    {
        if (turrets == null)
        {
            turrets = new GameObject[1];
            turrets[0] = standardTurretPrefab;
            Debug.LogError("No turrets!");
        }
    }


    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void OpenCloseShop(bool isOpen)
    {
        shop.SetActive(isOpen);
    }
}
