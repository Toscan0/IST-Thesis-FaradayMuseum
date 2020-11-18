using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour {

    public GameObject loadingMenu;
    public bool changedScene;
    

   

   // LoadSceneSingleton scene;
    /*
    public LoadSceneSingleton Scene
    {
        get
        {
            return scene;
        }
    }
    */
    void Awake()
    {
        //Debug.Log("o loadingSceneManager acordou");
        DontDestroyOnLoad(this);


        //scene = LoadSceneSingleton.instance;

        

        loadingMenu.SetActive(false);
        

    }
    private void Update()
    {
        if (changedScene) EnterFade();
    }

    public void EnterFade()
    {
        StartCoroutine("LoadScreenFade");
    }

    IEnumerator LoadScreenFade()
    {
        //loadingMenu.SetActive(true); // activar o canvas

        //yield return new WaitUntil(() => LoadSceneSingleton.instance.LoadProgress() >= 0.7f);
        //faz algo

        yield return new WaitForSeconds(2.0f);
        LoadSceneSingleton.instance.ActivateScene();
        yield return new WaitUntil(() => LoadSceneSingleton.instance.CheckIfSceneReady() == true);
        gameObject.GetComponent<Animator>().SetBool("canFade", false);
        yield return new WaitForSeconds(1.25f);
        loadingMenu.SetActive(false);
        changedScene = false;
    }

   
}
