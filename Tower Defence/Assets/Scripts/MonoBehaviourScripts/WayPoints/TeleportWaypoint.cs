using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWaypoint : WaypointBase, IInteractableWayPoint
{
    public override IWayPoint GetNextWayPoint()
    {
        return waypoints[0];
    }

    public void OnEnemyComesIn(Enemy enemy)
    {
        enemy.transform.position = waypoints[0].transform.position;
    }
}
