using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game End", menuName = "ScriptableObjects/GameEnd")]
public class GameEnd : ScriptableObject
{
    [Tooltip("Case Sensitive!")]
    public string artifactID;

    public string title;
    [TextArea]
    public string description;
}
