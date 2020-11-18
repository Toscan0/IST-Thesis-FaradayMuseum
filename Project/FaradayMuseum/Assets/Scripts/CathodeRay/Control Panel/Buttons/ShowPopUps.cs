using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUps : MouseOverButton
{
    public static Action<bool> OnShowPopUps;

    [SerializeField]
    private bool showPopUps = false;
    
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    private void Awake()
    {
        PopUps.OnPopUpDisable += ShowPopUpsToFalse;
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
