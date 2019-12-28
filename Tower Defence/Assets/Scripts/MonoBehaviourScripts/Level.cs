using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GlobalState
{
    GAME,
    PAUSE,
    END,
    TUTORIALPAUSE
}
public class Level : MonoBehaviour
{
    public static Level singleton;

    public float[] speeds = new float[3];

    public BuildManager buildManager;
    public WaveSpawner waveSpawner;

    //public ItemsSpawner itemsSpawner;

    public int levelIndex;

    int speedIndex = 0;

    /*public event Action OnWinGame;
    public event Action OnLooseGame;*/

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
    public GameObject PauseMenu;
    public GameObject ExitWindow;
    public GameObject GameOverScreen;
    public GameObject GameWinScreen;
    public TextMeshProUGUI WinEnemiesKilledText;
    public TextMeshProUGUI LooseEnemiesKilledText;

    public Action<int> SpeedChange;

    private GlobalState _state;
    public GlobalState state
    {
        get => _state;
        protected set
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
            ExitWindow.SetActive(true);
        }
    }

    private void CloseExitWindow()
    {
        if (PauseMenu.gameObject.activeInHierarchy)
            ExitWindow.SetActive(false);
        else
        {
            ChangeState(GlobalState.GAME);
            ExitWindow.SetActive(false);
        }
    }

    private void Start()
    {
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

    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetActiveScene().buildIndex)
        ChangeState(GlobalState.GAME);
    }

    public void GameOver(GameObject gameOverScreen)
    {
        ChangeState(GlobalState.END);
        gameOverScreen.SetActive(true);
    }

    public void ChangeGameSpeed()
    {
        if (speedIndex >= 2)
            speedIndex = 0;
        else
            speedIndex += 1;
        Settings.singletonSettings.SetSpeed(speeds[speedIndex]);
        Time.timeScale = speeds[speedIndex];
        SpeedChange?.Invoke(speedIndex);
    }

    void OnPause(GameObject menu)
    {
        ChangeState(GlobalState.PAUSE);
        menu.SetActive(true);
    }

    void OnPauseOff(GameObject menu)
    {
        ChangeState(GlobalState.GAME);
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

    void ActivatGameOverScreen()
    {
        LooseEnemiesKilledText.text = Level.singleton.waveSpawner.EnemiesKilled.ToString();
        GameOverScreen.SetActive(true);
    }
    void ActivateWinGameScreen()
    {
        WinEnemiesKilledText.text = Level.singleton.waveSpawner.EnemiesKilled.ToString();
        GameWinScreen.SetActive(true);
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void RetryButton()
    {
        Unpause();
        //ChangeState(GlobalState.GAME);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Unpause()
    {
        Settings.singletonSettings.SetSpeed(speeds[speedIndex]);
        Time.timeScale = speeds[speedIndex];
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

    public void EndGame()
    {
        ChangeState(GlobalState.END);
        ActivatGameOverScreen();
        buildManager.ClearEvents();
        ClearEvents();
    }

    public void WinGame()
    {
        PlayerPrefs.SetInt("levelReached", levelIndex + 1);
        ChangeState(GlobalState.END);
        ActivateWinGameScreen();
        buildManager.ClearEvents();
        ClearEvents();
    }

    public void OnEnemyDied(EnemyDieEvent dieEvent)
    {
        waveSpawner.EnemiesKilled++;
        EnemiesCounter += Score;
        EnemiesCounterChange?.Invoke();
        waveSpawner.EnemiesAlive--;
        buildManager.AddMoney(dieEvent.moneyBonus);

        //itemsSpawner.InitDrop(dieEvent);
    }

    void ClearEvents()
    {
        //Time.timeScale = 1;
    }
}
