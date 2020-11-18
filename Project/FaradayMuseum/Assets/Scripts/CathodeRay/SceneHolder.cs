using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHolder : MonoBehaviour
{
    public GameObject gameEnd;

    [SerializeField]
    private AchievementManager achievementManager;

    private List<string> achivementsIDLocked;

    /*
    * Currently scene holder:  Create intensity (CR0) -> 
    * Line (CR1) -> Circle1 (CR2) -> Circle2 (CR3) ->
    * Spiral1 (CR4) -> Spiral2 (CR5) -> Final Pop-up
    */
    private bool intensityDone = false;
    private bool lineDone = false;
    private bool circle1Done = false;
    private bool circle2Done = false;
    private bool spiral1Done = false;
    private bool spiral2Done = false;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    private void Awake()
    {
        ManageInput.OnIntesityChanged += IntesityChanged;
        CalculateShape.OnAchivementCompleted += UpdateSceneHolder;
    }


    private void UpdateSceneHolder(Achievements achievement)
    {
        if (achievement.ToString() == "CR1")
        {
            // check if previous achivement (intensity) is completed
            if (intensityDone)
            {
                AchivementDone(achievement);
                lineDone = true;
            }
        }

        if (achievement.ToString() == "CR2")
        {
            // check if previous achivement (line) is completed
            if (lineDone)
            {
                AchivementDone(achievement);
                circle1Done = true;
            }
        }

        if(achievement.ToString() == "CR3")
        {
            // check if previous achivement (cricle1Done) is completed
            if (circle1Done)
            {
                AchivementDone(achievement);
                circle2Done = true;
            }
        }

        if (achievement.ToString() == "CR4")
        {
            // check if previous achivement (cricle2Done) is completed
            if (circle2Done)
            {
                AchivementDone(achievement);
                spiral1Done = true;
            }
        }

        if (achievement.ToString() == "CR5")
        {
            // check if previous achivement (spiral1Done) is completed
            if (spiral1Done)
            {
                AchivementDone(achievement);
                spiral2Done = true;


                gameEnd.SetActive(true);
                singleton.AddGameEvent(LogEventType.NoActionClick, "GameEnd");
            }
        }

    }

    private void IntesityChanged(float intensity)
    {
        if (intensityDone == false && intensity != 0)
        {
            AchivementDone(Achievements.CR0);
            intensityDone = true;
        }
    }

    private void AchivementDone(Achievements achivement)
    {
        bool achivementUnlocked = achievementManager.IncrementAchievement(achivement);

        if (achivementUnlocked == true)
        {
            achievementManager.AchivemententUnlocked(achivement);
            singleton.AddGameEvent(LogEventType.AchivementUnlocked, achievementManager.GetAchievementID(achivement));
        }
    }

    private void OnDestroy()
    {
        ManageInput.OnIntesityChanged -= IntesityChanged;
        CalculateShape.OnAchivementCompleted -= UpdateSceneHolder;
    }
}
