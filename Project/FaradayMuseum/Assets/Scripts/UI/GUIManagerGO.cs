using UnityEngine;

public class GUIManagerGO : MonoBehaviour
{    
    public void Show(GameObject gameObject)
    {
        GUIManager.Show(gameObject.GetComponent<GUIControl>());
    }
     public void Hide(GameObject gameObject)
    {
        GUIManager.Hide(gameObject.GetComponent<GUIControl>());
    }
}
