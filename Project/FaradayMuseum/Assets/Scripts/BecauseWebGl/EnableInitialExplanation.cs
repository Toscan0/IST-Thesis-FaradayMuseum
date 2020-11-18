using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInitialExplanation : MonoBehaviour
{
    public GameObject IE;

    void Start()
    {
        IE.SetActive(true);
    }
}
