using System.Collections;
using System.IO;
using UnityEngine;

public class ShareImage : MonoBehaviour
{

    public string subject;
    public string ShareMessage;
    private bool isProcessing = false;
    private readonly string screenshotName = "temp.png";


    public void Share()
    {
        if (!isProcessing)
            StartCoroutine(ShareScreenshot());
    }


    public IEnumerator ShareScreenshot()
    {
        isProcessing = true;

        string screenShotPath = Application.persistentDataPath + "/" + screenshotName;

        yield return new WaitForSecondsRealtime(0.2f);
        if (!Application.isEditor)
        {
            new NativeShare().AddFile(screenShotPath).SetSubject(subject).SetText(ShareMessage).Share();
        }

        isProcessing = false;
    }

}