﻿using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CalculateShape))]
public class ManageInput : PhysicsConsts
{
    [SerializeField]
    private Transform Ampule;

    public static Action<float> OnIntesityChanged;
    public static Action OnTensionChanged;

    #region PRIVATE_VARIABLES

    private float intensity;  //Intensity of the eletrical current on the coils
    private float rotation; //angle of rotation of the ampoule in degrees
    private float tension; // tension of the eletrical current in the cathode ray

    private CalculateShape calculateShape;

    #endregion


    void Awake()
    {
        calculateShape = gameObject.GetComponent<CalculateShape>();
    }

    private void OnEnable()
    {
        IntensityUI.OnIntensityChanged += UpdateIntensity;
        TensionUI.OnTensionChanged += UpdateTension;
        RotationUI.OnRotationChanged += UpdateRotation;
    }

    private void UpdateIntensity(float newValue)
    {
        intensity = newValue;

        calculateShape.SetB(intensity);
        OnIntesityChanged?.Invoke(intensity);
    }

    private void UpdateTension(float newValue)
    {
        tension = newValue;

        calculateShape.SetV0(tension);

        OnTensionChanged?.Invoke();
    }

    private void UpdateRotation(float newValue)
    {
        rotation = newValue;

        Ampule.localEulerAngles = new Vector3(rotation, 0, 0);

        calculateShape.SetAlpha(rotation);
    }

    private void OnDisable()
    {
        IntensityUI.OnIntensityChanged -= UpdateIntensity;
        TensionUI.OnTensionChanged -= UpdateTension;
        RotationUI.OnRotationChanged -= UpdateRotation;
    }
}