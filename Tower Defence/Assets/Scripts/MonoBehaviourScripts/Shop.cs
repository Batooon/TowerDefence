using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
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
        buildManager.ChooseTurret(buildManager.standardTurret);
    }
}
