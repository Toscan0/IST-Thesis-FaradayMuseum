using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParserData : MonoBehaviour
{
    [SerializeField]
    private TargetManager targetManager;

    // For CR
    [SerializeField]
    private RotationUI rotationUI;
    [SerializeField]
    private IntensityUI intensityUI;
    [SerializeField]
    private TensionUI tensionUI;

    /*
     * This code may need to be redone to match the BLE module script
     * Or change the BLE script to match this
     * Im using the following sintaxt:
     *      CR;I;2;\n          (singel value passed)
     *      CR;I;2;R;90;T;150;\n  (multiple value passed)
     * Where:
     *      CR - target ID (need to know wich target refering too)
     *      ; - separator  (need for easy parser)
     *      I - Intensity  (For cathode Ray )
     *      R - Rotation   (For cathode Ray )
     *      T - Tension    (For cathode Ray )
     *      After each value ID (I,R,T) the respective value
     */

    public void Parser(string strToParse)
    {
        Debug.Log("Start Parsing: " + strToParse);
        string[] splited;
        string artifactID;

        strToParse = strToParse.Trim();

        if (strToParse != "" && strToParse != null)
        {
            // ';' needs to match the separator in BLE module script
            splited = strToParse.Split(';');
           
            artifactID = splited[0];
            if(artifactID != targetManager.TargetID)
            {
                Debug.LogError("ArtifactID parsed not match the tatget ArtifactID! " + 
                    "ArtifactID parsed: " + artifactID + " Target artifactID: " + targetManager.TargetID);
                return;
            }

            if(artifactID == TargetIDs.CR.ToString())
            {
                for (int i = 0; i < splited.Length; i++)
                {
                    if(splited[i] == "I")
                    {
                        intensityUI.SetIntensity(float.Parse(splited[i + 1]));
                    }
                    else if (splited[i] == "T")
                    {
                        tensionUI.SetTension(float.Parse(splited[i + 1]));
                    }
                    else if (splited[i] == "R")
                    {
                        rotationUI.SetRotation(float.Parse(splited[i + 1]));
                    }
                }
            }
            else
            {
                Debug.LogWarning("ArtifactID not recgnozided! ArtifactID: " + artifactID);
            }
      
        }
    }
}
