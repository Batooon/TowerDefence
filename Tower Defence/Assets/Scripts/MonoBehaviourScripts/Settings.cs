using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings singletonSettings;

    void Awake()
    {
        speed = 1;
        singletonSettings = this;
    }

    public float speed
    {
        private set;
        get;
    }
    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
