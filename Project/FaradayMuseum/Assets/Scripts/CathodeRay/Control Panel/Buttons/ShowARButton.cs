using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowARButton : MonoBehaviour
{
    public static Action<bool> OnARButtonClicked;

    [SerializeField]
    private bool showAR = true;
    [SerializeField]
    private GameObject hideARButton;
    [SerializeField]
    private GameObject showARButton;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void OnButtonClick()
    {
        showAR = !showAR;

        OnARButtonClicked?.Invoke(showAR);

        // Change button
        hideARButton.SetActive(showAR);
        showARButton.SetActive(!showAR);

        singleton.AddGameEvent(LogEventType.Click, "ShowAR: " + showAR);
    }
}
