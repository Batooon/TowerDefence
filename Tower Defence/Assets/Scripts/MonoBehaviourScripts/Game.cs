using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game singleton;

    private bool IsTutorialPassed;

    void Awake()
    {
        singleton = this;
        IsTutorialPassed = Convert.ToBoolean(PlayerPrefs.GetInt("IsTutorialPassed", 0));
    }

    public void OpenCampaignMenu(GameObject CampaignMenu)
    {
        if (IsTutorialPassed)
        {
            CampaignMenu.SetActive(true);
        }
        else
        {
            LevelManager.ActivateLevel(0);
        }
    }

    public void GoToLevel(int index)
    {
        LevelManager.ActivateLevel(index);
    }

    public void TutorialPassed()
    {
        IsTutorialPassed = true;
        PlayerPrefs.SetInt("IsTutorialPassed", IsTutorialPassed.GetHashCode());
    }
}
