using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitWindow : MonoBehaviour
{
    public GameObject exitWindow;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitWindowProcessing();
    }

    public void ExitWindowProcessing()
    {
        if (!exitWindow.gameObject.activeInHierarchy)
            OpenExitWindow();
        else
            CloseExitWindow();
    }

    private void OpenExitWindow()
    {
        exitWindow.SetActive(true);
    }

    private void CloseExitWindow()
    {
        exitWindow.SetActive(false);
    }
}
