using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUps : MonoBehaviour
{
    public bool SwipeCenterToRight { get; set; } = false;
    public bool SwipeRightToCenter { get; set; } = false;
    public bool IsActive { get; set; } = false;

    protected bool firstTime = true;

    //Events
    public static Action<string> OnPopUpEnable;
    public static Action OnPopUpDisable;


    public IEnumerator ClosePopUp(Animator animator, GameObject go)
    {
        IsActive = false;

        animator.SetTrigger("Close");

        yield return new WaitForSeconds(0.40f);

        gameObject.SetActive(false);       
    }
}
