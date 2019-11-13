using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CampaignButtonUI : MonoBehaviour
{
    public int CampaignId;

    public bool IsLocked;

    [SerializeField]
    Image background;
    [SerializeField]
    TextMeshProUGUI CampaignName;
    [SerializeField]
    GameObject Lock;

    public CampaignObject campaign;

    string sceneName;
    Button button;

    public void Init()
    {
        if (IsLocked)
            Lock.SetActive(true);
        else
            Lock.SetActive(false);

        sceneName = campaign.SceneName;
        background.sprite = campaign.Background;
        CampaignName.text = campaign.CampaignName;
        button = GetComponent<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene(sceneName));
    }
}
