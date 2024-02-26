using System;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    
    public float smoothSpeed = 0.01f;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
            targetPos, smoothSpeed);

        transform.position = smoothFollow;
    }
}
