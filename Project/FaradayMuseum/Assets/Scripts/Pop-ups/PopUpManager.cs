using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    private List<string> popUpsOpened = new List<string>();

    [SerializeField]
    private GameObject initialExplanation;
    [SerializeField]
    private GameObject explanation;
    [SerializeField]
    private GameObject endPopUp;
    [SerializeField]
    private ArrowsManager arrowsManager;

    private InitialExplicationDisplay initialExplicationDisplay;
    private ExplanationDisplay explanationDisplay;

    private int count = 0;

    private void Awake()
    {
        PopUps.OnPopUpEnable += AddPopUp;
        ShowPopUps.OnShowPopUps += ShowOrHide;

        initialExplicationDisplay = initialExplanation.GetComponent<InitialExplicationDisplay>();
        explanationDisplay = explanation.GetComponent<ExplanationDisplay>();
    }

    private void Start()
    {
        ShowOrHide(true);
        count = 1;
    }

    private void AddPopUp(string popUp)
    {
        if(popUp != null && popUp != "")
        {
            popUpsOpened.Add(popUp);
        }
        

        if(popUp != "IE")
        {
            initialExplanation.SetActive(false);
        }
        if (popUp == "End")
        {
            explanation.SetActive(false);
        }

        count = popUpsOpened.Count;
        arrowsManager.ShowArrows();
        UpdateArrows();
    }

    private void ShowOrHide(bool toShow)
    {
        if (toShow)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        initialExplanation.SetActive(false);
        explanation.SetActive(false);
        endPopUp.SetActive(false);

        arrowsManager.HideArrows();
    }

    private void Show()
    {
        initialExplanation.SetActive(false);
        explanation.SetActive(false);
        endPopUp.SetActive(false);

        if (popUpsOpened.Count == 0)
        {
            return;
        }

        if (popUpsOpened.Count >= 1)
        {
            arrowsManager.ShowArrows();
            count = popUpsOpened.Count;

            UpdateArrows();

            string popUpToShow = popUpsOpened[popUpsOpened.Count - 1];
            if (popUpToShow == "IE")
            {
                initialExplanation.SetActive(true);
            }
            else if (popUpToShow == "End")
            {
                endPopUp.SetActive(true);
            }
            else
            {
                explanationDisplay.SetID(popUpToShow);
                explanation.SetActive(true);
            }
        }
    }
    

    public void OnArrowRightClick()
    {
        if (popUpsOpened.Count == 1)
        {
            return;
        }

        if (popUpsOpened.Count > count)
        {
            if(count == 1)
            {
                initialExplicationDisplay.Disable();
            }
            else
            {
                explanation.SetActive(false);
            }

            count++;

            if(popUpsOpened[count - 1] == "End")
            {
                endPopUp.SetActive(true);

                singleton.AddGameEvent(LogEventType.Click, "Right Arrow, Show: End pop-up");
            }
            else
            {
                explanationDisplay.SetID(popUpsOpened[count - 1]);
                explanation.SetActive(true);

                singleton.AddGameEvent(LogEventType.Click, "Right Arrow, Show: " + popUpsOpened[count - 1] + " Explanation");
            }
        }

        UpdateArrows();
    }

    public void OnArrowLeftClick()
    {
        if(popUpsOpened.Count == 1)
        {
            return;
        }

        if(count <= popUpsOpened.Count)
        {
            
            count--;

            string popUpID = popUpsOpened[count-1];
            if (popUpID == "IE")
            {
                explanation.SetActive(false);

                initialExplanation.SetActive(true);

                singleton.AddGameEvent(LogEventType.Click, "Left Arrow, Show: Initial Explanation");
            }
            else
            {
                endPopUp.SetActive(false);
                initialExplanation.SetActive(false);

                explanation.SetActive(false);
                explanationDisplay.SetID(popUpID);
                explanation.SetActive(true);

                singleton.AddGameEvent(LogEventType.Click, "Left Arrow, Show: " + popUpID + " Explanation");
            }

            UpdateArrows();
        }
    }

    private void UpdateArrows()
    {
        // Left Arrow
        if (count == 1)
        {
            arrowsManager.SetLeftArrowInteractable(false);
        }
        else
        {
            arrowsManager.SetLeftArrowInteractable(true);
        }

        // Right Arrow
        if (count == popUpsOpened.Count)
        {
            arrowsManager.SetRightArrowInteractable(false);
        }
        else if (count < popUpsOpened.Count)
        {
            arrowsManager.SetRightArrowInteractable(true);
        }
    }

    private void OnDestroy()
    {
        PopUps.OnPopUpEnable -= AddPopUp;
        ShowPopUps.OnShowPopUps -= ShowOrHide;
    }
}
