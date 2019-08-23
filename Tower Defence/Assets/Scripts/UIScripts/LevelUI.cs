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
    public float fadeSpeed = 2f;
    public Vector3 turretAllertOffset;


    [Header("Wave countdown text")]
    [Space(20)]
    public Text waveText;

    [Header("Money Text")]
    [Space(20)]
    public TextMeshProUGUI money;

    [Space(20)]

    public Level level;

    // Start is called before the first frame update
    void Start()
    {
        money.text = level.buildManager.money.ToString();
        level.buildManager.onMoneyChanged += MoneyManager;
        level.waveSpawner.onWaveStateChanged += ChangeWaveText;
        level.buildManager.onTurretNull += ShowTurretAlertText;
    }

    public void ChangeWaveText()
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
                level.waveSpawner.onWaveStateChanged -= ChangeWaveText;
                break;
        }
    }

    public void ShowTurretAlertText(Transform position)
    {
        Vector3 pos2D = cam.WorldToScreenPoint(position.position);
        GameObject AlertText = Instantiate(turretAlertText, pos2D + turretAllertOffset, Quaternion.identity);
        AlertText.transform.SetParent(transform);
        AlertText.transform.localScale = Vector3.one;
    }

    public void MoneyManager()
    {
        money.text = level.buildManager.money.ToString();
    }
}
