using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Text waveText;

    public Level level;
    // Start is called before the first frame update
    void Start()
    {
        level.waveSpawner.onWaveStateChanged += ChangeText;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (level.waveSpawner.state == State.COUNTDOWN)
        {

        }*/
    }

    public void ChangeText()
    {
        switch (level.waveSpawner.state)
        {
            case State.COUNTDOWN:
                waveText.color = Color.white;
                waveText.text = Mathf.CeilToInt(level.waveSpawner.countdown).ToString();
                break;
            case State.SPAWN:
                waveText.color = Color.red;
                waveText.text = "!WARNING! Wave incoming!";
                break;
            case State.END:
                Destroy(gameObject);
                level.waveSpawner.onWaveStateChanged -= ChangeText;
                break;
        }
    }
}
