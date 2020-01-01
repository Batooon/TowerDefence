using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Developer: Antoshka

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField]
    GameObject exitWindow;

    MainMenuManager mainMenuManager;

    private void Awake()
    {
        InitExitWindow();
        AddMainMenuManager();

        mainMenuManager.Init();
    }

    public void InitExitWindow()
    {
        GameObject exit = Instantiate(exitWindow, transform, false);
        exit.AddComponent<ExitWindow>();
        exit.SetActive(false);
    }

    public void AddMainMenuManager()
    {
        mainMenuManager = gameObject.AddComponent<MainMenuManager>();
    }
}
