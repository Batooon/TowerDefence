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
    public static Level singleton;

    public BuildManager buildManager;
    public WaveSpawner waveSpawner;

    [HideInInspector]
    public int EnemiesCounter = 0;
    [Header("Сколько нужно засчитать очков за одно убийство")]
    public int Score;
    [Space(20)]
    public Action EnemiesCounterChange;

    [SerializeField]
    private string telegramAccountUrl;
    [SerializeField]
    private string instagramAccountUrl;
    public GameObject GameOverScreen;
    public GameObject PauseMenu;
    public GameObject ExitWindow;

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

    public void ChangeState(GlobalState State)
    {
        state = State;
    }

    public int Hp;

    private void Awake()
    {
        singleton = this;
        Hp = (int)Mathf.Clamp(Hp, 0f, Mathf.Infinity);
        buildManager.LivesUpdate += DecreaseHp;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitWindowProcessing();
    }

    public void ExitWindowProcessing()
    {
        if (!ExitWindow.gameObject.activeInHierarchy)
            OpenExitWindow();
        else
            CloseExitWindow();
    }

    private void OpenExitWindow()
    {
        if (PauseMenu.gameObject.activeInHierarchy)
            ExitWindow.SetActive(true);
        else
        {
            ChangeState(GlobalState.PAUSE);
            //state = GlobalState.PAUSE;
            ExitWindow.SetActive(true);
        }
    }

    private void CloseExitWindow()
    {
        if (PauseMenu.gameObject.activeInHierarchy)
            ExitWindow.SetActive(false);
        else
        {
            //state = GlobalState.GAME;
            ChangeState(GlobalState.GAME);
            ExitWindow.SetActive(false);
        }
    }

    private void Start()
    {
        //state = GlobalState.GAME;
        ChangeState(GlobalState.GAME);
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
        ChangeState(GlobalState.END);
        //state = GlobalState.END;
        gameOverScreen.SetActive(true);
    }

    public void ChangeGameSpeed(float speed)
    {
        Settings.singletonSettings.SetSpeed(speed);
        Time.timeScale = speed;
    }

    void OnPause(GameObject menu)
    {
        ChangeState(GlobalState.PAUSE);
        //state = GlobalState.PAUSE;
        menu.SetActive(true);
    }

    void OnPauseOff(GameObject menu)
    {
        ChangeState(GlobalState.GAME);
        //state = GlobalState.GAME;
        menu.SetActive(false);
    }

    public void PauseProcessing()
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
        ChangeState(GlobalState.END);
        //state = GlobalState.END;
        ActivateGameOverMenu();
        buildManager.ClearEvents();
    }
}
