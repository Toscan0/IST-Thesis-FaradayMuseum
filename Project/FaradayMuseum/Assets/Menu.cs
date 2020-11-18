using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject extendedMenu;

    // Update is called once per frame
    void Update()
    {

    }

    public void Disable(){
        gameObject.SetActive(false);
    }

    public void Enable(){
        gameObject.SetActive(true);
    }

}
