using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugManager : MonoBehaviour {

    public static debugManager debugInst;
    public Text debugText;
    // Use this for initialization
    void Awake () {
        debugInst = this;
        DontDestroyOnLoad(this);

	}
	
	
}
