using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItemController : MonoBehaviour
{
    [SerializeField] Image unlockedIcon;
    [SerializeField] Image lockedIcon;

    [SerializeField] Text titleLabel;
    [SerializeField] Text descriptionLabel;
    [SerializeField] Text pointsLabel;

    public bool unlocked;
    public Achievement achievement;

    public int current;

    public void RefreshView()
    {
        titleLabel.text = achievement.title;
        descriptionLabel.text = achievement.description;
        pointsLabel.text = achievement.points.ToString();


        unlockedIcon.enabled = unlocked;
        lockedIcon.enabled = !unlocked;
    }

    public bool Increment()
    {
        current++;

        if (current >= achievement.objective)
        {
            unlocked = true;
            return true;
        }
        else
        {
            unlocked = false;
            return false;

        }
    }

    public string GetID()
    {
        return achievement.id;
    }

    public string GetArtifactID()
    {
        return (achievement.targetID).ToString();
    }

    public bool GetHaveExplanation()
    {
        return achievement.haveExplanation;
    }

    private void OnValidate()
    {
        RefreshView();
    }
}
