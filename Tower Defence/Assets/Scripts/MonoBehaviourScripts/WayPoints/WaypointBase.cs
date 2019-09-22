using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaypointBase : MonoBehaviour, IWayPoint
{
    public WaypointBase[] waypoints;

    void Awake()
    {
        if(waypoints.Length==0)
        {
            Debug.LogError($"Waypoint.cs : {name} don't have any waypoits");
        }
    }

    public abstract IWayPoint GetNextWayPoint();
    public Vector3 GetWayPointLocation() => transform.position;
}
