using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasAnimationController : MonoBehaviour {
    public GameObject returnButtonGameObject;
    public GameObject objectivePanelGameObject;
    public GameObject toolsPanelGameObject;
    public GameObject gameController;

    public Animator returnButtonAnimator;
    public Animator objectivePanelAnimator;
    public Animator toolsPanelGameAnimator;


    //VUFORIA STUFF
    private bool detectedObject;
    public bool phoned;
    // Use this for initialization
    void Start () {
        returnButtonAnimator = returnButtonGameObject.GetComponent<Animator>();
        objectivePanelAnimator = objectivePanelGameObject.GetComponent<Animator>();
        toolsPanelGameAnimator = toolsPanelGameObject.GetComponent<Animator>();
        phoned = gameController.GetComponent<EletricityController>().telefonou;
        detectedObject = true;
        if(detectedObject)
        {
            toolsPanelGameAnimator.SetBool("canShowMenu",true);
        }

    }
	
	// Update is called once per frame
	void Update () {
        //ver como tirar isto daqui
        phoned = gameController.GetComponent<EletricityController>().telefonou;
        /*
        if (phoned)
        {
            returnButtonAnimator.SetBool("canShowMenu", true);
            objectivePanelAnimator.SetBool("canShowMenu",true);
        }*/
        //Debug.Log(phoned);
	}
}
