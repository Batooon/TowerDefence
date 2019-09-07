using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector3 positionOffset;
    public Vector3 GetBuildPosition() => transform.position + positionOffset;

    [Header("Optional")]
    public GameObject turret;

    public bool IsTurretActive = false;

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
        buildManager.SelectPlatform(this);
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
