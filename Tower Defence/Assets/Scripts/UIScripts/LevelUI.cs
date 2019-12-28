using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class LevelUI : MonoBehaviour
{
    [Inject]
    Level level;
    [Inject]
    BuildManager buildManager;
    [Inject]
    WaveSpawner waveSpawner;

    public Camera cam;

    [Header("Attributes for turret alert text")]
    [Space(20)]
    public GameObject turretAlertText;
    public GameObject notEnoughMoneyText;
    public GameObject maxTurretLevelText;
    public Vector3 turretAllertOffset = new Vector3(0, 45, 0);

    [Space(20)]
    public char currency = '₴';


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

    Vector3 pos2D;

    /*private void OnValidate()
    {
        cam = Camera.main;

        waveText = transform.Find("WaveCountdown").GetComponent<TextMeshProUGUI>();
        waveCounterText = transform.Find("WaveCounterText").GetComponent<TextMeshProUGUI>();
    }*/

    void Awake()
    {
        livesText.text = level.Hp.ToString();
        money.text = currency + buildManager.money.ToString();
    }

    void Start()
    {
        waveCounterText.text = $"{waveSpawner.waveIndex + 1}/{waveSpawner.amountOfWaves}";
        waveSpawner.WavePassed += UpdateWaveCounter;
        buildManager.MoneyUpdate += OnMoneyUpdate;
        waveSpawner.onWaveStateChanged += ChangeWaveText;
        buildManager.TurretAlert += ShowTurretAlertText;
        buildManager.LivesUpdate += OnLivesUpdate;
        level.EnemiesCounterChange += OnScoreUpdate;
        buildManager.TurretMaxLevelAllert += ShowMaxTurretLevel;
        buildManager.NotEnoughMoney += ShowNotEnoughMoneyText;
        level.SpeedChange += ChangeSpeedUI;
    }

    public void OnScoreUpdate()
    {
        scoreText.text = "Score: " + level.EnemiesCounter;
    }

    public void ChangeWaveText()
    {
        switch (waveSpawner.state)
        {
            case State.COUNTDOWN:
                waveText.faceColor = Color.black;
                waveText.text = string.Format("{0:00.00}", waveSpawner.countdown);
                break;
            case State.SPAWN:
                waveText.faceColor = Color.red;
                waveText.text = "!WARNING! Wave incoming!";
                break;
            case State.END:
                waveSpawner.onWaveStateChanged -= ChangeWaveText;
                break;
        }
    }

    public void ShowTurretAlertText(Transform position)
    {
        Vector3 pos2D = cam.WorldToScreenPoint(position.position);
        if (!buildManager.CanBuild())
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
        money.text = currency + buildManager.money.ToString();
    }

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
        waveCounterText.text = $"{waveSpawner.waveIndex + 1}/{waveSpawner.amountOfWaves}";
    }
}
