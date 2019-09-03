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
    public Vector3 turretAllertOffset;

    [Space(20)]
    public char currency;


    [Header("Wave countdown text")]
    [Space(20)]
    public TextMeshProUGUI waveText;

    [Header("Money Text")]
    [Space(20)]
    public TextMeshProUGUI money;

    [Header("Lives Text")]
    public TextMeshProUGUI livesText;

    [Space(20)]

    public Level level;

    void Awake()
    {
        livesText.text = level.Hp.ToString();
        money.text = currency + level.buildManager.money.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        level.buildManager.MoneyUpdate += OnMoneyUpdate;
        level.waveSpawner.onWaveStateChanged += ChangeWaveText;
        level.buildManager.TurretAlert += ShowTurretAlertText;
        level.buildManager.LivesUpdate += OnLivesUpdate;
    }

    public void ChangeWaveText()
    {
        switch (level.waveSpawner.state)
        {
            case State.COUNTDOWN:
                waveText.color = Color.black;
                waveText.text = string.Format("{0:00.00}", level.waveSpawner.countdown);
                break;
            case State.SPAWN:
                waveText.color = Color.red;
                waveText.text = "!WARNING! Wave incoming!";
                break;
            case State.END:
                Destroy(gameObject);
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

    public void OnMoneyUpdate()
    {
        money.text = currency + level.buildManager.money.ToString();
    }

    public void OnLivesUpdate()
    {
        livesText.text = level.Hp.ToString();
    }
}
