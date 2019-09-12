using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public GameObject TurretButton;

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

    public void TurretPressed(GameObject newTurret, ShopButtonUI button) => buildManager.SelectTurret(newTurret, button);
}
