using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class panelAnimationController : MonoBehaviour
    , IPointerClickHandler
{
    Animator animator;
    bool clickedPanel;
    AnimatorStateInfo stateInfo;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        //stateInfo = animator.GetNextAnimatorStateInfo(0);
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


    public void OnPointerClick(PointerEventData eventData)
    {

        singleton.AddGameEvent(LogEventType.Click, gameObject.tag);

        ////Debug.Log("Nome do gameobject: " + gameObject.name);
        if (gameObject.tag == "Objectives")
        {
            // ////Debug.Log("Nome do gameobject: " + gameObject.name);
            //se carregar nos objetivos faz reset a variavel de tremer pois quer dizer que o player já viu que estava a tremer
            animator.SetBool("somethingNew", false);
            if (animator.GetBool("clickedLeftPanel"))
            {
                animator.SetBool("clickedLeftPanel", false);
            }
            else
            {
                animator.SetBool("clickedLeftPanel", true);

            }
        }
        else if (gameObject.tag == "Tools")
        {
            if (animator.GetBool("clickedRightPanel"))
            {
                animator.SetBool("clickedRightPanel", false);
            }
            else
            {
                animator.SetBool("clickedRightPanel", true);
            }
        }

        /*else if (gameObject.tag == "InfoPanel")
        {
            //gameObject.SetActive(false);
            animator.SetBool("clickedOrTimeOut", true);
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().atendeu == true && GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().finishSecondDemo != true)
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().atendeu = false;
            }
            GameObject.FindGameObjectWithTag("GameController").GetComponent<EletricityController>().canvasStart = true;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<EletricityController>().firstTimeObjectiveShakeAnim = true;

        }*/
        /*
        else if (gameObject.tag == "InfoPanelBook")
        {
            //gameObject.SetActive(false);
            animator.SetBool("clickedOrTimeOut", true);
            GameObject.FindGameObjectWithTag("Book").GetComponent<PageController>().stopBoolean = false;
            //GameObject.FindGameObjectWithTag("Book").GetComponent<PageController>().buttonsInteractableBool = true;
        }
        */
    }
    public void Destroy()
    {

        Destroy(this.gameObject);

    }
    public void setInfoPanelBoolTelephone(bool flag)
    {
        singleton.AddGameEvent(LogEventType.InfoClose, transform.parent.gameObject.tag);

        if (transform.parent.gameObject.tag == "InfoPanel")
        {
            Animator animator = transform.parent.gameObject.GetComponent<Animator>();
            animator.SetBool("clickedOrTimeOut", true);
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().atendeu == true && GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().finishSecondDemo != true)
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().atendeu = false;
            }
            GameObject.FindGameObjectWithTag("GameController").GetComponent<EletricityController>().canvasStart = true;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<EletricityController>().firstTimeObjectiveShakeAnim = true;
        }
        if (transform.parent.gameObject.tag == "InfoPanelBook")
        {
            singleton.AddGameEvent(LogEventType.InfoClose);

            Animator animator = transform.parent.gameObject.GetComponent<Animator>();
            //gameObject.SetActive(false);
            animator.SetBool("clickedOrTimeOut", true);
            GameObject.FindGameObjectWithTag("Book").GetComponent<PageController>().stopBoolean = false;
            //GameObject.FindGameObjectWithTag("Book").GetComponent<PageController>().buttonsInteractableBool = true;
        }
        if(transform.parent.gameObject.tag == "InfoPanelIntruccoes")
        {
            transform.parent.GetComponent<Animator>().SetBool("clickedOrTimeOut", true);
            GameObject.FindGameObjectWithTag("DarkBackground").GetComponent<Animator>().SetBool("canDark", false);
            
        }

    }
}
