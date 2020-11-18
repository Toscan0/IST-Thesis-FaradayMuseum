using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    #region Unity Variables

    public AchievementDatabase database;

    public AchievementNotificationController achievementNotificationController;

    public AchievementWindowController achievementWindowController;

    public GameObject achievemenItemPrefab;

    public Transform content;

    public AchievementButtonController achievementsButtonController;

    private int notificationCount;

    //explanation appears when achivements is unlocked
    public GameObject explanation;

    //give an hint if is take to long
    public HintManager hintManager;

    #endregion

    #region Const Variables

    private Color baseColor = new Color(0.0f, 0.62f, 0.63f, 1.0f);

    private Color evenColor = new Color(0.0f, 0.62f, 0.63f, 1.0f);

    private Color oddColor = new Color(0.0f, 0.82f, 0.83f, 1.0f);

    #endregion


    [SerializeField][HideInInspector]
    private List<AchievementItemController> achievementItems;
    
    private int achieventPointsTotal;

    private void Start()
    {
        LoadAchievementsTable();
        notificationCount = 0;
    }

    public void ShowNotification(Achievement achievement)
    {
        achievementNotificationController.ShowNotification(achievement);
    }

    public void ToggleAchievementWindow()
    {
        achievementWindowController.ToggleAchievementWindow();
        ResetNotification();
    }

    [ContextMenu("LoadAchievementsTable()")]
    private void LoadAchievementsTable(){

        foreach (AchievementItemController controller in achievementItems)
        {
            DestroyImmediate(controller.gameObject);
        }

        achievementItems.Clear();

        int i = 0;
        achieventPointsTotal = 0;
        int achievementUnlockedPoints = 0;
        foreach (Achievement achievement in database.achievements)
        {
            achieventPointsTotal += achievement.points;

            GameObject achievementPrefab = Instantiate(achievemenItemPrefab, content);

            Color color = baseColor;

            if (i%2 == 0)
            {
                color = evenColor;
            }
            else
            {
                color = oddColor;
            }

            achievementPrefab.GetComponent<Image>().color = color;

            AchievementItemController item = achievementPrefab.GetComponent<AchievementItemController>();

            item.achievement = achievement;
            item.unlocked = Convert.ToBoolean(PlayerPrefs.GetInt(achievement.id));

            item.RefreshView();
            achievementItems.Add(item);

            if (item.unlocked)
            {
                achievementUnlockedPoints += item.achievement.points;
            }

            achievementWindowController.AchieventPointsTotal = achieventPointsTotal;

            i++;
        }

        CalculateAchievementPoints(achievementUnlockedPoints);
    }

    public void UnlockAchievement(Achievements achievement){
    
        AchievementItemController item = achievementItems[(int)achievement];

        ShowNotification(item.achievement);
        PlayerPrefs.SetInt(item.achievement.id, 1);
        item.unlocked = true;
        item.RefreshView();

        notificationCount++;
        ActivateNotification();
        CalculateAchievementPoints(item.achievement.points);
    }

    public bool IncrementAchievement(Achievements achievement)
    {
        if(achievementItems.Count == 0){
            return false;
        }

        AchievementItemController item = achievementItems[(int)achievement];


        if (item.unlocked)
            return false;

        if (item.Increment())
        {
            UnlockAchievement(achievement);
            item.RefreshView();

            return true;
        }

        item.RefreshView();
        return false;
    }

    [ContextMenu("LockAllAchievements()")]
    public void LockAllAchievements()
    {
        foreach(Achievement achievement in database.achievements)
        {
            PlayerPrefs.DeleteKey(achievement.id);
        }

        foreach(AchievementItemController controller in achievementItems)
        {
            controller.unlocked = false;
            controller.RefreshView();
        }
        CalculateAchievementPoints(0);
    }

    public void CalculateAchievementPoints(int points)
    {
        achievementWindowController.UpdatePoints(points);
    }

    public void ActivateNotification()
    {
        achievementsButtonController.ActivateNotification(notificationCount);

    }

    public void ResetNotification()
    {
        achievementsButtonController.ResetNotification();
        notificationCount = 0;
    }

    public string GetAchievementID(Achievements achievement)
    {
        AchievementItemController item = achievementItems[(int)achievement];

        return item.GetID();
    }

    public string GetAchievementArtifactID(Achievements achievement)
    {
        AchievementItemController item = achievementItems[(int)achievement];

        return item.GetArtifactID();
    }

    public string[] GetAllAchievementsID()
    {
        string[] allAchievementsID = System.Enum.GetNames(typeof(Achievements));

        return allAchievementsID;
    }

    public List<string> GetUnlockedAchievementsID()
    {
        List<string> unlockedAchievementsID = new List<string>(); ;

        foreach (Achievements ach in Enum.GetValues(typeof(Achievements)))
        {
            AchievementItemController item = achievementItems[(int)ach];

            if (item.unlocked == true)
            {
                unlockedAchievementsID.Add(GetAchievementID(ach));
            }
        }

        return unlockedAchievementsID;
    }

    public List<string> GetLockedAchievementsID()
    {
        List<string> lockedAchievementsID = new List<string>(); ;

        foreach (Achievements ach in Enum.GetValues(typeof(Achievements)))
        {
            AchievementItemController item = achievementItems[(int)ach];

            if (item.unlocked == false)
            {
                lockedAchievementsID.Add(GetAchievementID(ach));
            }
        }

        return lockedAchievementsID;
    }

    public List<string> GetLockedAchievementsIDByArtifactID(string artifactID)
    {
        List<string> lockedAchievementsID = new List<string>(); ;

        foreach (Achievements ach in Enum.GetValues(typeof(Achievements)))
        {
            AchievementItemController item = achievementItems[(int)ach];

            if (item.unlocked == false && artifactID == item.GetArtifactID())
            {
                lockedAchievementsID.Add(GetAchievementID(ach));
            }
            //Debug.Log(artifactID + " ** " + item.GetArtifactID());
        }

        return lockedAchievementsID;
    }

    public void AchivemententUnlocked(Achievements achievement)
    {


        AchievementItemController item = achievementItems[(int)achievement];
        string id = item.GetID();      
        
        if(item.GetHaveExplanation() == true)
        {
            explanation.GetComponent<ExplanationDisplay>().SetID(id);
            explanation.SetActive(true);
        }
        
        hintManager.SetStartTimer(true);
    }
}
