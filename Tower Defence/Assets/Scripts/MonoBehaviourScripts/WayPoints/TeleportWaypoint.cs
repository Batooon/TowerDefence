using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWaypoint : WaypointBase, IInteractableWayPoint
{
    public GameObject teleportEffect;

    public override IWayPoint GetNextWayPoint()
    {
        return waypoints[0];
    }

    public void OnEnemyComesIn(Enemy enemy)
    {
        GameObject teleportStart = Instantiate(teleportEffect, transform.position, Quaternion.identity);
        teleportStart.transform.parent = null;

        enemy.transform.position = waypoints[0].transform.position;

        GameObject teleportFinish = Instantiate(teleportEffect, enemy.transform.position, Quaternion.identity);
        teleportFinish.transform.parent = null;
    }
}
