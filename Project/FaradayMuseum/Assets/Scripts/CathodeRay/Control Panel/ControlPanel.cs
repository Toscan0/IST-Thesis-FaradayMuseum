using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    private bool UIEnabled = false;
    [SerializeField]
    private Animator UIAnimator;
    
    [SerializeField]
    private GameObject UI;
    //Warning
    [SerializeField]
    private GameObject warning;
    [SerializeField]
    private TextMeshProUGUI warningText;  
    
    private int count = 0;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void OnButtonClick()
    {
        UIEnabled = !UIEnabled;

        singleton.AddGameEvent(LogEventType.Click, "Control Panel: " + UIEnabled);
        
        UIAnimator.SetBool("IsOpen", !UIEnabled);
    }

    public IEnumerator WarningTheUsers(string s)
    {
        if(count == 0)
        {
            count++;
            yield break;
        }
        warning.SetActive(true);
        warningText.text = s;
        singleton.AddGameEvent(LogEventType.Warning);

        yield return new WaitForSeconds(1.5f);

        warning.SetActive(false);
    }

    public float CheckMin(float min, float f)
    {
        if (f < min)
        {
            StartCoroutine(WarningTheUsers("The min value is: " + min + "."));

            f = min;
        }
        return f;
    }

    public float CheckMax(float max, float f)
    {
        if (f > max)
        {
            StartCoroutine(WarningTheUsers("The max value is: " + max + "."));

            f = max;
        }
        return f;
    }

    public void SendDataToBLE(string s) { 

    }
}
