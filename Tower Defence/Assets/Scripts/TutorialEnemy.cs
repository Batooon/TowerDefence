using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialEnemy : Enemy
{

    void Update()
    {
        if (TutorialLevel.globalState==TutorialEnum.TUTORIALPAUSE)
            return;

        Move();
        if (Vector3.Distance(transform.position, target) <= 0.1f)
        {
            GetNextWaypoint();
            transform.LookAt(target);
        }
    }
}
