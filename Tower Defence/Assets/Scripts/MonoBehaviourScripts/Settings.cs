using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings singletonSettings;

    void Awake()
    {
        singletonSettings = this;
        speed = 1f;
        volume = PlayerPrefs.GetFloat("Volume", 1);
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

    public float volume
    {
        private set;
        get;
    }

    public void SetVolume(float _volume)
    {
        volume = _volume;
        PlayerPrefs.SetFloat("Volume", _volume);
    }
}
