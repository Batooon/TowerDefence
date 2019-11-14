using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : Enemy
{
    void Update()
    {
        if (Level.singleton.state == GlobalState.TUTORIALPAUSE)
            return;

        Move();
        if (Vector3.Distance(transform.position, target) <= 0.1f)
        {
            GetNextWaypoint();
            transform.LookAt(target);
        }
    }
}
