using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CallState : MonoBehaviour {
 
     #if UNITY_ANDROID
         AndroidJavaObject jc;
    #endif

    public Button m_CallButton;
 
     void Start ()
     {
        m_CallButton.onClick.AddListener(CallButtonOnClick);

         #if UNITY_ANDROID
             AndroidJNI.AttachCurrentThread();
             jc = new AndroidJavaClass("unity.phoneix.com.unitycallstateplugin.CallStateUnity");
             jc.CallStatic("setCallBack", new object[2] {gameObject.name, "OnCallStateChange"});

             Debug.Log("OnStart CallState");
         #endif
     }

    private void CallButtonOnClick()
    {
        /*
        Debug.Log("CallButtonOnclick");
        m_CallButton.GetComponent<Image>().color = Color.blue;
        CheckCallStatus();
        */       
    }

    public void OnCallStateChange(string state){
         Debug.Log("call status:" + state);
        m_CallButton.GetComponent<Image>().color = Color.magenta;
    }

    #if UNITY_ANDROID
        public int CheckCallStatus(){
         int i = jc.CallStatic<int>("getCallStatus");
         
         m_CallButton.GetComponent<Text>().text = i.ToString();

        return i;

        }
    #endif
}