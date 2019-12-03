using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CampaignButtonUI : MonoBehaviour
{
    private AudioManager audioManager;

    [HideInInspector]
    public bool IsLocked;

    public Image background;

    public TextMeshProUGUI CampaignName;

    public GameObject Lock;

    public CampaignObject campaign;

    public string sceneName;
    public Button button;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
            Debug.LogWarning("Can't find audio manager in the scene!");
    }

    public void Init(int levelIndex)
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        IsLocked = levelIndex > levelReached;

#if UNITY_ANDROID
        if (IsLocked)
            Lock.SetActive(true);
        else
            Lock.SetActive(false);
#endif

        sceneName = campaign.SceneName;
        background.sprite = campaign.Background;
        CampaignName.text = campaign.CampaignName;
        button.onClick.AddListener(() => SceneManager.LoadScene(sceneName));
        button.onClick.AddListener(() => audioManager.Play("ButtonSound"));
    }

    public void UpdateLock()
    {
        if (IsLocked)
            return;
        else
            Lock.SetActive(false);
    }
}
