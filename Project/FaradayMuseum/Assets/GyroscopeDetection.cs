using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeDetection : MonoBehaviour
{

    private bool gyroEnabled;
    private Gyroscope gyroscope;

    private Quaternion lastGyro;
    private Quaternion currentGyro;

    private readonly float gyroMinValue = 0.22f;
    private readonly float gyroMaxValue = 0.42f;

    // Use this for initialization
    void Start()
    {
        gyroEnabled = EnableGyro();

        if (gyroEnabled == false){
            return;
        }

        //TODO what to do when Gyro is not detected
    }

    // Update is called once per frame
    void Update()
    {
        DetectGyroscopeMovement();
    }

    private void DetectGyroscopeMovement()
    {
        lastGyro = currentGyro;
        currentGyro = gyroscope.attitude;

        float delta = Quaternion.Angle(currentGyro, lastGyro);

        if (delta > gyroMinValue && delta < gyroMaxValue)
        {
            Debug.Log("HINT");
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
            return true;
        }
        else
        {
            return false;
        }

    }

}
