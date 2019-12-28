using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TutorialEnemy : Enemy
{
    [Inject]
    Level level;

    void Update()
    {
        if (level.state == GlobalState.TUTORIALPAUSE)
            return;

        Move();
        if (Vector3.Distance(transform.position, target) <= 0.1f)
        {
            GetNextWaypoint();
            transform.LookAt(target);
        }
    }
}
