using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUps : MonoBehaviour
{
    public static Action<bool> OnShowPopUps;

    [SerializeField]
    private bool showPopUps = false;
    
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    private void Awake()
    {
        PopUps.OnPopUpDisable += ShowPopUpsToFalse;
        MyTrackableEventHandler.OnTrackingObj += ShowOrHide;
    }

    private void ShowOrHide(bool show)
    {
        if (show)
        {
            showPopUps = true;            
        }
        else
        {
            showPopUps = false;
        }

        OnShowPopUps?.Invoke(showPopUps);
    }

    private void ShowPopUpsToFalse()
    {
        showPopUps = false;
    }

    public void OnButtonClick()
    {
        showPopUps = !showPopUps;

        OnShowPopUps?.Invoke(showPopUps);

        singleton.AddGameEvent(LogEventType.Click, "Show pop-ups: " + showPopUps);
    }

    private void OnDestroy()
    {
        PopUps.OnPopUpDisable -= ShowPopUpsToFalse;
    }
}
