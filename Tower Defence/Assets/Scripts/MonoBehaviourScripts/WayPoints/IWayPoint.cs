using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWayPoint 
{
    IWayPoint GetNextWayPoint();
    Transform GetWaypointTransform();
}
