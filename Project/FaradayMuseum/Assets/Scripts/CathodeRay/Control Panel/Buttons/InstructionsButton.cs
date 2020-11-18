using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsButton : MonoBehaviour
{
    public static Action<bool> OnInstructionsButtonClicked;

    [SerializeField]
    private bool instructionsEnabled = false;
    

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void OnButtonClick()
    {
        instructionsEnabled = !instructionsEnabled;

        OnInstructionsButtonClicked?.Invoke(instructionsEnabled);

        singleton.AddGameEvent(LogEventType.Click, "Instructions: " + instructionsEnabled); 
    }


   
}
