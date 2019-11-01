using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovementCalculator 
{
    private int points;
    private float speed;
    private float[] t;
    private Vector3[] pos;

    private System.Func<float, Vector3> passFunction;

    private int dotNumber;
    float lifeTime;

    /// <summary>
    ///  
    /// </summary>
    /// <param name="passFunction"> Need to choose function with: f(0)=start, f(1)=finish </param>
    /// <param name="points"></param>
    public CurveMovementCalculator(System.Func<float, Vector3> passFunction, float speed, int points = 20)
    {
        this.passFunction = passFunction;
        this.points = points;
        this.speed = speed;

        Reset();
    }

    public void Reset()
    {
        t = new float[points + 1];
        pos = new Vector3[points + 1];

        t[0] = 0;
        pos[0] = passFunction(0);

        float points_float = (float)points;
        for(int i=1; i<=points; i+=1)
        {
            pos[i] = passFunction(i / points_float);
            float length = Vector3.Distance(pos[i - 1], pos[i]);
            t[i] = t[i - 1] + length / speed;
            Debug.Log(pos[i]+" "+ t[i]);
        }

        dotNumber = 0;
        lifeTime = 0;
    }

    public void Tick(float deltaTime)
    {
        lifeTime += deltaTime;

        while (dotNumber < t.Length && lifeTime > t[dotNumber])
            dotNumber++;

        //if (dotNumber == t.Length-1)
            dotNumber--;
    }

    public System.ValueTuple<Vector3, Quaternion> GetObjectTransform()
    {
        float a = (lifeTime - t[dotNumber]) / (t[dotNumber + 1] - t[dotNumber]);

        Vector3 resV = Vector3.Lerp(pos[dotNumber], pos[dotNumber + 1], a);
        Quaternion resQ = Quaternion.FromToRotation(pos[dotNumber], pos[dotNumber + 1]);

        Debug.Log(dotNumber + " " + lifeTime + " " + resV);

        return (resV, resQ);
    }
}
