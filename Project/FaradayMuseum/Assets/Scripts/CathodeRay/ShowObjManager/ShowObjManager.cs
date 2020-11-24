using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objsToShow;

    private void OnEnable()
    {
        ShowObjsButton.OnShowObjsButtonClicked += ShowOrHide;
    }

    private void ShowOrHide(bool showOrHide)
    {
        for (int i = 0; i < objsToShow.Length; i++)
        {
            objsToShow[i].SetActive(showOrHide);
        }
    }

    private void OnDisable()
    {
        ShowObjsButton.OnShowObjsButtonClicked -= ShowOrHide;
    }
}
