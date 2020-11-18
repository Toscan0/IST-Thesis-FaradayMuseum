using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonTouchRaycastController : MonoBehaviour
{

    public List<Button> buttons;
    private GameObject XMLManager;
    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("XMLManager") != null)
        {
            XMLManager = GameObject.FindGameObjectWithTag("XMLManager");
        }
        foreach (Button button in buttons)
        {
            //Debug.Log("Nome do gameobject: " + button.name);
        }
        //buttons = new List<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                //comunicar com o gameController
        }
		*/

    }

    void OnEnable()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => buttonCallBack(button));
        }
    }

    private void buttonCallBack(Button buttonPressed)
    {
        if (buttonPressed.name == "TelephoneDemo")
        {
            //Debug.Log("Clicked: " + buttonPressed.name);
            SceneManager.LoadScene("Telephone", LoadSceneMode.Single);
        }
    }

    void OnDisable()
    {
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
    /*
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
    //AdicionarAquiOutrasSituações
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Entrei aqui no pointerclick");
        ////Debug.Log("Nome do gameobject: " + gameObject.name);
        foreach(Button button in buttons)
        {
            //Debug.Log("Entrei aqui no FOREACH  do pointerclick");
            //entrei aqui dentro do pointer click no foreach
            if (button.tag == "TelephoneDemo")
            {
                SceneManager.LoadScene("Telephone", LoadSceneMode.Single);
            }
        }
        
        


    }
    */
}
