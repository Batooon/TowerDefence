using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalState
{
    GAME,
    PAUSE,
    END
}
public class Level : MonoBehaviour
{
    public BuildManager buildManager;
    public WaveSpawner waveSpawner;

    public GlobalState state;

    public int Hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GlobalState.GAME:

                break;
            case GlobalState.PAUSE:
                Time.timeScale = 0;
                break;
            case GlobalState.END:
                break;
        }
    }
}
