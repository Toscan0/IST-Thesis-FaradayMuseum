using UnityEngine;

public interface IRotable
{
    Axis GetAxis();

    void ApplyRotation(Transform target, float angle);

    void ResetAxis();
}