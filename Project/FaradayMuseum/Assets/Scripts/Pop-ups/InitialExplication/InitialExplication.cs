using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Initial Explication", menuName = "ScriptableObjects/InitialExplication")]
public class InitialExplication : ScriptableObject
{
    [Tooltip("Case Sensitive!")]
    public string artifactID;

    public string title;
    [TextArea]
    public string description;

    [Tooltip("Use lower case! Case Sensitive with settingsManager.cs")]
    public string expertiseLevel;
}
