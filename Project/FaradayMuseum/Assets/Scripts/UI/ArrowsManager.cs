using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject leftArrow;
    [SerializeField]
    private GameObject rightArrow;

    private void Awake()
    {
        PopUps.OnPopUpDisable += HideArrows;
    }

    public void ShowArrows()
    {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
    }

    public void SetLeftArrowInteractable(bool isInteractable)
    {
        leftArrow.GetComponent<Button>().interactable = isInteractable;
    }

    public void SetRightArrowInteractable(bool isInteractable)
    {
        rightArrow.GetComponent<Button>().interactable = isInteractable;
    }

    public void HideArrows()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }

    private void OnDestroy()
    {
        PopUps.OnPopUpDisable -= HideArrows;
    }
}
