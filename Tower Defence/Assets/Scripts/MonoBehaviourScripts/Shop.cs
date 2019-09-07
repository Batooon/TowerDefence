using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    //private GameObject turret;

    void Start()
    {
        buildManager = BuildManager.singleton;
    }

    /*void SelectTurret(GameObject turretGO)
    {
        //turret = null;
        //turret = turretGO;
        buildManager.SelectTurret(turretGO);
    }

    void DeselectTurret(GameObject turretGO)
    {
        buildManager.SelectTurret(null);
    }*/

    void ActivateTurretUI(Sprite turretUI)
    {
        /*Sprite selectedTurretUI = buildManager.GetSelectedTurret().GetComponent<Turret>().turret.selectedTurretUI;
        turretUI.GetComponent<Image>().sprite = selectedTurretUI;*/

        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = turretUI;
        buildManager.ShopUIUpdate -= ActivateTurretUI;
    }

    void DeactivateTurretUI(Sprite turretUI)
    {
        /*Sprite deselectedTurretUI = buildManager.GetSelectedTurret().GetComponent<Turret>().turret.deselectedTurretUI;
        turretUI.GetComponent<Image>().sprite = deselectedTurretUI;*/

        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = turretUI;
        buildManager.ShopUIUpdate -= DeactivateTurretUI;
    }

    public void TurretPressed(GameObject newTurret)
    {
        if (buildManager.GetSelectedTurret() != newTurret)
        {
            buildManager.ShopUIUpdate += ActivateTurretUI;
            buildManager.SelectTurret(newTurret);
        }
        else
        {
            buildManager.ShopUIUpdate += DeactivateTurretUI;
            buildManager.SelectTurret(null);
        }
    }


    //public void SelectTurretManager(GameObject newTurret)
    //{
        /*
        //if (buildManager.CanBuild())
            DeselectTurret(newTurret);
        //else
        if (turret != newTurret)
            SelectTurret(newTurret);*/
    //}

    /*public void TurretUIManager(GameObject turretUI)
    {
        if (buildManager.CanBuild())
            DeselectTurret(turretUI);
        else
            ActivateTurretUI(turretUI);
    }*/
}
