using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Camera cam;
    public static LevelUI singletonUI;
    public GameObject turretAlertText;
    public Vector3 turretAllertOffset;
    public Text waveText;

    public float fadeSpeed = 2f;

    public Level level;
    // Start is called before the first frame update
    void Start()
    {
        level.waveSpawner.onWaveStateChanged += ChangeWaveText;
        BuildManager.singleton.onTurretNull += ShowTurretAlertText;
    }

    // Update is called once per frame
    void Update()
    {
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
}
