using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
    public Vector3 positionOffset;
    public Vector3 GetBuildPosition() => transform.position + positionOffset;

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
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.selectedPlatform != this)
            buildManager.TrySelectPlatform(this);
        else
            buildManager.TrySelectPlatform(null);
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public void Choose()
    {
        if (turret != null)
            turret.GetComponent<Turret>().Activate();
    }

    public void Unchoose()
    {
        if (turret != null)
            turret.GetComponent<Turret>().Deactivate();
    }

    public bool IsEmpty()
    {
        return turret == null;
    }

    public IEnumerator Highlight()
    {
        rend.material.color = Color.Lerp(Color.white, Color.red, Mathf.Sin(Time.deltaTime));
        yield return null;
    }

    public void StopHighlight()
    {
        rend.material.color = startColor;
    }
}
