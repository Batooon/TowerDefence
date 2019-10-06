using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aiming))]
public class TrailEffect : MonoBehaviour
{
    [HideInInspector]
    public Transform Target;

    Aiming aim;

    void Start()
    {
        aim = transform.GetComponent<Aiming>();
    }
    void Update()
    {
        if (Target == null)
            return;

        aim.FlyTo(Target);

        if (aim.direction.magnitude <= aim.DistanceThisFrame)
        {
            Destroy(gameObject);
            return;
        }

    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}
