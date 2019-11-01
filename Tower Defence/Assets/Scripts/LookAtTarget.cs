using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Leave empty to look at main camera")]
    private GameObject target = null;

    private Vector3 targetPosition;

    private void Start()
    {
        if (target == null)
            target = Camera.main.gameObject;
    }

    private void LateUpdate()
    {
        if (targetPosition != target.transform.position)
        {
            targetPosition = target.transform.position;
            transform.LookAt(targetPosition);
        }
    }
}
