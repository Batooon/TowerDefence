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

    void Start()
    {
        ChangeGameSpeed(1f);
    }

    public static void ChangeGameSpeed(float speed)
    {
        Settings.singletonSettings.SetSpeed(speed);
        Time.timeScale = speed;
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
        ChangeGameSpeed(1f);
        SceneManager.LoadScene(index);
        //LevelChanger.singleton.FadeToLevel(index);
    }

    public void TutorialPassed()
    {
        IsTutorialPassed = true;
        PlayerPrefs.SetInt("IsTutorialPassed", IsTutorialPassed.GetHashCode());
        ChangeGameSpeed(1f);
    }
}
