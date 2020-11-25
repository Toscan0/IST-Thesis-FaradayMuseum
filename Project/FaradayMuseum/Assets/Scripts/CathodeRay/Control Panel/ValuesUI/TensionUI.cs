using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Globalization;
using System.Text.RegularExpressions;

[RequireComponent(typeof(ControlPanel))]
public class TensionUI : MonoBehaviour
{
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();
    public static Action<float> OnTensionChanged;

    [SerializeField]
    private TMP_InputField tensionInput;

    [SerializeField]
    private TextMeshProUGUI minText;
    [SerializeField]
    private TextMeshProUGUI maxText;

    [SerializeField]
    private float tension;
    [SerializeField]
    private float minTension;
    [SerializeField]
    private float maxTension;

    private ControlPanel controlPanel;

    void Start()
    {
        controlPanel = GetComponent<ControlPanel>();

        OnTensionChanged?.Invoke(tension);

        // --- update the UI  ----
        // Input
        tensionInput.text = tension.ToString();
        // Text
        minText.text = minTension.ToString();
        maxText.text = maxTension.ToString();
        // --- --- ----
    }

    public void InputChanged(string s)
    {
        if (float.TryParse(s, out float f))
        {
            f = controlPanel.CheckMin(minTension, f);
            f = controlPanel.CheckMax(maxTension, f);

            SetTension(f);
        }
    }

    public void SliderChanged(float f)
    {
        SetTension(f);

        controlPanel.SendDataToBLE("Tension: " + f);
    }

    public void OnIncrementButtonClick()
    {
        SetTension(tension + 1);
    }

    public void OnDecrementButtonClick()
    {
        SetTension(tension - 1);
    }

    public void SetTension(float f)
    {
        f = controlPanel.CheckMin(minTension, f);
        f = controlPanel.CheckMax(maxTension, f);

        tension = f;

        tensionInput.text = f.ToString();

        OnTensionChanged?.Invoke(f);

        singleton.AddGameEvent(LogEventType.Tension, f.ToString());
    }
}
