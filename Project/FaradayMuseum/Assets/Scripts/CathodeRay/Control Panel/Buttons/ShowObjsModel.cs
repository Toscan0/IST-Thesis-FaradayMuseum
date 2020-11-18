using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowObjsModel : MouseOverButton
{
    [SerializeField]
    private bool showObjs = true;
    [SerializeField]
    private GameObject hideButton;
    [SerializeField]
    private GameObject showButton;
    [SerializeField]
    private GameObject[] objsToShow;
    [SerializeField]
    private Text tooltipText;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void OnButtonClick()
    {
        showObjs = !showObjs;

        for(int i = 0; i < objsToShow.Length; i++)
        {
            objsToShow[i].SetActive(showObjs);
        }

        hideButton.SetActive(showObjs);
        showButton.SetActive(!showObjs);
        
        singleton.AddGameEvent(LogEventType.Click, "ShowObjs: " + showObjs);
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        string text;

        if (showObjs)
        {
            text = "Hide Objects";
        }
        else
        {
            text = "Show Objects";
        }

        tooltipText.text = text;
        Tooltip.SetActive(true);
    }
}
