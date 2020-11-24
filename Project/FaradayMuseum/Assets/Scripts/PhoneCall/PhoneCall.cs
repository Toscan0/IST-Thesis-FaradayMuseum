using System;
using System.Collections;
using UnityEngine;

public class PhoneCall : MonoBehaviour
{
    private bool isCalling = false;
    private bool callMade;

    public void Call()
    {
        if (!isCalling)
            StartCoroutine(AndroidCall());

        callMade = false;

    }

    public IEnumerator AndroidCall()
    {
        isCalling = true;

        // wait for graphics to render
        yield return new WaitForEndOfFrame();

        yield return new WaitForSecondsRealtime(0.3f);

        if (!Application.isEditor)
        {
            string phoneNum = "tel: 218417663";

            //For accessing static strings(ACTION_CALL) from android.content.Intent
            AndroidJavaClass intentStaticClass = new AndroidJavaClass("android.content.Intent");
            string actionCall = intentStaticClass.GetStatic<string>("ACTION_CALL");

            //Create Uri
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", phoneNum);

            //Pass ACTION_CALL and Uri.parse to the intent
            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", actionCall, uriObject);

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            try
            {
                //Start Activity
                unityActivity.Call("startActivity", intent);

                callMade = true;

            }

            catch (Exception e)
            {
                Debug.LogWarning("Failed to Dial number: " + e.Message);
            }

            isCalling = false;
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (callMade && hasFocus)
        {
            GetComponent<AudioSource>().Play();
            callMade = false;
        }
    }

}