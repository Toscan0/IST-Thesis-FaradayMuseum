using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnaireButton : MonoBehaviour
{
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    private string URL = "https://docs.google.com/forms/d/e/1FAIpQLSdZq6hoDsuPIF5vdX1ceHgA2nduqopJGreP97TT2GgTjH3mug/viewform?usp=pp_url&entry.596911872=";

    public void OpenURL()
    {
        URL += singleton.LogFileName;
        Application.OpenURL(URL);

        singleton.AddGameEvent(LogEventType.Click, "Questionnaire");
    }
}
