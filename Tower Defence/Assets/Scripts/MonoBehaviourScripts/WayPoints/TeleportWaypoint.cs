using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWaypoint : WaypointBase, IInteractableWayPoint
{
    public GameObject trailEffect;

#if UNITY_EDITOR
    bool bWasShowTrail = false;
#endif

    public override IWayPoint GetNextWayPoint()
    {
        return waypoints[0];
    }

    public void OnEnemyComesIn(Enemy enemy)
    {
#if UNITY_EDITOR
        if(!bWasShowTrail)
        {
            bWasShowTrail = true;
            Debug.Log(transform.position);
            Debug.Log(waypoints[0].transform.position);
        }
#endif

        GameObject TeleportEffect = Instantiate(trailEffect, transform.position, Quaternion.identity);
        TeleportEffect.transform.parent = null;
        TeleportEffect.GetComponent<TrailEffect>().SetTarget(waypoints[0].transform);

        enemy.transform.position = waypoints[0].transform.position;
    }
}
