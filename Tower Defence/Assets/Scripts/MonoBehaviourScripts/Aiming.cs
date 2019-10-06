using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [SerializeField]
    float speed;
    [HideInInspector]
    public float DistanceThisFrame;
    [HideInInspector]
    public Vector3 direction;

    public void FlyTo(Transform target)
    {
        DistanceThisFrame = speed * Time.deltaTime;
        direction = target.transform.position - transform.position;

        transform.LookAt(target.transform);
        transform.Translate(direction.normalized * DistanceThisFrame, Space.World);
    }
}
