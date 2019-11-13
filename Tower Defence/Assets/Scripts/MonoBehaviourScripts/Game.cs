using System;
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
            CampaignMenu.SetActive(true);
        else
            SceneManager.LoadScene(0);
            //LevelChanger.singleton.FadeToLevel(0);
    }

    public void GoToLevel(int index)
    {
        SceneManager.LoadScene(index);
        //LevelChanger.singleton.FadeToLevel(index);
    }

    public void TutorialPassed()
    {
        IsTutorialPassed = true;
        PlayerPrefs.SetInt("IsTutorialPassed", IsTutorialPassed.GetHashCode());
        Time.timeScale = 1;
    }
}
