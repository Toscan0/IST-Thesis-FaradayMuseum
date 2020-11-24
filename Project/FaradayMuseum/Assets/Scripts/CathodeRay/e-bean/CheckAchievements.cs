using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAchievements : MonoBehaviour
{
    [SerializeField]
    private AchievementManager achievementManager;
    public static Action<Achievements> OnAchivementCompleted;

    //Circumference Achievements
    private bool circumFirstTime = true;
    private bool circumBig = false;
    private bool circumSmall = false;

    //Spiral Achivements
    private float lastIntensity = 0;
    private float lastRotation = 0;
    private float lastTension = 0;
    public bool Spiral1 { get; private set; } = false;
    public bool Spiral2 { get; private set; } = false;

    public void CheckCircumferenceAchivement(double radius)
    {
        if (circumFirstTime)
        {
            AchivementDone(Achievements.CR2);
            circumFirstTime = false;

            //Image target -> 0.0638976494092061f ; Model target -> 0.0645f
            if (radius > 0.0645f)
            {
                
                circumBig = true;
                circumSmall = false;
            }
            else
            {
                
                circumBig = false;
                circumSmall = true;
            }
            
        }
        else
        {
            if(circumBig == true && circumSmall == false && (radius <= 0.0645f))
            {
                AchivementDone(Achievements.CR3);
            }
            else if (circumBig == false && circumSmall == true && (radius > 0.0645f))
            {
                AchivementDone(Achievements.CR3);
            }
        }
    }

    public void CheckSpiralAchivement(float r, float t, float i)
    {
        if (r != -1)
        {
            lastRotation = r;
        }
        if (t != -1)
        {
            lastTension = t;
        }
        if (i != -1)
        {
            lastIntensity = i;
        }

        if (lastRotation == 110 && lastTension == 150 && lastIntensity == 0.4f)
        {
            Spiral1 = true;
            Spiral2 = false;
        }
        else if (lastRotation == 94 && lastTension == 100 && lastIntensity == 0.7f)
        {
            Spiral1 = false;
            Spiral2 = true;
        }
        else
        {
            Spiral1 = false;
            Spiral2 = false;
        }

    }    

    public void AchivementDone(Achievements achivement)
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
}
