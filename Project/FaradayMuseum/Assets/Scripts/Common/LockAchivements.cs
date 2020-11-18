using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAchivements : MonoBehaviour
{
    public AchievementManager achievementManager;

    void Awake()
    {
        achievementManager.LockAllAchievements();
    }
}
