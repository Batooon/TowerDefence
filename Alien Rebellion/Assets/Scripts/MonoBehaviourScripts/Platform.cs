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
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("WTF DUDE ARE YOU DUMB?? tHeRe IS ALREAADY A TURRET!");
            return;
        }
        rend.material.color = Color.yellow;
        BuildManager.instance.onTurretChoose += InstantiateTurret;
        BuildManager.instance.OpenCloseShop(true);
    }

    public void InstantiateTurret()//Стоит ли перенести это в BuildManager?
    {
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
        BuildManager.instance.onTurretChoose -= InstantiateTurret;
        rend.material.color = startColor;
    }
}
