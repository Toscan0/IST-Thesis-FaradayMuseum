using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAchivements : MonoBehaviour
{
    public AchievementManager achievementManager;

    // Start is called before the first frame update
    void Awake()
    {
        achievementManager.LockAllAchievements();
    }
}
