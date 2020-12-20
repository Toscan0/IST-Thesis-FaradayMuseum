using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement{

    [Tooltip("Achievement ID, not showed to the user")]
    public string id;
    public string title;
    public string description;

    [Tooltip("How many times need to be acomplished to unlock")]
    public int objective;
    public int points;

    [Tooltip("If this achivement have an explanation")]
    public bool haveExplanation;

    public TargetIDs targetID;
}
