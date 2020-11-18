using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class LoadSceneSingleton : MonoBehaviour
{
    public static LoadSceneSingleton instance;

    //UserTest
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    [SerializeField]
    [Tooltip("Record logs from user interaction")]
    private bool LOG = true;
   
    private static bool fileNameDefined = false;
    private static string myFileName = "default";
    public string MyFileName { get { return myFileName; } }
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = true;
        
        // UserTest
        if (LOG)
        {
            if (!fileNameDefined)
            {
                myFileName = singleton.FileName();
                fileNameDefined = true;
            }
            singleton.LogFileName = myFileName;
            singleton.AddGameEvent(LogEventType.OnAppLoad, "WebGL CathodeRay");

            StartCoroutine(singleton.PostRequest());
        }
#endif

        DontDestroyOnLoad(this.gameObject);
    }
   
}

