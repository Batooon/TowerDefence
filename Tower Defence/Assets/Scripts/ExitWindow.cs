using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitWindow : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitWindowProcessing();
    }

    public void ExitWindowProcessing()
    {
        if (!gameObject.activeInHierarchy)
            OpenExitWindow();
        else
            CloseExitWindow();
    }

    private void OpenExitWindow()
    {
        gameObject.SetActive(true);
    }

    private void CloseExitWindow()
    {
        gameObject.SetActive(false);
    }
}
