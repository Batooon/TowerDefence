using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CampaignButtonUI : MonoBehaviour
{
    [SerializeField]
    Image background;
    [SerializeField]
    TextMeshProUGUI CampaignName;

    string sceneName;
    Button button;

    public void Init(CampaignObject campaign)
    {
        sceneName = campaign.SceneName;
        background.sprite = campaign.Background;
        CampaignName.text = campaign.CampaignName;
        button = GetComponent<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene(sceneName));
    }
}
