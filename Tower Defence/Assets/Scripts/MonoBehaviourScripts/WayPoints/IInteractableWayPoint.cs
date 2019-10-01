using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableWayPoint : IWayPoint
{
    void OnEnemyComesIn(Enemy enemy);
}
