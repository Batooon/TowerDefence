using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialLevel : Level
{
    [Header("Tutorial Objects")]

    public GameObject topMenu;
    public GameObject shop;
    public GameObject scoreUI;
    public TextMeshProUGUI nextStepButtonText;

    private int numberStep = 0;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        //globalState = TutorialEnum.TUTORIALPAUSE;
        ChangeState(GlobalState.TUTORIALPAUSE);
        Debug.Log(Time.timeScale);
    }

    public void NextStep()
    {
        numberStep++;
        StopAllCoroutines();
        UpdateStep();
    }

    private void UpdateStep()
    {
        switch (numberStep)
        {
            case 1:
                FirstStep();
                break;
            case 2:
                SecondStep();
                break;
            case 3:
                ThirdStep();
                break;
            case 4:
                FourthStep();
                break;
            case 5:
                FifthStep();
                break;
            case 6:
                SixthStep();
                break;
        }
    }

    public void FirstStep()
    {
        topMenu.SetActive(true);
    }

    private void SecondStep()
    {
        shop.SetActive(true);
    }

    private void ThirdStep()
    {

    }

    private void FourthStep()
    {
        scoreUI.SetActive(true);
    }

    private void FifthStep()
    {
        nextStepButtonText.text = "Let's go!";
    }

    private void SixthStep()
    {
        //globalState = TutorialEnum.GAME;
        ChangeState(GlobalState.GAME);
        Game.singleton.TutorialPassed();
    }
}
