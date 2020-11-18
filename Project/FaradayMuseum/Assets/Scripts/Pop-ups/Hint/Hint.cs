using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hint", menuName = "ScriptableObjects/Hint")]
public class Hint : ScriptableObject
{
    [Tooltip("Case Sensitive!")]
    public string artifactID;

    [Tooltip("Case Sensitive! Case Sensitive with achivement")]
    public string ID;

    public string title = "Hey, it seems that you need help...";
    [TextArea]
    public string description;

    [Tooltip("Use lower case! Case Sensitive with settingsManager.cs")]
    public string expertiseLevel;
}
