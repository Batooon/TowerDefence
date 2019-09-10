using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public GameObject TurretButton;

    //private GameObject turret

    private void Start()
    {
        buildManager = BuildManager.singleton;
        buildManager.InitTurretsEvent += SetTurrets;
    }

    public void SetTurrets(TurretObject[] turrets)
    {
        foreach(TurretObject t in turrets)
        {
            GameObject newTurretButton = Instantiate(TurretButton);
            newTurretButton.transform.SetParent(transform);

            ShopButtonUI sbui = newTurretButton.GetComponent<ShopButtonUI>();
            sbui?.Init(t);
        }
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

        gameObject.transform.GetChild(buildManager.GetSelectedTurret().GetComponent<Turret>().index).GetComponent<Image>().sprite = turretUI;
        buildManager.ActivateShopUI -= ActivateTurretUI;
    }

    void DeactivateTurretUI(Sprite turretUI)
    {
        /*Sprite deselectedTurretUI = buildManager.GetSelectedTurret().GetComponent<Turret>().turret.deselectedTurretUI;
        turretUI.GetComponent<Image>().sprite = deselectedTurretUI;*/

        gameObject.transform.GetChild(buildManager.GetSelectedTurret().GetComponent<Turret>().index).GetComponent<Image>().sprite = turretUI;
        buildManager.ActivateShopUI -= DeactivateTurretUI;
    }

    public void TurretPressed(GameObject newTurret)
    {
        if (buildManager.GetSelectedTurret() != newTurret)
        {
            buildManager.ActivateShopUI += DeactivateTurretUI;
            buildManager.SelectTurret(newTurret);
            buildManager.ActivateShopUI += ActivateTurretUI;
        }
        else
        {
            buildManager.ActivateShopUI += DeactivateTurretUI;
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
