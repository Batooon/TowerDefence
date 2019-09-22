using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public GameObject TurretButton;

    private List<ShopButtonUI> turretButtons = new List<ShopButtonUI>();

    private void Start()
    {
        foreach(Transform child in transform)
        {
            turretButtons.Add(child.GetComponent<ShopButtonUI>());
        }
        buildManager = BuildManager.singleton;
        //buildManager.InitTurrets();
        SetTurrets(buildManager.turrets);
    }

    public void SetTurrets(TurretObject[] turrets)
    {
        //int i = 0;
        foreach(TurretObject t in turrets)
        {
            /*turretButtons[i].Init(t);
            i++;*/
            GameObject newTurretButton = Instantiate(TurretButton);
            newTurretButton.transform.SetParent(transform);

            ShopButtonUI sbui = newTurretButton.GetComponent<ShopButtonUI>();
            sbui?.Init(t);
        }
    }

    public void TurretPressed(GameObject newTurret, ShopButtonUI button) => buildManager.SelectTurret(newTurret, button);
}
