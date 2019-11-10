using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWaveSpawner : WaveSpawner
{
    void Update()
    {
        /*if (TutorialLevel.IsPause)
            return;*/

        if (TutorialLevel.globalState == TutorialEnum.TUTORIALPAUSE)
            return;

        //onWaveStateChanged?.Invoke();
        ActivateWaveStateChangedEvent();

        if (EnemiesAlive > 0)
            return;

        if (waveIndex == amountOfWaves)
        {
            Level.singleton.WinGame();
            return;
        }

        if (!isWaveIncoming)
        {
            state = State.COUNTDOWN;
            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            if (countdown <= 0)
            {
                state = State.SPAWN;
                isWaveIncoming = true;
                SpawnWave();
            }
        }
        else
            countdown = 0f;
    }
}
