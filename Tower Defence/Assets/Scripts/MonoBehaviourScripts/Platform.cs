using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector3 positionOffset;
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.singleton;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public void OnMouseDown()
    {
        if (turret != null)
            return;//Сделать меню продажи турели и апгрейда

        if (!buildManager.CanBuild)
        {
            buildManager.OnTurretNull(transform);
            return;
        }
        rend.material.color = Color.yellow;
        buildManager.BuildTurretOn(this);
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
