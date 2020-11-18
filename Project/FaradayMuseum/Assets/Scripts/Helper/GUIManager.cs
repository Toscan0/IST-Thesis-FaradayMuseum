using System.Collections.Generic;

public static class GUIManager
{
    private static Dictionary<string, GUIControl> m_Controls = new Dictionary<string, GUIControl>();

    public static void Register(GUIControl pControl)
    {
        if (pControl != null && !m_Controls.ContainsKey(pControl.Name))
        {
            m_Controls.Add(pControl.Name, pControl);
        }
    }

    public static void Unregister(GUIControl pControl)
    {
        if (pControl != null)
        {
            m_Controls.Remove(pControl.Name);
        }
    }

    public static void Show(string pControlName)
    {
        GUIControl result = null;
        if (m_Controls.TryGetValue(pControlName, out result) && !result.gameObject.activeSelf)
        {
            result.OnShow();
        }
    }

    public static void Show(GUIControl pControl)
    {
        if (!pControl.gameObject.activeSelf)
        {
            pControl.OnShow();
        }
    }

    public static void ShowAndHide(string pControlName, string pToHideName)
    {
        if (pControlName == pToHideName)
        {
            return;
        }
        GUIControl resultToShow = null;
        if (m_Controls.TryGetValue(pControlName, out resultToShow))
        {
            GUIControl resultToHide = null;
            if (!resultToShow.gameObject.activeSelf) resultToShow.OnShow();
            if (m_Controls.TryGetValue(pToHideName, out resultToHide) && resultToHide.gameObject.activeSelf) resultToHide.OnHide();
        }
    }

    public static void ShowAndHide(GUIControl pControl, GUIControl pToHide)
    {
        if (pControl.Name == pToHide.Name)
        {
            return;
        }
        if (!pControl.gameObject.activeSelf) pControl.OnShow();
        if (pToHide.gameObject.activeSelf) pToHide.OnHide();
    }

    public static void ShowAndHide(string pControlName, GUIControl pToHide)
    {
        if (pControlName == pToHide.Name)
        {
            return;
        }
        GUIControl result = null;
        if (m_Controls.TryGetValue(pControlName, out result))
        {
            if (!result.gameObject.activeSelf) result.OnShow();
            if (pToHide.gameObject.activeSelf) pToHide.OnHide();
        }
    }

    public static void Hide(string pControlName)
    {
        GUIControl result = null;
        if (m_Controls.TryGetValue(pControlName, out result) && result.gameObject.activeSelf)
        {
            result.OnHide();
        }
    }

    public static void Hide(GUIControl pControl)
    {
        if (pControl.gameObject.activeSelf)
        {
            pControl.OnHide();
        }
    }

    public static void HideAllUI(){
        foreach(KeyValuePair<string, GUIControl> entry in m_Controls)
        {
          entry.Value.OnHide();
        }
    }

    public static void ShowAllUI(){
        foreach(KeyValuePair<string, GUIControl> entry in m_Controls)
        {
          entry.Value.OnShow();
        }
    }
}