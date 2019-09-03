using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTurret : Turret
{
    public Transform secondPoint;
    int Shoots = 0;

    public override Transform GetFirePointTransform()
    {
        Shoots++;
        return Shoots % 2 == 0 ? secondPoint : firePoint;
    }
}
