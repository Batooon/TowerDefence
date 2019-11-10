using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Campaign", menuName = "New Campaign")]
public class CampaignObject : ScriptableObject
{
    public string SceneName;

    public Sprite Background;

    public string CampaignName;
}
