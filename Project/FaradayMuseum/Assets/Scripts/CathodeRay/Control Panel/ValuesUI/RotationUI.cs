using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Globalization;
using System.Text.RegularExpressions;

[RequireComponent(typeof(ControlPanel))]
public class RotationUI : MonoBehaviour
{
    [SerializeField]
    private ManageInput manageInput;

    [SerializeField]
    private TMP_InputField rotationInput;

    [SerializeField]
    private TextMeshProUGUI minText;
    [SerializeField]
    private TextMeshProUGUI maxText;

    [SerializeField]
    private float rotation;
    [SerializeField]
    private float minRotation;
    [SerializeField]
    private float maxRotation;

    private ControlPanel controlPanel;
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    void Start()
    {
        controlPanel = GetComponent<ControlPanel>();

        manageInput.Rotation = rotation;

        // --- update the UI  ----
        // Input
        rotationInput.text = rotation.ToString();
        // Text
        minText.text = minRotation.ToString();
        maxText.text = maxRotation.ToString();
        // --- --- ----
    }

    public void InputChanged(string s)
    {
        if (float.TryParse(s, out float f))
        {
            f = controlPanel.CheckMin(minRotation, f);
            f = controlPanel.CheckMax(maxRotation, f);

            SetRotation(f);
        }
    }

    public void SliderChanged(float f)
    {
        SetRotation(f);

        controlPanel.SendDataToBLE("Rotation: " + f);
    }

    public void OnIncrementButtonClick()
    {
        SetRotation(rotation + 1);
    }

    public void OnDecrementButtonClick()
    {
        SetRotation(rotation - 1);
    }

    public void SetRotation(float f)
    {
        f = controlPanel.CheckMin(minRotation, f);
        f = controlPanel.CheckMax(maxRotation, f);

        rotation = f;

        rotationInput.text = f.ToString();

        manageInput.Rotation=  f;

        singleton.AddGameEvent(LogEventType.Rotation, f.ToString());
    }
}
