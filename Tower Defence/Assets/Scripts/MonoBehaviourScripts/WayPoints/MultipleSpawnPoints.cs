using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSpawnPoints : WaypointBase
{
    int index;
    public override IWayPoint GetNextWayPoint()
    {
        return waypoints[0];
    }

    public override Transform GetWaypointTransform()
    {
        index = Random.Range(0, waypoints.Length);
        return waypoints[index].transform;
    }
}
