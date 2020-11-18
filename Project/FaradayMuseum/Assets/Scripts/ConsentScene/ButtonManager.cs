using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class ButtonManager : MonoBehaviour
{ 
    [SerializeField]
    private GameObject fadeIn;
    [SerializeField]
    private GameObject consetContainer;
    [SerializeField]
    private GameObject instructionsContainer;
    [SerializeField]
    private GameObject fullscreenContainer;
    [SerializeField]
    private LoadSceneSingleton loadSceneSingleton;

    private string URL = "https://www.google.pt/";
    private string formURL = "https://docs.google.com/forms/d/e/1FAIpQLSc4h0fdAyvRWEZKR5RoG6ADygwMzRhrzXLgCboB_nNNXeuNJA/viewform?entry.1791559743=";

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void YESButton()
    {
        consetContainer.SetActive(false);
        fullscreenContainer.SetActive(true);

        singleton.AddGameEvent(LogEventType.Click, "Warning Button");
    }

    public void NoButton()
    {
        Application.OpenURL(URL);

        singleton.AddGameEvent(LogEventType.Click, "No Button");
    }

    public void NextButton()
    {
        fullscreenContainer.SetActive(false);
        instructionsContainer.SetActive(true);

        singleton.AddGameEvent(LogEventType.Click, "FullScreen Button");
    }

    public void PlayButton()
    {
        StartCoroutine(LoadNextScene());

        singleton.AddGameEvent(LogEventType.Click, "Play Button");
    }

    public void ConsetFormButton()
    {
        consetContainer.SetActive(false);
        instructionsContainer.SetActive(true);

        formURL += loadSceneSingleton.MyFileName;
        openIt(formURL);
    }

    private IEnumerator LoadNextScene()
    {
        fadeIn.SetActive(true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    public void openIt(string url)
    {
    #if !UNITY_EDITOR && UNITY_WEBGL
                 OpenNewTab(url);
    #endif
    }
}
