using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class LoadSceneSingleton : MonoBehaviour
{

    private string levelName;
    AsyncOperation async;
    //public GameObject loadingSceneManager;
    public static LoadSceneSingleton instance;
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    private string lastScene;

    private bool LOG = false;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //Debug.Log("o loadingSceneSingleton acordou");

        singleton.AddGameEvent(LogEventType.OnAppLoad);

        if (LOG)
        {
            StartCoroutine("SendToSigma");
        }

        UniAndroidPermission.RequestPermission(AndroidPermission.CALL_PHONE); //unity permissions do not work
        UniAndroidPermission.RequestPermission(AndroidPermission.READ_PHONE_STATE);
        UniAndroidPermission.RequestPermission(AndroidPermission.WRITE_EXTERNAL_STORAGE);
        UniAndroidPermission.RequestPermission(AndroidPermission.READ_EXTERNAL_STORAGE);

    }

    IEnumerator SendToSigma()
    {
        while (true)
        {
            singleton.SendtoSigma();
            yield return new WaitForSeconds(10.0f);
        }
    }

    IEnumerator SaveToMobile()
    {
        while (true)
        {
            singleton.SaveOnMobile();
            yield return new WaitForSeconds(30.0f);
        }
    }

    public void StartLoading(string name)
    {
        levelName = name;
        StartCoroutine("Load");
    }

    IEnumerator Load()
    {
        gameObject.GetComponent<LoadingSceneManager>().changedScene = true;
        gameObject.GetComponent<LoadingSceneManager>().loadingMenu.SetActive(true);

        gameObject.GetComponent<Animator>().SetBool("canFade", true);
        yield return new WaitForSeconds(1f);
        async = SceneManager.LoadSceneAsync(levelName);
        ////Debug.Log(levelName);
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }

    public float LoadProgress()
    {
        return async.progress;
    }

    public bool CheckIfSceneReady()
    {
        return async.allowSceneActivation;
    }
}

