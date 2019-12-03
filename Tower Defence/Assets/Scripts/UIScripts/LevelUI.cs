using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{
    public Camera cam;

    [Header("Attributes for turret alert text")]
    [Space(20)]
    public GameObject turretAlertText;
    public GameObject notEnoughMoneyText;
    public GameObject maxTurretLevelText;
    public Vector3 turretAllertOffset;

    [Space(20)]
    public char currency;


    [Header("Wave countdown text")]
    [Space(20)]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI waveCounterText;

    [Header("Money Text")]
    [Space(20)]
    public TextMeshProUGUI money;

    [Header("Lives Text")]
    public TextMeshProUGUI livesText;
    public Color startColor;
    public Color loseLifeColor;

    [Header("Enemies Counter Text")]
    public TextMeshProUGUI scoreText;

    [Header("Speed Button")]
    public Image speedButton;
    public Sprite firstSpeed;
    public Sprite secondSpeed;
    public Sprite thirdSpeed;

    /*[Header("Game Win Data")]
    public GameObject WinScreen;
    public TextMeshProUGUI EnemiesKilledText;

    [Header("Game Loose Data")]
    public GameObject GameOverScreen;*/

    [Space(20)]

    public Level level;

    Vector3 pos2D;

    void Awake()
    {
        livesText.text = level.Hp.ToString();
        money.text = currency + level.buildManager.money.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        waveCounterText.text = $"{Level.singleton.waveSpawner.waveIndex + 1}/{Level.singleton.waveSpawner.amountOfWaves}";
        Level.singleton.waveSpawner.WavePassed += UpdateWaveCounter;
        level.buildManager.MoneyUpdate += OnMoneyUpdate;
        level.waveSpawner.onWaveStateChanged += ChangeWaveText;
        level.buildManager.TurretAlert += ShowTurretAlertText;
        level.buildManager.LivesUpdate += OnLivesUpdate;
        Level.singleton.EnemiesCounterChange += OnScoreUpdate;
        level.buildManager.TurretMaxLevelAllert += ShowMaxTurretLevel;
        level.buildManager.NotEnoughMoney += ShowNotEnoughMoneyText;
        Level.singleton.SpeedChange += ChangeSpeedUI;
        /*Level.singleton.OnWinGame += Win;
        Level.singleton.OnLooseGame += Loose;*/
    }

    public void OnScoreUpdate()
    {
        scoreText.text = "Score: " + level.EnemiesCounter;
    }

    public void ChangeWaveText()
    {
        switch (level.waveSpawner.state)
        {
            case State.COUNTDOWN:
                //waveText.color = Color.black;
                waveText.faceColor = Color.black;
                waveText.text = string.Format("{0:00.00}", level.waveSpawner.countdown);
                break;
            case State.SPAWN:
                //waveText.color = Color.red;
                waveText.faceColor = Color.red;
                waveText.text = "!WARNING! Wave incoming!";
                break;
            case State.END:
                level.waveSpawner.onWaveStateChanged -= ChangeWaveText;
                break;
        }
    }

    public void ShowTurretAlertText(Transform position)
    {
        Vector3 pos2D = cam.WorldToScreenPoint(position.position);
        if (!level.buildManager.CanBuild())
        {
            GameObject AlertText = Instantiate(turretAlertText, pos2D + turretAllertOffset, Quaternion.identity);
            AlertText.transform.SetParent(transform);
            AlertText.transform.localScale = Vector3.one;
            return;
        }

        if (!level.buildManager.IsEnoughMoney())
        {
            GameObject Text = Instantiate(notEnoughMoneyText, pos2D + turretAllertOffset, Quaternion.identity);
            Text.transform.SetParent(transform);
            Text.transform.localScale = Vector3.one;
            return;
        }
    }

    public void ShowNotEnoughMoneyText(Transform position)
    {
        Vector3 pos2D = cam.WorldToScreenPoint(position.position);
        GameObject AlertText = Instantiate(notEnoughMoneyText, pos2D + turretAllertOffset, Quaternion.identity);
        AlertText.transform.SetParent(transform);
        AlertText.transform.localScale = Vector3.one;
    }

    public void ShowMaxTurretLevel(Transform position)
    {
        Vector3 pos2D = cam.WorldToScreenPoint(position.position);
        GameObject text = Instantiate(maxTurretLevelText, pos2D + turretAllertOffset, Quaternion.identity);
        text.transform.SetParent(transform);
        text.transform.localScale = Vector3.one;
    }

    public void OnMoneyUpdate()
    {
        money.text = currency + level.buildManager.money.ToString();
    }

    /*public void Win()
    {
        EnemiesKilledText.text = WaveSpawner.EnemiesKilled.ToString();
        WinScreen.SetActive(true);
    }*/

    /*public void Loose()
    {
        GameOverScreen.SetActive(true);
    }*/

    public void OnLivesUpdate()
    {
        livesText.text = level.Hp.ToString();
    }

    public void ChangeSpeedUI(int speedIndex)
    {
        switch (speedIndex)
        {
            case 0:
                speedButton.sprite = firstSpeed;
                break;
            case 1:
                speedButton.sprite = secondSpeed;
                break;
            case 2:
                speedButton.sprite = thirdSpeed;
                break;
            default:
                speedButton.sprite = firstSpeed;
                break;
        }
    }

    public void UpdateWaveCounter()
    {
        waveCounterText.text = $"{Level.singleton.waveSpawner.waveIndex + 1}/{Level.singleton.waveSpawner.amountOfWaves}";
    }
}
