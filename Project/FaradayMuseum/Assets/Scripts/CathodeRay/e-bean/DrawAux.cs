using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawAux : MonoBehaviour
{
    #region PRIVATE_VARIABLES
    [SerializeField]
    [Tooltip("This color is not being used, it's only here for debug reasons")]
    private Color defaultColor;
    [SerializeField]
    private Color augementedColor;
    private Color augementedColor_Lock;

    private float ampuleRadius = 0.133f;

    //private int numberOfVertices = 400;

    private float lineWidthMultiplier = 0.2f;
    private float lineStartWidth = 0.02f;
    private float lineEndWidth = 0.02f;

    private LineRenderer lineRenderer;
    private new Renderer renderer;
    #endregion

    void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = false;

        lineRenderer.widthMultiplier = lineWidthMultiplier;
        lineRenderer.startWidth = lineStartWidth;
        lineRenderer.endWidth = lineEndWidth;

        renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_Radius", ampuleRadius);
        renderer.material.SetColor("_AugmentedColor", defaultColor);
        renderer.material.SetColor("_AugmentedColor", augementedColor);

        augementedColor_Lock = augementedColor;

        // Connect to ARButton event
        ShowARButton.OnARButtonClicked += UpdateColor;
    }

    public void DrawAuxCircle(float radius, int i, int numberOfVertices)
    {
        float x;
        float y;
        float z = 0;

        float angle = 0f;

        // not a loop, only draw the augmented part of the circle
        lineRenderer.positionCount = numberOfVertices - i + 2;
        lineRenderer.loop = false;

        /*
         *  x(t) = R * (1 - cos(w * t)) 
         *  y(t) = R * sen(w * t)
         *  z(t) = 0 
         */

        for (int e = 0; e < (numberOfVertices + 1); e++)
        {
            x = (float) (radius * (1 - Math.Cos((Math.PI / 180) * angle)));
            y = (float) -(radius * (Math.Sin((Math.PI / 180) * angle)));

            if (e >= i -1) //-1
            {
                lineRenderer.SetPosition(e - i +1, new Vector3(z, x, y));
            }
            
            angle += (360f / numberOfVertices);
        }
    }

    public void DisableAuxCircle()
    {
        // more eficient that disable or enable the componennt
        lineRenderer.positionCount = 0;
    }

    private void UpdateColor(bool showAR)
    {
        if (showAR)
        {
            //show the continus of the line
            augementedColor = augementedColor_Lock;
            renderer.material.SetColor("_AugmentedColor", augementedColor);
        }
        else
        {
            //Hide the line
            augementedColor.a = 0;
            renderer.material.SetColor("_AugmentedColor", augementedColor);
        }
    }

    //Always disable
    private void OnDisable()
    {
        ShowARButton.OnARButtonClicked -= UpdateColor;
    }
}



    
