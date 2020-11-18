using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowARButton : MouseOverButton
{
    [SerializeField]
    private bool showAR = true;
    [SerializeField]
    private GameObject hideARButton;
    [SerializeField]
    private GameObject showARButton;
    [SerializeField]
    private GameObject[] objsToShow;
    [SerializeField]
    private Text tooltipText;

    //Delegate an event
    public static Action<bool> OnARButtonClicked;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void OnButtonClick()
    {
        showAR = !showAR;

        for (int i = 0; i < objsToShow.Length; i++)
        {
            objsToShow[i].SetActive(showAR);
        }

        hideARButton.SetActive(showAR);
        showARButton.SetActive(!showAR);

        // Send a event msg
        OnARButtonClicked?.Invoke(showAR);

        singleton.AddGameEvent(LogEventType.Click, "ShowAR: " + showAR);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        string text;

        if (showAR)
        {
            text = "Hide AR";
        }
        else
        {
            text = "Show AR";
        }

        tooltipText.text = text;
        Tooltip.SetActive(true);
    }
}
