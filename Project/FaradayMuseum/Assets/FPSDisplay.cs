using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    float avgFPS = 0.0f;
    float timeForAvgFPS = 0.0f;
    int framesCounted = 0;
    bool enterIF = true;
    public float timeToCalculateAvgFPS;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        timeForAvgFPS += Time.deltaTime;
        framesCounted += 1;
        if (enterIF)
        {
            avgFPS += 1.0f / deltaTime;
        }
        
        ////Debug.Log(avgFPS);
        if (timeForAvgFPS >= timeToCalculateAvgFPS && enterIF)
        {
            ////Debug.Log("entrei aqui no //Debug.Log do FPSDISPLAY");
            enterIF = false;
            avgFPS = avgFPS / framesCounted;
            
        }
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        Rect rect1 = new Rect(0, 40, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps) {2} avgFPS", msec, fps,avgFPS);
        string text1 = string.Format("{0} avgFPS", avgFPS);
        GUI.Label(rect, text, style);
        //GUI.Label(rect1, text1, style);
    }
}
