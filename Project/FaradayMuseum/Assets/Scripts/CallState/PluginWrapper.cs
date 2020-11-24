using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PluginWrapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();

        var plugin = new AndroidJavaClass("unity.phoneix.com.unitycallstateplugin.PluginClass");

        text.text = plugin.CallStatic<string>("GetTextFromPlugin", 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
