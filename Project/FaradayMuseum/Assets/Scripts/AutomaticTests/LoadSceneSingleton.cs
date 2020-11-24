using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class LoadSceneSingleton : MonoBehaviour
{
    public static LoadSceneSingleton instance;

    [SerializeField]
    private bool log = true;

    private string levelName;
    private GameManager gameManager = GameManager.Instance;

    //UserTest
    private static string fileName = "DefaultFileName";
    private static bool fileNameDefined = false;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // UserTest
        singleton.Log = log;
        if (log)
        {
            if (!fileNameDefined)
            {
                fileName = singleton.FileName();
                fileNameDefined = true;
            }
            singleton.LogFileName = fileName;
            singleton.AddGameEvent(LogEventType.OnAppLoad, "Android");

            StartCoroutine(singleton.SendToSigma());
        }

        //unity permissions do not work
        UniAndroidPermission.RequestPermission(AndroidPermission.CALL_PHONE); 
        UniAndroidPermission.RequestPermission(AndroidPermission.READ_PHONE_STATE);
        UniAndroidPermission.RequestPermission(AndroidPermission.WRITE_EXTERNAL_STORAGE);
        UniAndroidPermission.RequestPermission(AndroidPermission.READ_EXTERNAL_STORAGE);

        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string name)
    {
        levelName = name;
    } 
}

