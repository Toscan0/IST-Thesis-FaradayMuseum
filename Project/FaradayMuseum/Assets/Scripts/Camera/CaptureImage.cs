using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CaptureImage : MonoBehaviour{

    private bool isProcessing = false;
    private readonly string screenshotName = "temp.png";

    public GameObject captureImage;

    public GameObject photo;
    public GameObject afterPhoto;
    public ToggleController toggleButton;

    private bool isOn;
    private GameObject[] modelTargets;


    public void Start(){

        if (modelTargets == null)
            modelTargets = GameObject.FindGameObjectsWithTag("ModelTarget");

        toggleButton.isOn = isOn;
    }

    public void Update(){

        if (toggleButton.isOn == isOn) return;

        if (toggleButton.isOn){

            foreach(GameObject modelTarget in modelTargets){
                modelTarget.SetActive(true);
            }

            isOn = true;
        }else{
            foreach (GameObject modelTarget in modelTargets){
                modelTarget.SetActive(false);
            }
            isOn = false;
        }
    }

    public void Capture(){
        if (!isProcessing)
            StartCoroutine(CaptureScrenShot());
    }

    public IEnumerator CaptureScrenShot(){
        isProcessing = true;

        // wait for graphics to render
        yield return new WaitForEndOfFrame();
        string screenShotPath = Application.persistentDataPath + "/" + screenshotName;

        photo.SetActive(false);

        ScreenCapture.CaptureScreenshot(screenshotName, 2);

        yield return new WaitForSecondsRealtime(0.2f);

        captureImage.GetComponent<RawImage>().texture = LoadPNG(screenShotPath);
        captureImage.SetActive(true);

        isProcessing = false;


        afterPhoto.SetActive(true);
    }

    public void SetCaptureImage(bool value){
        captureImage.SetActive(value);
    }

    public static Texture2D LoadPNG(string filePath){

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
