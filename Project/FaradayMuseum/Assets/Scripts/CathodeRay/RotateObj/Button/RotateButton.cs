using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotateButton : MouseOverButton
{
    [SerializeField]
    private bool allowRotation = false;
    [SerializeField]
    private GameObject rotateButton;
    [SerializeField]
    private GameObject no_RotateButton;
    [SerializeField]
    private Text tooltipText;

    public static Action<bool> OnRotateButtonClicked;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void OnButtonClick()
    {
        allowRotation = !allowRotation;

        rotateButton.SetActive(!allowRotation);
        no_RotateButton.SetActive(allowRotation);

        OnRotateButtonClicked?.Invoke(allowRotation);

        singleton.AddGameEvent(LogEventType.Click, "Rotation: " + allowRotation);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        string text;

        if (allowRotation)
        {
            text = "Disable rotation";
        }
        else
        {
            text = "Allow rotation\n Use the mouse to rotate";
        }

        tooltipText.text = text;
        Tooltip.SetActive(true);
    }
}
