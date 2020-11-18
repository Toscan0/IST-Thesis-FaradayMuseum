using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MouseOverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject tooltip;
    public GameObject Tooltip { get { return tooltip; } }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
