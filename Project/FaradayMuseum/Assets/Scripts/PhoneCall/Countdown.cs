using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {
 
    public float timeLeft = 5.0f;
    public bool stop = true;
 
    private float minutes;
    private float seconds;

    public Text text;
    public PhoneCall phoneCall;
    public GameObject dialog;

    public void StartTimer(float from){
         stop = false;
         timeLeft = from;
         Update();
         StartCoroutine(UpdateCoroutine());
     }

    public void StartTimer()
    {
        stop = false;
        Update();
        StartCoroutine(UpdateCoroutine());
    }

    public void StopTimer()
    {
        stop = true;
        minutes = 0;
        seconds = 0;
    }

    void Update() {
         if(stop) return;
         timeLeft -= Time.deltaTime;
         
         minutes = Mathf.Floor(timeLeft / 60);
         seconds = timeLeft % 60;
         if(seconds > 59) seconds = 59;
         if(minutes < 0) {
             stop = true;
             minutes = 0;
             seconds = 0;

            dialog.SetActive(false);
            phoneCall.Call();
         }
     }
 
     private IEnumerator UpdateCoroutine(){
         while(!stop){
            text.text = seconds.ToString("0");
             yield return new WaitForSeconds(0.2f);
         }
     }
 }