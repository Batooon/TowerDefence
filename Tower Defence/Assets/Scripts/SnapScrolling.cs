﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    public float Spacing;

    public CampaignButtonUI[] campaignCards;
    public ScrollRect scrollRect;

    public float snapSpeed;
    public float scaleOffset;
    public float scaleSpeed;

    GameObject[] instantiatedPanels;
    Vector2[] panelPositions;
    Vector3[] panelScales;
    Button[] instantiatedPanelButtons;

    int selectedPanID;

    bool isScrolling;

    RectTransform contentRect;

    Vector2 contentVector;

    private void Start()
    {
        contentRect = GetComponent<RectTransform>();
        instantiatedPanels = new GameObject[campaignCards.Length];
        instantiatedPanelButtons = new Button[instantiatedPanels.Length];
        panelScales = new Vector3[campaignCards.Length];
        panelPositions = new Vector2[campaignCards.Length];
        for (int i = 0; i < campaignCards.Length; i++)
        {
            instantiatedPanels[i] = Instantiate(campaignCards[i].gameObject, transform, false);
            instantiatedPanels[i].GetComponent<CampaignButtonUI>().Init(i + 1);
            instantiatedPanelButtons[i] = instantiatedPanels[i].gameObject.GetComponent<Button>();
            if (i == 0) continue;
            instantiatedPanels[i].transform.localPosition = new Vector2(instantiatedPanels[i - 1].transform.localPosition.x +
                instantiatedPanels[i].GetComponent<RectTransform>().sizeDelta.x + Spacing,
                instantiatedPanels[i].transform.localPosition.y);
            panelPositions[i] = -instantiatedPanels[i].transform.localPosition;
        }
    }

    void FixedUpdate()
    {
        if (contentRect.anchoredPosition.x >= panelPositions[0].x && !isScrolling || contentRect.anchoredPosition.x <= panelPositions[panelPositions.Length - 1].x && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < campaignCards.Length; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - panelPositions[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }
            float scale = Mathf.Clamp(1 / (distance / Spacing) * scaleOffset, 0.5f, 1f);
            panelScales[i].x = Mathf.SmoothStep(instantiatedPanels[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            panelScales[i].y = Mathf.SmoothStep(instantiatedPanels[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            panelScales[i].z = 1;
            instantiatedPanels[i].transform.localScale = panelScales[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling)
            scrollRect.inertia = false;

        if (isScrolling || scrollVelocity > 400)
            return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, panelPositions[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
        for (int i = 0; i < instantiatedPanelButtons.Length; i++)
        {
            if (i == selectedPanID)
            {
#if UNITY_ANDROID
                //Диииико извиняюсь, ДИИИКО
                if (instantiatedPanels[i].GetComponent<CampaignButtonUI>().IsLocked)
                    continue;
#endif
                instantiatedPanelButtons[i].interactable = true;
                continue;
            }
            instantiatedPanelButtons[i].interactable = false;
        }
    }

    public void Scrolling(bool IsScrolling)
    {
        isScrolling = IsScrolling;
        if (IsScrolling)
            scrollRect.inertia = true;
    }
}
