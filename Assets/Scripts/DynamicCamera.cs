using System;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed = 0.1f;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
            targetPos, smoothSpeed);

        transform.position = smoothFollow;
    }
}
