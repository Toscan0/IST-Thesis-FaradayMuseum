using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotationButton : MouseOverButton
{
    [SerializeField]
    private GameObject resetButton;

    public static Action OnResetRotationButtonClicked;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    private void Start()
    {
        RotateButton.OnRotateButtonClicked += EnableResetRotationButton;
    }

    // If allow rotation button was clicked
    private void EnableResetRotationButton(bool enbaleButton)
    {
        resetButton.SetActive(enbaleButton);
    }

    // If reset button was clicked
    public void OnButtonClick()
    {
        OnResetRotationButtonClicked?.Invoke();

        singleton.AddGameEvent(LogEventType.Click, "Reset Rotation");
    }

    private void OnDestroy()
    {
        RotateButton.OnRotateButtonClicked -= EnableResetRotationButton;
    }
}
