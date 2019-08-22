using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Sprite standardTurretSelected;
    BuildManager buildManager;


    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.singleton;
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurret(GameObject.Find("StandardTurret"));
        buildManager.GetSelectedTurretUI().GetComponent<Image>().sprite = standardTurretSelected;
        buildManager.ChooseTurret(buildManager.standardTurret);
    }
}
