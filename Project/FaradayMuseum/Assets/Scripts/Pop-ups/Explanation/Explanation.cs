using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Explanation", menuName = "ScriptableObjects/Explanation")]
public class Explanation : ScriptableObject
{
    [Tooltip("Case Sensitive!")]
    public string artifactID;

    [Tooltip("Case Sensitive! Case Sensitive with achivement")]
    public string ID;

    public string title = "Sabias que..." ;
    [TextArea]
    public string description;

    [Tooltip("Use lower case! Case Sensitive with settingsManager.cs")]
    public string expertiseLevel;
}
