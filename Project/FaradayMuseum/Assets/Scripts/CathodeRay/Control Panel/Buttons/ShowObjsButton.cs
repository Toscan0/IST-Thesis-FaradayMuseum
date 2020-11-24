using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowObjsButton : MonoBehaviour
{
    public static Action<bool> OnShowObjsButtonClicked;

    [SerializeField]
    private bool showObjs = true;
    [SerializeField]
    private GameObject hideButton;
    [SerializeField]
    private GameObject showButton;

    

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void OnButtonClick()
    {
        showObjs = !showObjs;

        OnShowObjsButtonClicked?.Invoke(showObjs);

        // Change buttons
        hideButton.SetActive(showObjs);
        showButton.SetActive(!showObjs);
        
        singleton.AddGameEvent(LogEventType.Click, "ShowObjs: " + showObjs);
    }
}
