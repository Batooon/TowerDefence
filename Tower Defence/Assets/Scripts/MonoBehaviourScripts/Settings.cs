using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings singletonSettings;

    void Awake()
    {
        /*if (singletonSettings = null)
            singletonSettings = this;
        else
        {
            Destroy(gameObject);
            return;
        }*/

        //DontDestroyOnLoad(gameObject);

        singletonSettings = this;
        speed = 1f;
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
