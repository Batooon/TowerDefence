using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game singleton;

    private bool IsTutorialPassed;

    public event Action<int> ChangeLevel;

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
            ChangeLevel?.Invoke(0);
            //SceneManager.LoadScene(0);
    }

    public void GoToLevel(int index)
    {
        ChangeLevel?.Invoke(index);
        //SceneManager.LoadScene(index);
    }

    public void TutorialPassed()
    {
        IsTutorialPassed = true;
        PlayerPrefs.SetInt("IsTutorialPassed", IsTutorialPassed.GetHashCode());
    }
}
