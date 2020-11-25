using System;
using UnityEngine;

public class MyTrackableEventHandler : DefaultTrackableEventHandler
{
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    //true on tracking found, false on tracking lost
    public static Action<bool> OnTrackingObj;

    #region PRIVATE_VARIABLES

    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private TargetManager targetManager;
    [Tooltip("Initial Explanation of the Main Canvas. Same object to all the targets")]
    public GameObject initalExplanation;
    [SerializeField]
    [Tooltip("This is the ID of the artifact, should match the achivements and the pop-ups. Case Sensitive")]
    private string artifactID;
    [SerializeField]
    [Tooltip("Check if this target is a image target")]
    private bool isImageTarget;

    #endregion

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        targetManager.TargetID = artifactID;
        targetManager.IsImageTarget = isImageTarget;

        if (UI != null)
        {
            UI.SetActive(true);
        }

        OnTrackingObj?.Invoke(true);

        initalExplanation.SetActive(true);

        singleton.AddGameEvent(LogEventType.TrackingTarget, "TrackingFound! TargetID: " + artifactID + " Image target: " + isImageTarget);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        if (UI != null)
        {
            UI.SetActive(false);
        }

        targetManager.TargetID = "";
        targetManager.IsImageTarget = false;

        OnTrackingObj?.Invoke(false);

        singleton.AddGameEvent(LogEventType.TrackingTarget, "TrackingLost! TargetID: " + artifactID + " Image target: " + isImageTarget);
    }
}
