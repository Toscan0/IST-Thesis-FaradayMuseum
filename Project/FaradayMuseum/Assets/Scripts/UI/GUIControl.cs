using UnityEngine;

public class GUIControl : MonoBehaviour
{
    public virtual string Name => gameObject.name;

    void Awake()
    {
        GUIManager.Register(this);
    }

    void OnDestroy()
    {
        GUIManager.Unregister(this);
    }

    public virtual void OnShow()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnHide()
    {
        gameObject.SetActive(false);
    }
}