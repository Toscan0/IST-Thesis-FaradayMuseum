using System;
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

    #region PUBLIC_FUNCTIONS

    #region GETS&&SETS

    public float Intensity
    {
        get { return intensity; }

        set
        {
            intensity = value;

            calculateShape.SetB(intensity);
            OnIntesityChanged?.Invoke(intensity);
        }
    }

    public float Tension
    {
        get { return tension; }
        set
        {
            tension = value;

            calculateShape.SetV0(tension);

            OnTensionChanged?.Invoke();
        }
    }

    public float Rotation
    {
        get { return rotation; }
        set
        {
            rotation = value;

            Ampule.localEulerAngles = new Vector3(rotation, 0, 0);

            calculateShape.SetAlpha(rotation);
        }
    }

    #endregion

    void Awake()
    {
        calculateShape = gameObject.GetComponent<CalculateShape>();
    }

    #endregion

   
}
