using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform target;
    [SerializeField]
    [Range(0.01f, 5f)]
    private float sensitivity = 0.01f;
    [SerializeField]
    private GameObject[] axis;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 lastEndPosition;

    private Quaternion defaultTargetRotation;

    private bool rotationEnabled;

    private IRotable rotableObj;
    private Axis selectedAxis;

    public bool reset = false;

    private void Awake()
    {
        defaultTargetRotation = target.rotation;
    }

    private void OnEnable()
    {
        RotateButton.OnRotateButtonClicked += AllowRotation;
        ResetRotationButton.OnResetRotationButtonClicked += ResetRotation;
    }

    private void AllowRotation(bool allowRotation)
    {
        rotationEnabled = allowRotation;

        foreach(var el in axis)
        {
            el.SetActive(allowRotation);
        }
    }

    private void Update()
    {
        if (reset == true)
        {
            ResetRotation();
        }

        if (rotationEnabled)
        {
            Vector3 mousePos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    rotableObj = hitInfo.transform.GetComponent<IRotable>();
                    if (rotableObj != null)
                    {
                        selectedAxis = rotableObj.GetAxis();
                        StartRotating(mousePos);

                    }
                }
            }
            else if (Input.GetMouseButton(0))
            {
                ContinueRotating(mousePos);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
        }
    }

    private void StartRotating(Vector3 mousePos)
    {
        startPosition = mousePos;
    }

    private void ContinueRotating(Vector3 mousePos)
    {
        endPosition = mousePos;

        CalculateRotation();
        lastEndPosition = endPosition;
    }

    private void EndDrag()
    {
        if(rotableObj != null)
        {
            rotableObj.ResetAxis();
        }
       

        // reset variables
        selectedAxis = Axis.none;
        rotableObj = null;
    }


    private void CalculateRotation()
    {
        float angle = 0;

        if (selectedAxis != Axis.none && !endPosition.Equals(lastEndPosition))
        {
            float deltaX = endPosition.x - startPosition.x;
            float deltaY = endPosition.y - startPosition.y;

            if ((deltaX) == 0 && (deltaY) != 0)
            {
                angle = deltaY;
            }
            else if ((deltaX) != 0 && (deltaY) == 0)
            {
                angle = deltaX;
            }
            else if ((deltaX) != 0 && (deltaY) != 0)
            {
                //it will always be a postiive angle
                angle = Mathf.Sqrt(Mathf.Pow(deltaX, 2f) + Mathf.Pow(deltaY, 2f));

                //so lets give a signal
                angle = deltaY < 0 ? -angle : angle;
            }

            //aply sensitivity
            angle = angle * sensitivity;

            if (rotableObj != null)
            {
                rotableObj.ApplyRotation(target, angle);
            }
            //startPosition = endPosition;
        }
    }

    private void ResetRotation()
    {
        target.rotation = defaultTargetRotation;
    }

    private void OnDisable()
    {
        RotateButton.OnRotateButtonClicked -= AllowRotation;
        ResetRotationButton.OnResetRotationButtonClicked -= ResetRotation;
    }
}