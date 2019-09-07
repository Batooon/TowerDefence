using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Turret", menuName ="Turret")]
public class TurretObject : ScriptableObject
{
    [Range(2f, 4f)]
    public float range;

    [Range(1f, 5f)]
    public float fireRate;

    public float speedRotation;

    public float fireCountdown;

    public float damage;

    public int cost;

    public Sprite selectedTurretUI;

    public Sprite deselectedTurretUI;

    public TrajectoryType bulletTrajectory;

    [Header("Only if Area Damage")]
    public float area;
}
