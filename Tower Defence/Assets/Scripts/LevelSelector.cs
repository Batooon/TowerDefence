using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    //CampaignButtonUI[] buttons;

    public void Init(CampaignButtonUI button)
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        //buttons = gameObject.GetComponent<SnapScrolling>().campaignCards;
        //if()

        /*for (int i = 0; i < buttons.Length; i++)
        {
            if (i > levelReached - 1)
                buttons[i].IsLocked = true;
            else
                buttons[i].IsLocked = false;

            buttons[i].Init();
        }*/
    }
        
}