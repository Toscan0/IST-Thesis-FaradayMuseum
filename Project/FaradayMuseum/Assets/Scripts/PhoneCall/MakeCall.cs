using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCall : MonoBehaviour
{
    public float timer = 3.0f;
    public Countdown countdown;
    public GameObject dialog;
    public GameObject achievWindow;
   
    public void StartTimer()
    {
        achievWindow.SetActive(false);
        dialog.SetActive(true);
        countdown.StartTimer(timer);
    }

    public void StopTimer()
    {
        dialog.SetActive(false);
        countdown.StopTimer();
    }
}
