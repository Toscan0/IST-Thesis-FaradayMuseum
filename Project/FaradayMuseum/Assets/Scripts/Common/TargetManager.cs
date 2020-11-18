using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class TargetManager : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject centerSquare;

    private int currentId;
    private GameManager gameManager = GameManager.Instance;

    private string targeID;
    private bool imageTarget; //if false target -> model target.

    void OnDisable() {
        gameManager.OnStateChange -= HandleOnStateChange;
    }
    
    void Start()
    {
        currentId = 0;
        gameManager.OnStateChange += HandleOnStateChange;

        foreach (GameObject modelTarget in targets)
        {
            if (modelTarget.GetComponent<ModelTargetBehaviour>() != null)
            {
                modelTarget.GetComponent<ModelTargetBehaviour>().GuideViewMode = ModelTargetBehaviour.GuideViewDisplayMode.GuideView2D;
            }
            else
            {
                modelTarget.GetComponent<MyTrackableEventHandler>().preview?.SetActive(false);
            }
        }

        PreviewLogic();
    }

    private void HandleOnStateChange(object sender, GameStateChangedEventArgs args)
    {
        if (args.GameState == GameState.MAIN_MENU)
        {
            PreviewLogic();
        }
    }

    public void SetId(int dropdownId)
    {
        currentId = dropdownId;
        PreviewLogic();
    }

    public void PreviewLogic()
    {
        if (currentId == 0)
        {
            foreach (GameObject modelTarget in targets)
            {
                if (modelTarget.GetComponent<ModelTargetBehaviour>() != null)
                {
                    modelTarget.GetComponent<ModelTargetBehaviour>().GuideViewMode = ModelTargetBehaviour.GuideViewDisplayMode.GuideView2D;
                }
                else
                {
                    modelTarget.GetComponent<MyTrackableEventHandler>().preview?.SetActive(false);
                }
                modelTarget.SetActive(true);
                modelTarget.GetComponent<MyTrackableEventHandler>().StartTracking();
            }

            centerSquare.SetActive(true);
        }
        else
        {
            int tempCurrentId = currentId - 1;
            if (tempCurrentId > targets.Count - 1) return;

            GameObject modelTargetTemp = targets[tempCurrentId];

            foreach (GameObject modelTargetNotActive in targets)
            {
                if (modelTargetNotActive.GetComponent<ModelTargetBehaviour>() != null)
                {
                    modelTargetNotActive.GetComponent<ModelTargetBehaviour>().GuideViewMode = ModelTargetBehaviour.GuideViewDisplayMode.GuideView2D;
                }
                else
                {
                    modelTargetNotActive.GetComponent<MyTrackableEventHandler>().preview?.SetActive(false);
                }
                modelTargetNotActive.GetComponent<MyTrackableEventHandler>().StopTracking();
                modelTargetNotActive.SetActive(false);
            }

            if (modelTargetTemp == null) return;


            if (modelTargetTemp.GetComponent<ModelTargetBehaviour>() != null)
            {
                modelTargetTemp.GetComponent<ModelTargetBehaviour>().GuideViewMode = ModelTargetBehaviour.GuideViewDisplayMode.GuideView2D;
            }
            else
            {
                modelTargetTemp.GetComponent<MyTrackableEventHandler>().preview.SetActive(true);
            }

            modelTargetTemp.SetActive(true);
            modelTargetTemp.GetComponent<MyTrackableEventHandler>().StartTracking();
            centerSquare.SetActive(false);
        }
    }

    public string GetTargetID()
    {
        return targeID;
    }

    public void SetTargetID(string id)
    {
        targeID = id;
    }

    public bool GetImageTarget()
    {
        return imageTarget;
    }

    public void SetImageTarget(bool b)
    {
        imageTarget = b;
    }

}
