using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
//using Vuforia.EditorClasses;

public class delayVuforiaInitialization : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        GetComponent<VuforiaBehaviour>().enabled = false;
        //GetComponent<DefaultInitializationErrorHandler>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
