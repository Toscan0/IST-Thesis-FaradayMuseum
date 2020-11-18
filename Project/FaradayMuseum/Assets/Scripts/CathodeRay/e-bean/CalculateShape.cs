using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draw))]
[RequireComponent(typeof(CheckAchievements))]
public class CalculateShape : PhysicsConsts
{
    private Draw draw;
    private CheckAchievements checkAchievements;
    
    private int nPoints = 1000; //number of points to design the circle

    private double B; // magnetic field
    private double V0; // initial velocity
    private double alpha; // rotation angle in rads

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();


    void Awake()
    {
        draw = gameObject.GetComponent<Draw>();
        checkAchievements = gameObject.GetComponent<CheckAchievements>();
    }


    public void SetB(float intesity)
    {
        // B = U0 * H
        B = U0 * H(intesity);

        checkAchievements.CheckSpiralAchivement(-1, -1, intesity);

        ShapeCalculator();
    }

    public void SetV0(float tension)
    {
        // sqrt(2 * q /m * Ua) , Ua -> acceleration voltage
        V0 = Math.Sqrt(2 * tension * (q / m));

        checkAchievements.CheckSpiralAchivement(-1, tension, -1);

        ShapeCalculator();
    }

    public void SetAlpha(float rotation)
    {
        alpha = (Math.PI / 180) * rotation;

        checkAchievements.CheckSpiralAchivement(rotation, -1, -1);

        ShapeCalculator();
    }



    private void ShapeCalculator()
    {
        /** /
        *Debug.Log("--- Draw ---");
        Debug.Log("B: " +  B);
        Debug.Log("V0: " + V0);
        Debug.Log("Aplha: " + alpha);
        /**/

        
        if(B == 0 || V0 == 0)
        {
            /*
            * TODO: Fix this -ask to professor carlos
            */
            draw.NumberOfVertices = 2;
            draw.DrawDefault();
            return;
        }

        // alpha = 0 or alpha = 2PI, is a line 
        if (alpha == 0 || alpha == (2 * Math.PI))
        {
            draw.NumberOfVertices = 2;

            draw.DrawLine(alpha);
           
            checkAchievements.AchivementDone(Achievements.CR1);
        }
        // alpha = PI, is a line 
        else if (alpha == Math.PI)
        {
            draw.NumberOfVertices = 2;

            draw.DrawLine(alpha);

            checkAchievements.AchivementDone(Achievements.CR1);
        }
        // alpha = PI/2, is a circle
        else if (alpha == (Math.PI / 2))
        {
            double r = R(V0, B);
            double w = W(V0, r);
            double timePeriod = T(B);
            double D = Delta(B);

            draw.NumberOfVertices = nPoints;
            draw.Radius = (float) r;

            draw.DrawCircle();

            checkAchievements.CheckCircumferenceAchivement(r);
        }
        // alpha != 0,PI/2,Pi -> is a spiral
        else
        {
            double r = R(V0, B);
            double w = W(V0, r);
            double timePeriod = T(B);
            double D = Delta(B);

            draw.NumberOfVertices = 360;
            draw.Radius = (float) r;

            draw.DrawSpiral(alpha, V0, timePeriod, w);

            if (checkAchievements.Spiral1)
            {
                checkAchievements.AchivementDone(Achievements.CR4);
            }
            else if(checkAchievements.Spiral2)
            {
                checkAchievements.AchivementDone(Achievements.CR5);
            }
        }
    }


    private double R(double V0, double B)
    {
        // R = V0 / W0 = (m * V0) / (q * B) = (sqrt(2 * q / m * Ua) / q * (B / m))
        return ((m * V0) / (q * B));
    }

    private double W(double V0, double R)
    {
        // w = (q * B) / m = v0 / R
        return V0 / R;
    }

    private double T(double B)
    {
        // T = (2 * Pi / w) = (2 * Pi * m) / (q * B) = (2 * Pi * R / v0)
        return (2 * Math.PI * m) / (q * B);
    }

    private double Delta(double B)
    {
        // delta = (2 * Pi * m) / (q * B * N), where N is the number os points to design the circunference 
        return (2 * Math.PI * m) / (q * B * nPoints);
    }

    private double H(float intensity)
    {
        // H = ((4/5)^1.5 ) * ((n*I)/r) 0.7155417528
        double k = Math.Pow(0.8, 1.5); // 4/5 = 0.8

        return k * ((numberOfTurns * intensity) / coilRay);
    }

    private Vector3 VectorB()
    {
        // In our case the B is ALWAYS parallel to zz axis. Bvectorial = Bûz
        return new Vector3(0, 0, (float)B);
    }

    private Vector3 CirclePoint(float t, double R, double W)
    {
        float x = (float)(R * (1 - Math.Cos(W * t)));
        float y = (float)(R * (Math.Sin(W * t)));
        float z = 0;

        return new Vector3(x, y, z);
    }



    
}
