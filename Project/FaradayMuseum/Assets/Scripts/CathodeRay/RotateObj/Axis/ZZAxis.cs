using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZAxis : MonoBehaviour, IRotable
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
        return Axis.z;
    }

    void IRotable.ApplyRotation(Transform target, float angle)
    {
        target.Rotate(Vector3.forward, angle, Space.World);

        GetComponent<Renderer>().material.color = Color.yellow;
    }

    void IRotable.ResetAxis()
    {
        rend.material.color = color;
    }
}