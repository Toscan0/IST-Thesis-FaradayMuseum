using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementWindowController : MonoBehaviour
{
    public ScrollRect myScrollRect;

    public Text myPoints;

    private Animator m_animator;

    private int currentPoints = 0;

    private bool isWindowOpen = false;

    public int AchieventPointsTotal { get; set; }

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void ToggleAchievementWindow()
    {
        if (!isWindowOpen)
        {
            gameObject.SetActive(true);
        }


        if (m_animator != null)
        {
            bool isOpen = m_animator.GetBool("isOpen");

            m_animator.SetBool("isOpen", !isOpen);

            singleton.AddGameEvent(LogEventType.Click, "Achivement window: " + !isOpen);
        }
    }

    public void SetWindow(bool isOpen)
    {
        isWindowOpen = isOpen;
    }

    public void ResetScroll()
    {
        myScrollRect.verticalNormalizedPosition = 1.0f;
    }

    public void UpdatePoints(int points)
    {
        currentPoints += points;
        myPoints.text = currentPoints.ToString() + " / " + AchieventPointsTotal;
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}