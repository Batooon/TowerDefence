using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualWayPoint : WaypointBase
{
    public override IWayPoint GetNextWayPoint()
    {
        return waypoints[0];
    }
}
