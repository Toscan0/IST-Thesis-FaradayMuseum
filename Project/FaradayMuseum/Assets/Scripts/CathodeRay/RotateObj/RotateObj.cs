using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Another way to rotate the Obj
 * To use this, add this script to the obj and a collider
 * 
 */

public class RotateObj : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 20;

    private bool allowRotation = false;
    private Quaternion defaultRotation;

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    private void Start()
    {
        defaultRotation = gameObject.transform.rotation;
    }
    

    private void OnEnable()
    {
        RotateButton.OnRotateButtonClicked += AllowRotation;
        ResetRotationButton.OnResetRotationButtonClicked += ResetRotation;
    }

    private void ResetRotation()
    {
        gameObject.transform.rotation = defaultRotation;
    }

    private void AllowRotation(bool allowRotation)
    {
        this.allowRotation = allowRotation;
    }

    private void OnMouseDrag()
    {
        if(allowRotation)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            transform.Rotate(Vector3.down, rotX, Space.World);
            transform.Rotate(Vector3.right, rotY, Space.World);

            singleton.AddGameEvent(LogEventType.Click, "Obj Rotation: " + transform.localEulerAngles);
        }
        
    }

    private void OnDisable()
    {
        RotateButton.OnRotateButtonClicked -= AllowRotation;
        ResetRotationButton.OnResetRotationButtonClicked -= ResetRotation;
    }
}
