using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game singleton;

    List<TurretGameData> turretsGameDataList = new List<TurretGameData>();

    public List<GameObject> turretsTakenOnLevel = new List<GameObject>();

    private bool IsTutorialPassed;

    public GameObject exitWindow;

    void SerializeTurretsData()
    {
        TextAsset excelData = Resources.Load<TextAsset>("TurretsGameData");

        string[] data = excelData.text.Split('\n');

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(';');


            if (row[1] != "")
            {
                TurretGameData turretGameData = new TurretGameData();

                float.TryParse(row[1], out turretGameData.Range);
                float.TryParse(row[2], out turretGameData.FireRate);
                float.TryParse(row[3], out turretGameData.SpeedRotation);//TODO: Проверять каждое поле на корректность, в ином случае - CONTINUE
                float.TryParse(row[4], out turretGameData.FireCountdown);
                turretsGameDataList.Add(turretGameData);
            }
        }

        foreach(TurretGameData t in turretsGameDataList)
        {
            Debug.Log(t.FireRate);
        }
    }

    public void ExitWindowProcessing(GameObject window)
    {
        if (window.activeInHierarchy)
            window.SetActive(false);
        else
            window.SetActive(true);
    }

    void Awake()
    {
        singleton = this;
        IsTutorialPassed = Convert.ToBoolean(PlayerPrefs.GetInt("IsTutorialPassed", 0));
        SerializeTurretsData();
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
            SceneManager.LoadScene(1);
            //LevelChanger.singleton.FadeToLevel(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitWindowProcessing(exitWindow);
    }

    public void GoToLevel(int index)
    {
        ChangeGameSpeed(1f);
        SceneManager.LoadScene(index);
        //LevelChanger.singleton.FadeToLevel(index);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void TutorialPassed()
    {
        IsTutorialPassed = true;
        PlayerPrefs.SetInt("IsTutorialPassed", IsTutorialPassed.GetHashCode());
        ChangeGameSpeed(1f);
    }

    public void ClearSingletons()
    {
        BuildManager.singleton = null;
        Level.singleton = null;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
