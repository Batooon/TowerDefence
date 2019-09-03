using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    [Header("Standard Turret")]
    public TurretObject standardTurret;
    public GameObject standardTurretUI;

    void Start()
    {
        buildManager = BuildManager.singleton;
    }

    void SelectTurret(GameObject turret)
    {
        buildManager.SelectTurret(turret);
        standardTurretUI.GetComponent<Image>().sprite = standardTurret.selectedTurretUI;
    }

    void DeselectTurret(GameObject turret)
    {
        buildManager.SelectTurret(null);
        standardTurretUI.GetComponent<Image>().sprite = standardTurret.deselectedTurretUI;
    }

    public void SelectTurretManager(GameObject turret)
    {
        if (buildManager.CanBuild())
            DeselectTurret(turret);
        else
            SelectTurret(turret);
    }
}
