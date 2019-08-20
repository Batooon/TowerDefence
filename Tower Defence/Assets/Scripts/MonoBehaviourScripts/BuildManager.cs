using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public delegate void ClickAction();
    public event ClickAction onTurretBuild;
    public event ClickAction onTurretNull;

    public static BuildManager singleton;

    public float money;

    public GameObject standardTurret;
    private GameObject turretToBuild;

    void Awake()
    {
        if (singleton != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        singleton = this;
    }

    void Update()
    {
        onTurretNull?.Invoke();
        onTurretBuild?.Invoke();
    }


    public void ChooseTurret(GameObject turret)
    {
        turretToBuild = turret;
    }


    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void BuildTurret()
    {

    }
}
