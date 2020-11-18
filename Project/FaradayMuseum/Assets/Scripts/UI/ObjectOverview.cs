using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOverview : MonoBehaviour
{
    public BoxBlur ARCameraBlur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Ended)
        {
            gameObject.SetActive(false);

            ARCameraBlur.ResetBlur();
        }
        */

        if (Input.GetMouseButtonDown(0)){

            gameObject.SetActive(false);

            ARCameraBlur.ResetBlur();
        }
    }
}
