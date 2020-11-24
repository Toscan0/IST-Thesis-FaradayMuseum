using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class DownloadImage : MonoBehaviour
{
    private readonly string screenshotName = "temp.png";

    public void Download()
    {
        string screenShotPath = Application.persistentDataPath + "/" + screenshotName;

        string dowloadName = DateTime.Now.ToString("mmssddyyyy") + ".png";

        string downloadPath = Application.persistentDataPath + "/Screenshots/" + dowloadName;

        if (File.Exists(screenShotPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Screenshots");
            File.Copy(screenShotPath, downloadPath);

            ShowToastMethod("ScreenShot Saved: " + dowloadName);
        }
    }

    private string toastString;
    private AndroidJavaObject currentActivity;

    public void ShowToastMethod(string stringtoShow)
    {
    #if !UNITY_EDITOR && UNITY_ANDROID
            ShowToastOnUiThread(stringtoShow);
    #endif
    }

    void ShowToastOnUiThread(string stringtoShow)
    {
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        toastString = stringtoShow;

        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(ShowToast));
    }

    void ShowToast()
    {
        Debug.Log("Running on UI thread");
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_LONG"));
        toast.Call("show");
    }

}