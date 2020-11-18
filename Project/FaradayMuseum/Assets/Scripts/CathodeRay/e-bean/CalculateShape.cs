using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draw))]
public class CalculateShape : PhysicsConsts
{
    [SerializeField]
    private AchievementManager achievementManager;

    private Draw draw;
    
    private int nPoints = 1000; //number of points to design the circle

    private double B; // magnetic field
    private double V0; // initial velocity
    private double alpha; // rotation angle in rads

    public static Action<Achievements> OnAchivementCompleted;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    //To Spiral Achivements
    private float lastIntensity = 0;
    private float lastRotation = 0;
    private float lastTension = 0;
    private bool s1 = false;
    private bool s2 = false;

    void Awake()
    {
        draw = gameObject.GetComponent<Draw>();
    }


    public void SetB(float intesity)
    {
        // B = U0 * H
        B = U0 * H(intesity);

        ShapeCalculator();

        CheckSpiralAchivement(-1, -1, intesity);
    }

    public void SetV0(float tension)
    {
        // sqrt(2 * q /m * Ua) , Ua -> acceleration voltage
        V0 = Math.Sqrt(2 * tension * (q / m));

        ShapeCalculator();

        CheckSpiralAchivement(-1, tension, -1);
    }

    public void SetAlpha(float rotation)
    {
        alpha = (Math.PI / 180) * rotation;

        ShapeCalculator();

        CheckSpiralAchivement(rotation, -1, -1);
    }



    private void ShapeCalculator()
    {
       
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
           
            AchivementDone(Achievements.CR1);
        }
        // alpha = PI, is a line 
        else if (alpha == Math.PI)
        {
            draw.NumberOfVertices = 2;

            draw.DrawLine(alpha);

            AchivementDone(Achievements.CR1);
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

            if (r > 0.0586f)
            {
                AchivementDone(Achievements.CR3);
            }
            else
            {
                AchivementDone(Achievements.CR2);

            }
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

            if (s1)
            {
                AchivementDone(Achievements.CR4);
            }
            else if(s2)
            {
                AchivementDone(Achievements.CR5);
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



    private void AchivementDone(Achievements achivement)
    {
        OnAchivementCompleted?.Invoke(achivement);

        /*
        * If u want to unlock the achivement right here, descomment the next code
        * Unlocks achivement, activates explanation, and hint logic
        */
        
        /*bool achivementUnlocked = achievementManager.IncrementAchievement(achivement);

        if (achivementUnlocked == true)
        {
            achievementManager.AchivemententUnlocked(achivement);
            singleton.AddGameEvent(LogEventType.AchivementUnlocked, achievementManager.GetAchievementID(achivement));
        }*/
    }

    //To Spiral Achivements
    private void CheckSpiralAchivement(float r, float t, float i)
    {
        if(r != -1)
        {
            lastRotation = r;
        }
        if(t != -1)
        {
            lastTension = t;
        }
        if(i != -1)
        {
            lastIntensity = i;
        }

        if (lastRotation == 110 && lastTension == 150 && lastIntensity == 0.4f)
        {
            s1 = true;
            s2 = false;
        }
        else if(lastRotation == 94 && lastTension == 100 && lastIntensity == 0.7f)
        {
            s1 = false;
            s2 = true;
        }
        else
        {
            s1 = false;
            s2 = false;
        }
        
    }
}
