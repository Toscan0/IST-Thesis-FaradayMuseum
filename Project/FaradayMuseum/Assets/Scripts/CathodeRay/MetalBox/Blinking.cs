using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    [SerializeField]
    private int blinkTimes = 3;
    [SerializeField]
    private float blinkTimeOn = 0.1f;
    [SerializeField]
    private float blinkTimeOff = 0.1f;

    private bool firstTime = true;
    private new Renderer renderer;
     
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        ManageInput.OnTensionChanged += TensionChanged;
    }

    private void TensionChanged()
    {
        if (!firstTime)
        {
            StartCoroutine(Blink(blinkTimes, blinkTimeOn, blinkTimeOff));
        }
        else
        {
            firstTime = false;
        }
        
    }


    private IEnumerator Blink(int nTimes, float timeOn, float timeOff)
    {
        while (nTimes > 0)
        {
            renderer.enabled = true;
            yield return new WaitForSeconds(timeOn);

            renderer.enabled = false;
            yield return new WaitForSeconds(timeOff);

            nTimes--;
        }

        renderer.enabled = true;
    }

    private void OnDisable()
    {
        ManageInput.OnTensionChanged -= TensionChanged;
    }
}
