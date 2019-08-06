using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    [Range(10, 100)]
    public float speed = 70f;
    public GameObject impactEffect;

    public void FindTarget(Transform _target)
    {
        target = _target;
    }


    /*float TotalLifeTime;
    float LifeTime = 0;

    Vector3 A, B;*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);


        /*LifeTime += Time.deltaTime;

        float t = LifeTime / TotalLifeTime;

        if (t > 1)
            Destroy(gameObject);

        Vector3 SA = Vector3.Lerp(transform.position, A, t);
        Vector3 AB = Vector3.Lerp(A, B, t);
        Vector3 BT = Vector3.Lerp(B, target.position + Vector3.up * 0.8f, t);

        Vector3 SAB = Vector3.Lerp(SA, AB, t);
        Vector3 ABT = Vector3.Lerp(AB, BT, t);

        Vector3 SABT = Vector3.Lerp(SAB, ABT, t);

        transform.position = SABT;*/
    }

    private void HitTarget()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(target.gameObject);
        Destroy(gameObject);
    }

    /*public void Init(Transform target)
    {
        this.target = target;
        TotalLifeTime = Vector3.Distance(transform.position, this.target.position) / speed;

        A = Vector3.Lerp(transform.position, this.target.position, 0.3f) + Random.onUnitSphere * Random.Range(2f, 4f) + Vector3.up;
        B = Vector3.Lerp(transform.position, this.target.position, 0.6f) + Random.onUnitSphere * Random.Range(3f, 5f) + Vector3.up * 2;
    }*/
}
