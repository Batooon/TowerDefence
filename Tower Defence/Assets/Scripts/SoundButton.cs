using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundButton : MonoBehaviour
{
    bool sound = true;

    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    Sprite soundOn;
    [SerializeField]
    Sprite soundOff;
    [SerializeField]
    Button soundbutton;


    private void Start()
    {
        //Game.ChangeGameSpeed(1f);
        if (Settings.singletonSettings.volume > 0)
        {
            soundbutton.image.sprite = soundOn;
        }
        else
        {
            soundbutton.image.sprite = soundOff;
        }
    }

    public void OnOffSound()
    {
        if (sound)
        {
            sound = false;
            mixer.SetFloat("volume", -80f);
            soundbutton.image.sprite = soundOff;
            //Settings.singletonSettings.SetVolume(0f);
        }
        else
        {
            sound = true;
            soundbutton.image.sprite = soundOn;
            mixer.SetFloat("volume", 0f);
            //Settings.singletonSettings.SetVolume(0f);
        }
    }
}
