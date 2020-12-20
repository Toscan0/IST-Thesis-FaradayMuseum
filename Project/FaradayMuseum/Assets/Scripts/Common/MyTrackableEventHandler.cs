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
    [SerializeField]
    [Tooltip("Initial Explanation of the Main Canvas. Same object to all the targets")]
    private GameObject initalExplanation;
    [SerializeField]
    private TargetIDs targetID;
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

        targetManager.TargetID = targetID.ToString();
        targetManager.IsImageTarget = isImageTarget;

        if (UI != null)
        {
            UI.SetActive(true);
        }

        OnTrackingObj?.Invoke(true);

        initalExplanation.SetActive(true);

        singleton.AddGameEvent(LogEventType.TrackingTarget, "TrackingFound! TargetID: " + targetID + " Image target: " + isImageTarget);
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

        singleton.AddGameEvent(LogEventType.TrackingTarget, "TrackingLost! TargetID: " + targetID + " Image target: " + isImageTarget);
    }
}
