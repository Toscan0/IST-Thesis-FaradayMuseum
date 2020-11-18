using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField]
    private GameObject hint;
    [SerializeField]
    private AchievementManager achievementManager;
   
    [SerializeField] private float timer = 60;
    private float timerLock; // used to reset time

    private bool hintShowed = false; // if a hint was showed or not
    private bool startTimer = false; // if the timer should start to count

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    void Start()
    {
        timerLock = timer;
    }

    void Update()
    {
        HintLogic();
    }

    public void HintLogic()
    {
        if (timer >= 0 && startTimer == true)
        {
            timer -= Time.deltaTime;
        }
        //if timer < 0 , hint appears
        else if (timer < 0 && hintShowed == false)
        {
            // Select an locked achivement to show the hint
            List<string> lockedAchievementsIDByArtifactID =
                achievementManager.GetLockedAchievementsIDByArtifactID("CR");
            string[] aux = lockedAchievementsIDByArtifactID.ToArray();

            if (aux.Length > 0)
            {
                // for now we are selecting the first one
                // maybe we should change this later!!
                string id = GetAchivementToHelp(aux);
                // show hint
                hint.GetComponent<HintDisplay>().SetID(id);
                hint.SetActive(true);
                singleton.AddGameEvent(LogEventType.NoActionClick, "Hint showed: " + id);
                // dont show hint anymore
                hintShowed = true;
                // reset timer to next hint
                ResetTimer();
                startTimer = false;
            }
            else
            {
                //all achievements are completed
                ResetTimer();
                startTimer = false;
            }
        }
        else
        {
            hintShowed = false;
        }
    }

    public string GetAchivementToHelp(string[] achivementdIDs)
    {
        return achivementdIDs[0];
    }

    public void SetTimer(float t)
    {
        timer = t;
        timerLock = t;
    }

    public void ResetTimer()
    {
        timer = timerLock;
    }

    public void SetStartTimer(bool b)
    {
        startTimer = b;

        if (startTimer)
        {
            ResetTimer();
        }
    }
}
