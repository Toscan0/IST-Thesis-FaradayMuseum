using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeIn;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

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
