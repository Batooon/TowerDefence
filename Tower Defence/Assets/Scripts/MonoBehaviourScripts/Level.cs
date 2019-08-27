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

    private GlobalState _state;
    public GlobalState state
    {
        get => _state;
        private set
        {
            _state = value;
            OnStateChanged();
        }
    }

    public int Hp = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnStateChanged()
    {
        switch (state)
        {
            case GlobalState.GAME:
                Time.timeScale = Settings.singletonSettings.speed;
                break;
            case GlobalState.PAUSE:
                Time.timeScale = 0;
                break;
            case GlobalState.END:
                Time.timeScale = 0;
                break;
        }
    }

    public void ChangeGameSpeed(float speed)
    {
        Settings.singletonSettings.SetSpeed(speed);
        Time.timeScale = speed;
    }

    void OnPause(GameObject menu)
    {
        state = GlobalState.PAUSE;
        menu.SetActive(true);
    }

    void OnPauseOff(GameObject menu)
    {
        state = GlobalState.GAME;
        menu.SetActive(false);
    }

    public void PauseProcessing(GameObject PauseMenu)
    {
        switch (state)
        {
            case GlobalState.GAME:
            case GlobalState.END:
                OnPause(PauseMenu);
                break;
            case GlobalState.PAUSE:
                OnPauseOff(PauseMenu);
                break;
        }
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/rozumanton/");
    }
}
