using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MyTrackableEventHandler : DefaultTrackableEventHandler
{
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    //true on tracking found, false on tracking lost
    public static Action<bool> OnTrackingObj;

    #region PUBLIC_VARIABLES

    public GameObject DropdownBorder;

    public GameObject hint;

    public bool giveHint;

    public bool active = false;

    [Tooltip("Initial Explanation of the Main Canvas. Same object to all the targets")]
    public GameObject preview;

    #endregion

    #region PRIVATE_VARIABLES

    [SerializeField]
    private GameObject UI;

    [SerializeField]
    private TargetManager targetManager;

    [SerializeField]
    [Tooltip("This is the ID of the artifact, should match the achivements and the pop-ups. Case Sensitive")]
    private string artifactID;

    [SerializeField]
    [Tooltip("Check if this target is a image target")]
    private bool imageTarget;

    private IEnumerator coroutine;

    private Game gameScript;

    #endregion

    protected override void Start()
    {
        giveHint = true;

        gameScript = gameObject.GetComponent<Game>();

        base.Start();
        StartCoroutine(Hint(5.0f));
    }

    protected override void OnTrackingFound()
    {
        if (!active) return;

        base.OnTrackingFound();

        giveHint = false;
        hint.SetActive(false);

        targetManager.SetTargetID(artifactID);
        targetManager.SetImageTarget(imageTarget);

        if (UI != null)
        {
            UI.SetActive(true);
        }

        OnTrackingObj?.Invoke(true);

        gameScript.StartGame();

        singleton.AddGameEvent(LogEventType.TrackingTarget, "TrackingFound! TargetID: " + artifactID + " Image target: " + imageTarget);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        giveHint = true;

        if (UI != null)
        {
            UI.SetActive(false);
        }
        gameScript.LostFocus();

        targetManager.SetTargetID("");

        OnTrackingObj?.Invoke(false);
        singleton.AddGameEvent(LogEventType.TrackingTarget, "TrackingLost! TargetID: " + artifactID + " Image target: " + imageTarget);
    }

    public void StartTracking()
    {
        active = true;
    }

    public void StopTracking()
    {
        active = false;
    }

    IEnumerator Hint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (giveHint && DropdownBorder.activeInHierarchy == false)
            {
                hint.SetActive(true);
                DropdownBorder.SetActive(true);
            }
        }
    }
}
