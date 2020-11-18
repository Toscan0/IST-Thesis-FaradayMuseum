using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YYAxis : MonoBehaviour, IRotable
{
    [SerializeField]
    private Color color;
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = color;
    }

    Axis IRotable.GetAxis()
    {
        return Axis.y;
    }

    void IRotable.ApplyRotation(Transform target, float angle)
    {
        target.Rotate(Vector3.down, angle, Space.World);

        GetComponent<Renderer>().material.color = Color.yellow;
    }

    void IRotable.ResetAxis()
    {
        rend.material.color = color;
    }
}