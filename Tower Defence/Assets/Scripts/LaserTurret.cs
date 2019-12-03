using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Turret
{
    public bool UseLaser = false;
    public LineRenderer lineRenderer;

    public ParticleSystem impactEffect;

    public float damageOverTime = 5;

    private void Update()
    {
        if (target == null)
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
            }

            return;
        }

        RotateToEnemy();

        Laser();
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.transform.position);

        Vector3 direction = firePoint.position - target.transform.position;

        impactEffect.transform.position = target.transform.position + direction.normalized * .15f;

        impactEffect.transform.rotation = Quaternion.LookRotation(direction);

    }
}
