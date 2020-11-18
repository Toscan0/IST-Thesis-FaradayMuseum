using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour {

    public List<GameObject> UIButtons;


    public void Show(GameObject gameObject)
    {
        gameObject.SetActive(true);
        //gameObject.GetComponent<Animator>().Play("fadein" + gameObject.name);
        //StartCoroutine(FadeCanvas(gameObject, gameObject.GetComponent<CanvasGroup>(), 0.0f, 1.0f, 0.5f));
    }

    public void Hide(GameObject gameObject)
    {
        gameObject.SetActive(false);
        //gameObject.GetComponent<Animator>().Play("fadeout" + gameObject.name);
        //StartCoroutine(FadeCanvas(gameObject, gameObject.GetComponent<CanvasGroup>(), 1.0f, 0.0f, 0.5f));
    }

    public void HideAllUIGameObjects(){
        foreach(GameObject go in UIButtons)
        {
            go.SetActive(false);
        }
    }

    public void ShowAllUIGameObjects()
    {
        foreach (GameObject go in UIButtons)
        {
            go.SetActive(true);
        }
    }

    public void ShowAllUIExceptThis(GameObject gameObject){

        List<GameObject> tempList = new List<GameObject>(UIButtons);

        tempList.Remove(gameObject);

        foreach (GameObject go in tempList)
        {
            go.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    public void HideAllUIExceptThis(GameObject gameObject)
    {
        List<GameObject> tempList = new List<GameObject>(UIButtons);

        tempList.Remove(gameObject);

        foreach (GameObject go in tempList)
        {
            go.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    public void CancelUIClick(GameObject gameObject)
    {
        List<GameObject> tempList = UIButtons;

        tempList.Remove(gameObject);
        tempList.Remove(GameObject.Find("ExtendedMenu"));

        foreach (GameObject go in tempList)
        {
            go.SetActive(true);
            if(go.name == "MainMenu"){
                go.GetComponent<Animator>().Play("fadein" + go.name);
            }

        }

        gameObject.SetActive(false);
        GameObject.Find("ExtendedMenu").SetActive(false);
    }
}
