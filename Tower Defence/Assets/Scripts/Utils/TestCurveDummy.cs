using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCurveDummy : MonoBehaviour
{
    public GameObject a,b,c,d;
    public GameObject Obj;

    private CurveMovementCalculator cmc;

    public float speed = 0.8f;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateNewPath();
        }
#endif

        if (cmc != null)
        {
            cmc.Tick(Time.deltaTime);
            (transform.position, transform.rotation) = cmc.GetObjectTransform();
        }
    }

    void CreateNewPath()
    {
        /*if (a != null) Destroy(a);
        if (b != null) Destroy(b);
        if (c != null) Destroy(c);
        if (d != null) Destroy(d);

        a = Instantiate(Obj);
        b = Instantiate(Obj);
        c = Instantiate(Obj);
        d = Instantiate(Obj);

        a.transform.position = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
        b.transform.position = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
        c.transform.position = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
        d.transform.position = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));*/

        if (cmc == null)
            cmc = new CurveMovementCalculator(GetLerpPos, speed);
        else
            cmc.Reset();
    }

    Vector3 GetLerpPos(float t)
    {
        return t * t * t * a.transform.position + 3*t * t * (1-t) * b.transform.position +
            3*t * (1 - t)* (1 - t) * c.transform.position + (1 - t)* (1 - t)* (1 - t) * d.transform.position;
    }
}
