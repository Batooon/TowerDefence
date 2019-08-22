using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    [Header("Turrets")]
    [Space(20f)]
    [Header("Standard Turret")]
    public TurretObject standardTurret;
    public GameObject standardTurretUI;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.singleton;
    }

    void SelectTurret(GameObject turret)
    {
        buildManager.ChooseTurret(turret);
        standardTurretUI.GetComponent<Image>().sprite = standardTurret.selectedTurretUI;
    }

    void DeselectTurret(GameObject turret)
    {
        buildManager.ChooseTurret(null);
        standardTurretUI.GetComponent<Image>().sprite = standardTurret.deselectedTurretUI;
    }

    public void SelectTurretManager(GameObject turret)
    {
        if (buildManager.CanBuild/*buildManager.GetTurretToBuild() == turret*/)
            DeselectTurret(turret);
        else
            SelectTurret(turret);
    }
}
