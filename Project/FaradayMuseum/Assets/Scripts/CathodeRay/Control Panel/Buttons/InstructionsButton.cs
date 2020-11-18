using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsButton : MouseOverButton
{
    [SerializeField]
    private bool instructionsEnabled = false;
    [SerializeField]
    private GameObject inst;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void OnButtonClick()
    {
        instructionsEnabled = !instructionsEnabled;

        inst.SetActive(instructionsEnabled);

        singleton.AddGameEvent(LogEventType.Click, "Instructions: " + instructionsEnabled);
    }
}
