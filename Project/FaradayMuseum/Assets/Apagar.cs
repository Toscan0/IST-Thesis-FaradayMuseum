using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apagar : MonoBehaviour
{

    public Transform Left;
    public Transform Right;

    public GameObject myLine;

    private LineRenderer myLineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myLineRenderer = myLine.GetComponent<LineRenderer>();
        myLine.GetComponent<LineRenderer>().positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        myLineRenderer.SetPosition(0, Left.position);
        myLineRenderer.SetPosition(1, Right.position);

        float distance = Vector3.Distance(Right.position, Left.position);

        myLineRenderer.GetComponent<Renderer>().material.mainTextureScale = new Vector2(distance * 2, 1);


    }
}
