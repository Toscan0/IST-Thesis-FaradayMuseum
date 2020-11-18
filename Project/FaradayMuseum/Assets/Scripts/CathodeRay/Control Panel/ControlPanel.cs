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
    [SerializeField]
    [Tooltip("Stuff to disable when connected to BLE")]
    private GameObject[] stuffToDisable;

    private int count = 0;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    private void OnEnable()
    {
        BLEManager.OnBluetoothConnected += DisableValuesChangers;
    }

    private void DisableValuesChangers(bool toDisable)
    {
       foreach(var stuff in stuffToDisable)
       {
            var inputField = stuff.GetComponent<TMP_InputField>();
            if(inputField != null)
            {
                inputField.enabled = !toDisable;
            }
            else
            {
                stuff.SetActive(!toDisable);
            }
            
        }
    }

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


    private void OnDisable()
    {
        BLEManager.OnBluetoothConnected -= DisableValuesChangers;
    }
}
