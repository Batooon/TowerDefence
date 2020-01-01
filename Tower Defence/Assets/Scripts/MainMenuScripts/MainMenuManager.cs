using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Developer: Antoshka

public class MainMenuManager : MonoBehaviour
{
    bool isTutorialPassed;

    GameObject campaignMenu;
    GameObject mainMenu;

    Button campaignButton;
    Button tutorialButton;
    Button infinityModeButton;
    Button backButton;

    public void Init()
    {
        isTutorialPassed = Settings.singletonSettings.IsTutorialPassed;
        mainMenu = transform.Find("MainMenu").gameObject;
        campaignMenu = transform.Find("CampaignMenu").gameObject;
        campaignButton = mainMenu.transform.Find("CMButton").GetComponent<Button>();
        tutorialButton = mainMenu.transform.Find("TutorialButton").GetComponent<Button>();
        infinityModeButton = mainMenu.transform.Find("InfinityModeButton").GetComponent<Button>();
        backButton = campaignMenu.transform.Find("BackButton").GetComponent<Button>();

        campaignButton.onClick.AddListener(() => CampaignMenuProceed());
        tutorialButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        infinityModeButton.onClick.AddListener(() => SceneManager.LoadScene(7));
        backButton.onClick.AddListener(() => CampaignMenuProceed());
    }

    public void CampaignMenuProceed()
    {
        if (isTutorialPassed && !campaignMenu.activeInHierarchy)
        {
            campaignMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        else if (!isTutorialPassed)
            SceneManager.LoadScene(1);
        else
        {
            mainMenu.SetActive(true);
            campaignMenu.SetActive(false);
        }
    }
}
