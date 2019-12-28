using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Shop : MonoBehaviour
{
    [Inject]
    BuildManager buildManager;

    public GameObject TurretButton;

    private List<ShopButtonUI> turretButtons = new List<ShopButtonUI>();

    private void Start()
    {
        foreach(Transform child in transform)
        {
            turretButtons.Add(child.GetComponent<ShopButtonUI>());
        }
        SetTurrets(buildManager.turrets);
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
