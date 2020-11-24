using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class putBackgroundDark : MonoBehaviour {
    public GameObject backgroundPanel1;
    public GameObject backgroundPanel2;
    private Animator backGroundAnimator;
    public GameObject infoPanelInstruccoes;
	// Use this for initialization
	void Start () {
        //backGroundAnimator = backgroundPanel.GetComponent<Animator>();
	}

    public void turnBackgroundDark(int index)
    {
        if(index == 1)
        {
            backgroundPanel1.GetComponent<Animator>().SetBool("putDark", true);
            backgroundPanel1.GetComponent<Image>().raycastTarget = true;
        }
        else if( index == 2)
        {
            backgroundPanel2.GetComponent<Animator>().SetBool("putDark", true);
            backgroundPanel2.GetComponent<Image>().raycastTarget = true;
        }
        
        
        if(infoPanelInstruccoes != null)
        {
            infoPanelInstruccoes.SetActive(false);
            infoPanelInstruccoes.SetActive(true);
        }
    }

    public void turnBackgroundTransparent(int index)
    {
        if (index == 1)
        {
            backgroundPanel1.GetComponent<Animator>().SetBool("putDark", false);
            backgroundPanel1.GetComponent<Image>().raycastTarget = false;
        }
        else if (index == 2)
        {
            backgroundPanel2.GetComponent<Animator>().SetBool("putDark", false);
            backgroundPanel2.GetComponent<Image>().raycastTarget = false;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
