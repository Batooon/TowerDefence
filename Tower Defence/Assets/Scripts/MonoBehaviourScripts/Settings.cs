using System;
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
        IsTutorialPassed = Convert.ToBoolean(PlayerPrefs.GetInt("IsTutorialPassed", 0));
    }

    public bool IsTutorialPassed
    {
        private set;
        get;
    }

    public void TutorialPassed()
    {
        IsTutorialPassed = true;
        PlayerPrefs.SetInt("IsTutorialPassed", Convert.ToInt32(IsTutorialPassed));
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
