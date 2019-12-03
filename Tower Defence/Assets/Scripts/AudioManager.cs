using UnityEngine.Audio;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioListener audioListener;

    public static AudioManager singleton;

    public Sound[] sounds;

    void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        /*for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.GetComponent<AudioSource>();

            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
        }*/
    }

    void Start()
    {
        //Play("Menu");
        //AudioListener.volume = Settings.singletonSettings.volume;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        transform.GetComponent<AudioSource>().clip = s.clip;
        //s.source.Play();
    }
}
