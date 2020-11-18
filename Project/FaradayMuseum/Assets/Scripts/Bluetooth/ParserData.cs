using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParserData : MonoBehaviour
{

    [SerializeField]
    private RotationUI rotationUI;
    [SerializeField]
    private IntensityUI intensityUI;
    [SerializeField]
    private TensionUI tensionUI;

    /*
    private float r = 358;
    private float i = 50;
    private float t = 100;
    private string test = "CR ";
    void Update()
    {
    //my ideia of what soul be the input
        test = "CR " + r + " " + i + " " + t;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(test);
            Parser(test);
            r++;
            i++;
            t++;
        }
    }*/


    public void Parser(string s)
    {
        string[] splited;
        string artifactID;

        s = s.Trim();

        if (s != "")
        {
            splited = s.Split(' ');
           
            /*for ( int i = 0; i < splited.Length; i++)
            {
                Debug.Log(splited[i]);
            }*/
            
            artifactID = splited[0];
            if(artifactID == "Gramme")
            {
                throw new NotImplementedException();
            }
            else if(artifactID == "CR")
            {
                throw new NotImplementedException();

                //rotationUI.SetRotation(float.Parse(splited[1]));
                //intensityUI.SetIntensity(float.Parse(splited[2]));
                //tensionUI.SetTension(float.Parse(splited[3]));
            }
            else
            {
                Debug.LogWarning("ArtifactID not recgnozided! ArtifactID: " + artifactID);
            }
      
        }
    }
}
