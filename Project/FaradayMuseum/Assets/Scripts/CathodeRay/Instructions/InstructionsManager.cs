using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject instructions;

    private void OnEnable()
    {
        InstructionsButton.OnInstructionsButtonClicked += ShowOrHide;
    }

    private void ShowOrHide(bool showOrHide)
    {
        instructions.SetActive(showOrHide);
    }

    private void OnDisable()
    {
        InstructionsButton.OnInstructionsButtonClicked -= ShowOrHide;
    }
}
