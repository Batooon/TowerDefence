using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public string telegramAccountUrl;
    public string instagramAccountUrl;
    public GameObject GameOverScreen;

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

    public int Hp;

    private void Awake()
    {
        Hp = (int)Mathf.Clamp(Hp, 0f, Mathf.Infinity);
        buildManager.LivesUpdate += DecreaseHp;
    }

    private void Start()
    {
        state = GlobalState.GAME;
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

    public void GameOver(GameObject gameOverScreen)
    {
        state = GlobalState.END;
        gameOverScreen.SetActive(true);
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
                OnPause(PauseMenu);
                break;
            case GlobalState.PAUSE:
                OnPauseOff(PauseMenu);
                break;
        }
    }

    void ActivateGameOverMenu()
    {
        GameOverScreen.SetActive(true);
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenInstagram()
    {
        Application.OpenURL(instagramAccountUrl);
    }

    public void OpenTelegram()
    {
        Application.OpenURL(telegramAccountUrl);
    }

    public void DecreaseHp()
    {
        Hp -= 1;
        if (Hp <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        state = GlobalState.END;
        ActivateGameOverMenu();
        buildManager.ClearEvents();
    }
}
