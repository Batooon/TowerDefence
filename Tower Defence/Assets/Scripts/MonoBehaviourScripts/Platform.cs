using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.singleton;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (buildManager.GetTurretToBuild() == null)
            return;

        if (turret != null)
        {
            Debug.Log("WTF DUDE ARE YOU DUMB?? tHeRe IS ALREAADY A TURRET!");
            return;
        }
        rend.material.color = Color.yellow;
        buildManager.onTurretBuild += InstantiateTurret;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public void InstantiateTurret()
    {
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
        buildManager.onTurretBuild -= InstantiateTurret;
    }
}
