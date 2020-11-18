using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static string expertiseLevel  = "begginer";
   
    //set expertise level when dropdwon is changed
    //case sensitive
    public void SetExpertiseLevel(int value)
    {
        if(value == 0) // begginer
        {
            expertiseLevel = "begginer";
        }
        else if(value == 1) // medium
        {
            expertiseLevel = "medium";
        }
        else if (value == 2) // expert
        {
            expertiseLevel = "expert";
        }
        else
        {
            Debug.LogWarning("Well, this is shameful... Expertise level not recognized!");
        }
    }

    public string GetExpertiseLevel()
    {
        return expertiseLevel;
    }
}
