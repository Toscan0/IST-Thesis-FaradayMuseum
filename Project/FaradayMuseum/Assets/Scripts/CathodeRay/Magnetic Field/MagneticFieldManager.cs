using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldManager : MonoBehaviour
{
    [SerializeField]
    private MyMagnetTexture[] magnetTextures;

    private float minI = 0.1f;
    private float maxI = 3;
    private float minSpeed = 0.5f;
    private float maxSpeed = 2f;

    private float intensity = 0f;
    private bool showAR = true;

    private void Awake()
    {
        ManageInput.OnIntesityChanged += UpdateIntensity;
        ShowARButton.OnARButtonClicked += UpdateARToShow;

        gameObject.SetActive(false);
    }

    private void UpdateIntensity(float i)
    {
        intensity = i;

        UpdateMagneticField();
    }

    private void UpdateARToShow(bool showOrNot)
    {
        showAR = showOrNot;

        UpdateMagneticField();
    }

    private void UpdateMagneticField()
    {
        if (showAR)
        {
            // no eletrecity == no magnetic field
            if (intensity == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                updateSpeed(intensity);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void updateSpeed(float speed)
    {
        foreach(var magnetTexture in magnetTextures)
        {
            magnetTexture.ScrollSpeed = NumberConvert(speed);
        }
    }

    private float NumberConvert(float i)
    {
        float speed = (((i - minI) * (maxSpeed - minSpeed)) / (maxI - minI)) + minSpeed;

        return speed;
    }

    private void OnDestroy()
    {
        ManageInput.OnIntesityChanged -= UpdateIntensity;
        ShowARButton.OnARButtonClicked -= UpdateARToShow;

    }
}
