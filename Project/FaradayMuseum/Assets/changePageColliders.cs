using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePageColliders : MonoBehaviour {

    private Collider[] pageColliders;
    private Animator pageAnimator;
    private bool changeCollider;
    private GameObject targetObject;
    // Use this for initialization

    private void Awake()
    {
        pageColliders = GetComponents<Collider>();
        pageAnimator = GetComponent<Animator>();
        targetObject = gameObject.GetComponentInParent<PageController>().targetObject;
    }

    /*void Start () {
        pageColliders = GetComponents<Collider>();
        pageAnimator = GetComponent<Animator>();
        targetObject = gameObject.GetComponentInParent<PageController>().targetObject;

    }*/
	
	// Update is called once per frame
	void Update () {
        ////Debug.Log("dragRight é :" + pageAnimator.GetBool("dragRight"));
        ////Debug.Log("dragLeft é :" + pageAnimator.GetBool("dragLeft"));
       // //Debug.Log("targetObject é : " + targetObject.name);
        if (targetObject.name == "Tutorial1")
        {
            /*if(pageAnimator.GetBool("dragRight") && !pageAnimator.GetBool("dragRight"))
            {

            }*/
            ////Debug.Log("dragRight é :" + pageAnimator.GetBool("dragRight"));
            ////Debug.Log("dragLeft é :" + pageAnimator.GetBool("dragLeft"));
            if (pageAnimator.GetBool("dragRight"))
            {
                ////Debug.Log("entrei aqui no dragRight: ");
                pageColliders[1].enabled = false;
            }
            
            else if(pageAnimator.GetBool("dragLeft"))
            {
                ////Debug.Log("entrei aqui no dragLeft:");
                pageColliders[0].enabled = true;
            }
        }
	}
}
