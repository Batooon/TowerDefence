using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MultipleWaypointForkType
{
    Semafor,
    Random,
    Fibonachi
}

public class MultipleWaypoint : WaypointBase
{
    int preNumber = 0;
    int number = 0;
    int i = 0;
    int index = 0;
    [SerializeField]
    private MultipleWaypointForkType forkType;
    public override IWayPoint GetNextWayPoint()
    {
        switch (forkType)
        {
            case MultipleWaypointForkType.Semafor:
                index++;
                if (index % waypoints.Length == 0)
                {
                    index = 0;
                }

                break;

            case MultipleWaypointForkType.Random:
                index = Random.Range(0, waypoints.Length);
                break;
            case MultipleWaypointForkType.Fibonachi:
                if (i == 0)
                {
                    number += 1;
                    number += preNumber;
                    i = number;
                    index = 1;
                }
                index = 0;
                i -= 1;
                break;

            default:
                throw new System.NotSupportedException();
        }

        return waypoints[index];
    }
}