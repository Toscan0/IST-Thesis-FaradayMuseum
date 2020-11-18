using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButtonController : MouseOverButton
{
    public Text notificationText;

    public GameObject notificationImage;

    private bool firstTime = true;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }


    public void ActivateNotification(int notificationCount)
    {
        notificationText.text = notificationCount.ToString();

        PlayAnimation(true);
    }

    public void ResetNotification()
    {
        PlayAnimation(false);
        notificationText.text = "0";
    }

    public void PlayAnimation(bool isOpen)
    {
        if (m_animator != null)
        {
            m_animator.SetBool("isOpen", isOpen);

            if (firstTime)
            {
                m_animator.SetTrigger("FirstTime");
                firstTime = false;
            }
        }
    }
}
