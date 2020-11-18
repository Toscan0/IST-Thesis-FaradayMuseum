using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTextureOffset : MonoBehaviour {

    public float scrollSpeed = 0.5F;
    private float offset;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        offset = 0;
    }
    void Update()
    {
        offset += Time.deltaTime * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }

    void OnDisable()
    {
        //Debug.Log("PrintOnDisable: script was disabled");
    }

    void OnEnable()
    {
        //Debug.Log("PrintOnEnable: script was enabled");
    }
}
