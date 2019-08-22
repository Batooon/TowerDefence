using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    [Header("Standard Turret Attributes")]
    public Sprite standardTurretSelected;
    public Sprite standardTurretDeselected;
    public Image standardTurretImage;
    public GameObject standardTurretUI;

    //[Space(20f)]

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.singleton;
    }

    void SelectStandardTurret()
    {
        buildManager.ChooseTurret(buildManager.standardTurret);
        standardTurretImage.sprite = standardTurretSelected;
    }

    void DeselectStandardTurret()
    {
        buildManager.ChooseTurret(null);
        standardTurretImage.sprite = standardTurretDeselected;
    }

    public void SelectStandardTurretManager()
    {
        if (standardTurretImage.sprite == standardTurretSelected)
            DeselectStandardTurret();
        else
            SelectStandardTurret();
    }
}
