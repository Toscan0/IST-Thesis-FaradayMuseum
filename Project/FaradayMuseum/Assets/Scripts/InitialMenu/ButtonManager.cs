using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject consent;

    [SerializeField]
    private GameObject fadeIn;
    [SerializeField]
    private GameObject warningContainer;
    [SerializeField]
    private GameObject instructionsContainer;
    [SerializeField]
    private LoadSceneSingleton loadSceneSingleton;

    private string URL = "https://www.google.pt/";
    private string formURL = "https://docs.google.com/forms/d/e/1FAIpQLSc4h0fdAyvRWEZKR5RoG6ADygwMzRhrzXLgCboB_nNNXeuNJA/viewform?entry.1791559743=";

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    public void ConsentButton()
    {
        consent.SetActive(false);
        warningContainer.SetActive(true);

        singleton.AddGameEvent(LogEventType.Click, "consent Button");
    }

    public void NextButton()
    {
        warningContainer.SetActive(false);
        instructionsContainer.SetActive(true);

        singleton.AddGameEvent(LogEventType.Click, "Next Button");
    }

    public void PlayButton()
    {
        StartCoroutine(LoadNextScene());

        singleton.AddGameEvent(LogEventType.Click, "Play Button");
    }

    private IEnumerator LoadNextScene()
    {
        fadeIn.SetActive(true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
