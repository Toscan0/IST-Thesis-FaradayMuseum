using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class TargetManager : MonoBehaviour
{
    public string TargetID { get; set; }
    public bool IsImageTarget { get; set; } //if false target is type: model target
}
